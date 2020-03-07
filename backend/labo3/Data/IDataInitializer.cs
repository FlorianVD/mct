using System.Collections.Generic;
using backend.Models;

namespace backend.Data
{
    public interface IDataInitializer
    {
        IEnumerable<Education> Educations { get; set; }
        IEnumerable<Student> Students { get; set; }
        IEnumerable<Teacher> Teachers { get; set; }
    }
}