using System.Threading.Tasks;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepo teacherRepo;

        public TeacherController(ITeacherRepo teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        public async Task<IActionResult> Index()
        {
            var result = await teacherRepo.GetAllTeachersAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO: Implement Realistic Implementation
            await teacherRepo.Delete(id);
            return Ok();
        }
        
    }
}