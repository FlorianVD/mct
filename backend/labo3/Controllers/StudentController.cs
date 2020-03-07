using System.Threading.Tasks;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo studentRepo;
        public StudentController(IStudentRepo studentRepo)
        {
            this.studentRepo = studentRepo;
        }
        public async Task<IActionResult> Index()
        {
            var result = await studentRepo.GetAllStudentsAsync();
            return View(result);
        }

        public async Task<IActionResult> Details(int id) {
            var result = await studentRepo.GetStudentForIdAsync(id);
            return View(result);
        }
    }
}