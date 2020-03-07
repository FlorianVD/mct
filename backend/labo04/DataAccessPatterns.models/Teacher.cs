using System.Collections.Generic;

namespace DataAccesPatterns.models
{
    public class Teacher : Person
    {
        public ICollection<TeacherEducations> TeachersEducations { get;  set; }  
    }
}