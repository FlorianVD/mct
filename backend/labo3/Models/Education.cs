using System.Collections.Generic;

namespace backend.Models
{
    public class Education
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TeacherEducations> TeachersEducations { get;  set; }  
    }
}