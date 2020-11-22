using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.Models;

namespace School.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        //configure FireBase Url and AuthSecret
        IFirebaseConfig objFireConfig = new FirebaseConfig()
        {
            AuthSecret = "CX2fjLrzvUS3pSmPUJrQxsdCNkCtW83vt6GgTAkp",
            BasePath = "https://school-109c6.firebaseio.com/"
        };

        //IFirebaseClient client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Student objStudent)
        {
            IFirebaseClient client = new FirebaseClient(objFireConfig);
            var setter = client.Set("StudentList/" + objStudent.StudentId, objStudent);

            return Content("Hello, " + objStudent.StudentId + "-" + objStudent.StudentName + " Inserted Successfully!!");


        }
        public IActionResult GetAllStudents()
        {
            IFirebaseClient client = new FirebaseClient(objFireConfig);
            client = new FirebaseClient(objFireConfig);
           
           var result = client.Get("StudentList/");
            return Content(result.Body);
        
        }

        public IActionResult GetStudentById()
        {
            IFirebaseClient client = new FirebaseClient(objFireConfig);
            var result = client.Get("StudentList/" + "2");
            Student obj = result.ResultAs<Student>();
            return Content(obj.StudentId + "-" + obj.StudentName + "-" + obj.StudentDivision);

        }

        public IActionResult UpdateStudentDetail(Student objStudent)
        {
            IFirebaseClient client = new FirebaseClient(objFireConfig);
            var setter = client.Update("StudentList/" + objStudent.StudentId, objStudent);

            return Content("Hello, " + objStudent.StudentId + "-" + objStudent.StudentName + " Updated Successfully!!");
           
        }

        public IActionResult DeleteStudent()
        {
            IFirebaseClient client = new FirebaseClient(objFireConfig);
            var setter = client.Delete("StudentList/" + 2);
            return Content("Record Deleted !!");

        }
    }
}

//Student onjStudent = new Student
//{
//    StudentId = Convert.ToInt32(HttpContext.Request.Form["StudentId"]),
//    StudentName = HttpContext.Request.Form["StudentName"],
//    StudentDivision = HttpContext.Request.Form["StudentDivision"]
//};
