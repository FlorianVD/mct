using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DataAccesPatterns.models;
using DataAccesPatterns.models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            this.studentRepo = studentRepo;
        }
        public async Task<IActionResult> Index(string search = null)
        {
            IEnumerable<Student> result = null;
            ViewBag.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.search = search;

            if (search != null) {
                result = await studentRepo.GetStudentByName(search);
            } else {
                result = await studentRepo.GetAllStudentsAsync();
            }
            return View(result);
        }

        public async Task<IActionResult> Details(int id) {
            var result = await studentRepo.GetStudentForIdAsync(id);
            return View(result);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create (Student student) {
            try {
                if (!ModelState.IsValid) {
                    throw new Exception("Validation Error");
                }
                Student createdStudent = await studentRepo.Add(student);
                return RedirectToAction(nameof(Index));
            } catch (Exception e) {
                Debug.WriteLine(e);
                return View(student);
            }
        }
    }
}