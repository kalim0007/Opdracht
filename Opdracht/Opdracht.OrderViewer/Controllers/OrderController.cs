using Opdracht.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opdracht.OrderViewer.Controllers
{
    public class OrderController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Order
        public ActionResult Index(string sortOrder)
        {
            var orders = from o in db.Orders select o;
            ViewData["DateSort"] = sortOrder == "date_desc" ? "date_asc" : "date_desc";
            switch (sortOrder)
            {
                case "date_asc":
                    orders = orders.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    orders = orders.OrderBy(s => s.CreatedAt);
                    break;
            }
            return View(orders);
        }
    }
}