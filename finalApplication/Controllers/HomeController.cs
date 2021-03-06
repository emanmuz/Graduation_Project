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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var emp = new Employees().QueryReader("SELECT * FROM EMPLOYEE");
            ViewBag.DataEmp = emp.DefaultView;
            return View();
        }

        public IActionResult Emplist()
        {
            var Data = new Employees().QueryReader("SELECT * FROM EMPLOYEE");
            ViewBag.DataEmp = Data.DefaultView;
            return View();
        }

        public IActionResult EmployeeIndex(string ID)
        {
            var Data = new Employees().QueryReader("SELECT * FROM EMPLOYEE  WHERE EMPLOYEE.EMP_ID" + ID);
            ViewBag.DataEmployee = Data.DefaultView;
            return View();
        }
        public IActionResult Attendance()
        {
            var Data = new Employees().QueryReader("SELECT * FROM EMPLOYEE");
            ViewBag.DataAttendance = Data.DefaultView;
            return View();
        }

        public IActionResult Search(string ID)
        {
            var Data = new Employees().QueryReader("SELECT * FROM EMPLOYEE WHERE EMPLOYEE.EMP_ID" + ID);
            ViewBag.search = Data.DefaultView;
            return View();
        }


        [HttpPost]
        public string add(int emp_id, string emp_name, string jop_titel, int start_hour, int total_hours, int leaving_hour, int jop_type, int days,
            string img_path, DateTime emp_dob, string user_name, string phone, int age, string city, string edu_degree)
        {

            var added_employee = new Employees().Add(emp_id, emp_name, jop_titel, start_hour, total_hours, leaving_hour, jop_type, days,
                img_path, emp_dob, user_name, phone, age, city, edu_degree);
            return emp_name;

        }

        public string DeleteEmployee(string EMP_ID)
        {
            var count = new Employees().QueryReader("SELECT COUNT(*) AS COUNT FROM EMPLOYEE WHERE EMPLOYEE.EMP_ID=" + EMP_ID);
            return "-1";
        }

        public string Update(int emp_id, string emp_name, string jop_titel, int start_hour, int total_hours, int leaving_hour, int jop_type, int days,
            string img_path, DateTime emp_dob, string user_name, string phone, int age, string city, string edu_degree)
        {

            var added_student = new Employees().Update(emp_id, emp_name, jop_titel, start_hour, total_hours, leaving_hour, jop_type, days,
                img_path, emp_dob, user_name, phone, age, city, edu_degree);
            return emp_name;
        }

        public JsonResult GetEmployee(string ID)
        {
            var Data = new Employees().QueryReader("SELECT * FROM EMPLOYEE");
            return Json(Data);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
