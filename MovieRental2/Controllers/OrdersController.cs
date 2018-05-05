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
    public class OrdersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["UserId"] != null)//checked if session userId set, for not allow to enter direct on url! 
            {
                int userId = (int)Session["UserId"];
                List<Orders> userOrders = (from u in db.Orders
                                          where (u.UserId == userId)
                                          select u).ToList<Orders>();
                ///////////////////////////////
                
                int price=0;
                int countCart = 0;
                List<Movies> listMovie=new List<Movies>();
                
                foreach (Orders itemId in userOrders)
                {
                    int idMovie= itemId.MovieId;
                    Movies movieModel = db.Movies.Find(idMovie);
                    price += movieModel.Price;

                    listMovie.Add(movieModel);//add obj item of one user to the list
                    countCart++;
                }
                // ViewBag.price = price*3.60;//convert $ to sekel 
                ViewBag.price = price;
                ViewBag.listMovieForView = listMovie;//for list of movies orders by one user

                Session["Cart"]=countCart;
                    /////////////////////
                    
                ////////////////////
                return View(userOrders.ToList());
            }

            return RedirectToAction("Index", "Login");
            // return View(db.Orders.ToList());
        }

        //// GET: Orders/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Orders orders = db.Orders.Find(id);
        //    if (orders == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orders);
        //}

        

        //// GET: Orders/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Orders orders = db.Orders.Find(id);
        //    if (orders == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orders);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,UserId,MovieId,date")] Orders orders)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(orders).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(orders);
        //}

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                //return HttpNotFound();
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
