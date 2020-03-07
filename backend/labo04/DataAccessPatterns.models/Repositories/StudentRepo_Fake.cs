using System.Threading.Tasks;
using System.Linq;
using DataAccesPatterns.models.Data;
using DataAccesPatterns.models;
using System.Collections.Generic;

namespace DataAccesPatterns.models.Repositories
{
    public class StudentRepo_Fake : IStudentRepo
    {
        private readonly IDataInitializer dataInitializer;
        public StudentRepo_Fake(IDataInitializer dataInitializer)
        {
            this.dataInitializer = dataInitializer;
        }

        public Task<Student> Add(Student student)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Student> GetStudentForIdAsync(int studentId)
        {
            var result = dataInitializer.Students.Where(s => s.ID == studentId)
                .FirstOrDefault();
            return await Task.FromResult(result);
        }

        public Task<IEnumerable<Student>> GetStudentsByEducationAsync(int educationId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Student> Update(Student student)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var result = dataInitializer.Students.OrderBy(student => student.Name);

            return await Task.FromResult(result);
        }

        public Task<IEnumerable<Student>> GetStudentByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}