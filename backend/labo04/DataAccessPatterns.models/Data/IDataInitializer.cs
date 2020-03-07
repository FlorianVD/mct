using System.Collections.Generic;
using DataAccesPatterns.models;

namespace DataAccesPatterns.models.Data
{
    public interface IDataInitializer
    {
        IEnumerable<Education> Educations { get; set; }
        IEnumerable<Student> Students { get; set; }
        IEnumerable<Teacher> Teachers { get; set; }
    }
}