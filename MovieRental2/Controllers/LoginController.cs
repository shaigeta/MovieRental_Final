using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MovieRental2.Models;
using System.Net.Mail;
using System.Security.Cryptography;
//using System.Text;
using Microsoft.Security.Application;

namespace MovieRental2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["Cart"] = 0;
            ViewBag.result = "";
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            return View("Index");
        }
        //public ActionResult Authenticate(Users usr)
        //{
        //    Model1 db = new Model1();
        //    if (ModelState.IsValid)
        //    {
        //        List<Users> userValidated = (from u in db.Users
        //                                    where (u.UserName == usr.UserName) && (u.Password == usr.Password)
        //                                    select u).ToList<Users>();

        //        if (userValidated.Count == 1)
        //        {
        //            //FormsAuthentication.SetAuthCookie("cookie", true);
        //            //if (usr.UserName == "shai")// Admin Permissions
        //            //  return RedirectToAction("Index", "Admin");

        //            //session 

        //            Session["UserId"] = userValidated[0].Id;//user found

        //            return RedirectToAction("Index", "Movies");
        //        }
        //        else
        //        {
        //            ViewBag.result = "The user is not on DataBase ,You Need to Accept permissions";
        //            return View("Index", usr);
        //        }
        //    }
        //    else
        //    {
        //        return View("Index", usr);
        //    }


        //}
        [HttpPost, ValidateInput(false)]
        public ActionResult Authenticate(MultipleModelsView multipleMview)
        {
            Users usr = multipleMview.usersModel;//specific model
            usr.UserName = Encoder.HtmlEncode(usr.UserName);//xss protection
            usr.Password = Encoder.HtmlEncode(usr.Password);//xss protection

            Model1 db = new Model1();
            if (ModelState.IsValid)
            {
                                                //LINQ to SQL will prevent any SQL Injection
                List<Users> userValidated = (from u in db.Users
                                                 where (u.UserName == usr.UserName) && (u.Password == usr.Password)
                                                 select u).ToList<Users>();
               

                if (userValidated.Count == 1)
                {
                    //FormsAuthentication.SetAuthCookie("cookie", true);
                    if (usr.UserName == "admin")// Admin Permissions, to control users, edit etc..
                    {
                        return RedirectToAction("Index", "Admin");
                        
                    }

                    Session["UserId"] = userValidated[0].Id;//user found
                    Session["UserName"] = userValidated[0].UserName;
                    return RedirectToAction("Index", "Movies");
                }
                else
                {
                    ViewBag.result = "The user is not on DataBase ,You Need to Accept permissions! >> please Registration First!";
                    return View("Index", multipleMview);//for stay value of entering
                }
            }
            else
            {
                return View("Index", multipleMview);//for stay value of entering
            }


           

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Registation(MultipleModelsView multipleMview)
        {
            Registration usr = multipleMview.registrationModel;//specific model
          
            //usr.UserName = Encoder.JavaScriptEncode(usr.UserName);//xss protection
            usr.UserName = Encoder.HtmlEncode(usr.UserName);//xss protection
            usr.Password = Encoder.HtmlEncode(usr.Password);
            usr.Email = Encoder.HtmlEncode(usr.Email);

            usr.Date = DateTime.Now;
            //Model1 db = new Model1();
            if (ModelState.IsValid)
            {
                using (Model1 db = new Model1())
                {

                    List<Users> userValidated = (from u in db.Users
                                                 where (u.UserName == usr.UserName)
                                                 select u).ToList<Users>();
                    if (userValidated.Count >= 1) // check if has one or more user
                    {
                        ViewBag.Message = "UnSuccessfully!!!, choice another User name!";
                    }
                    else
                    {
                        //////////////////////
                        //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                        //UTF8Encoding utf8 = new UTF8Encoding();
                        //byte[] data = md5.ComputeHash(utf8.GetBytes(usr.Password));
                        //usr.Password = Convert.ToBase64String(data);


                        db.Registration.Add(usr);
                        Users userUpdate = new Users { UserName = usr.UserName, Password = usr.Password };
                        db.Users.Add(userUpdate);
                        db.SaveChanges();
                        ModelState.Clear();
                        multipleMview = null;
                        ViewBag.Message = "Successfully Registration Done!";

                        /// sent Email
                        try
                        {
                            MailMessage mail = new MailMessage("greatchatmail@gmail.com", usr.Email);
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.EnableSsl = true;
                            client.Host = "smtp.gmail.com";
                            client.Credentials = new System.Net.NetworkCredential("greatchatmail@gmail.com", "greatchat123");
                            mail.Subject = "Welcome To Movie Rental!";
                            mail.Body = "Welcome To Movie Rental, start to orders movie!\n http://MovieRental.com";
                            client.Send(mail);
                        }
                        catch
                        {
                            ViewBag.Message = "something wrong";
                        }


                    }

                }
                return View("Index");
            }
            else
            {
                //ViewBag.Message = "UnSuccessfully Registration!";
                return View("Index", multipleMview);//for stay value of entering
            }
            
        }

    }
}