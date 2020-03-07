using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories
{
    public interface ITeacherRepo
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherForIdAsync(int TeacherId);
        //CREATE (Async)
        Task<Teacher> Add(Teacher Teacher);
        //UPDATE (Async)
        Task<Teacher> Update(Teacher Teacher);
        //DELETE (Async)
        Task Delete(int TeacherId);
    }
}