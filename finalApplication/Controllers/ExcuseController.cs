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

        public string DeleteExcuse(string LEAVING_ID)
        {
            var count = new Employees().QueryReader("SELECT COUNT(*) AS COUNT FROM LEAVING WHERE LEAVING.LEAVING_ID=" + LEAVING_ID);

            int x = Int32.Parse(count.Rows[0]["COUNT"].ToString());
            Console.WriteLine(x);
            if (x == 0)
            {
                var DELETED_LEAVING_ID = new Excuse().DELETE_EXCUSE(LEAVING_ID);
                return LEAVING_ID;
            }

            return "-1";
        }

    }
}
