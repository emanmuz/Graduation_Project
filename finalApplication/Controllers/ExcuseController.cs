using finalApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace finalApplication.Controllers
{
    public class ExcuseController : Controller
    {
        // GET: ExcuseController
        public ActionResult Index()
        {
            var DataExcuse = new Excuse().QueryReader("SELECT * FROM LEAVING");
            ViewBag.emp = DataExcuse.DefaultView;
            return View();
        }

        // GET: ExcuseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExcuseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExcuseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
      
    }
}
