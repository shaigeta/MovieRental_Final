using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRental2.Models;

namespace MovieRental2.Controllers
{
    public class MoviesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Movies
        public ActionResult Index()
        {
            if (Session["UserId"] != null)//checked if session userId set, for not allow to enter direct on url! 
            {
                int userId = (int)Session["UserId"];
                List<Orders> userOrders = (from u in db.Orders
                                           where (u.UserId == userId)
                                           select u).ToList<Orders>();
               
                Session["Cart"] = userOrders.Count;
            }
            else
            {
                return RedirectToAction("index", "Login");
            }

            return View(db.Movies.ToList());
        }


        // GET: Movies/Add/5

        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["UserId"] != null)//checked if session userId set, for not allow to enter direct on url! 
            {
                ///////////////////////////////////////////

                Movies movie = db.Movies.Find(id);

                Orders orderNew = new Orders();
                orderNew.MovieId = movie.MovieId;
                orderNew.UserId = (int)Session["UserId"];
                orderNew.date = DateTime.Now;

                db.Orders.Add(orderNew);
                db.SaveChanges();


                return RedirectToAction("index", "Orders");
            }
            return RedirectToAction("index", "Movies");
        }

        public JsonResult GetSearchValue(string search)
        {
            //List<Movies> allsearch = db.Movies.Where(x => x.Name.Contains(search)).Select(x => new Movies
            //    {
            //        MovieId = x.MovieId,
            //        Name = x.Name,

            //    }).ToList();

            List<Movies> allsearch =
            (from u in db.Movies
             where u.Name.Contains(search)
             select u).ToList<Movies>();

            return new JsonResult {Data=allsearch,JsonRequestBehavior=JsonRequestBehavior.AllowGet };

        }

    }
}