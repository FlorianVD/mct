using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly SchoolDbContext context;

        public TeacherRepo(SchoolDbContext context)
        {
            this.context = context;
        }

        public Task<Teacher> Add(Teacher Teacher)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(int TeacherId)
        {
            var teacher = await context.Teachers.FindAsync(TeacherId);
            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await context.Teachers.OrderBy(e => e.ID).ToListAsync();
        }

        public Task<Teacher> GetTeacherForIdAsync(int TeacherId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Teacher> Update(Teacher Teacher)
        {
            throw new System.NotImplementedException();
        }
    }
}