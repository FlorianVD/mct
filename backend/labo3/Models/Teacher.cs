using System.Collections.Generic;

namespace backend.Models
{
    public class Teacher : Person
    {
        public ICollection<TeacherEducations> TeachersEducations { get;  set; }  
    }
}