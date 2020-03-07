using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentForIdAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId);
        //CREATE (Async)
        Task<Student> Add(Student student);
        //UPDATE (Async)
        Task<Student> Update(Student student);
        //DELETE (Async)
        Task Delete(int studentId);
    }
}