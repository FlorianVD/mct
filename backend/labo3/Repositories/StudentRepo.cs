using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class StudentRepo : IStudentRepo
    {
        private readonly SchoolDbContext context;

        public StudentRepo(SchoolDbContext schooldbContext)
        {
            this.context = schooldbContext;            
        }

        public Task<Student> Add(Student student)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentForIdAsync(int studentId)
        {
            return await context.Students.FindAsync(studentId);
        }

        public Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new System.NotImplementedException();
        }
    }
}