using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesPatterns.models.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccesPatterns.models.Repositories
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

        public async Task<IEnumerable<Student>> GetStudentByName(string name) {
            return await context.Students.Where(student => student.Name.Contains(name)).ToListAsync();
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