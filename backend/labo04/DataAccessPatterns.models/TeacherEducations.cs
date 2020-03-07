namespace DataAccesPatterns.models
{
    public class TeacherEducations
    {
        public int TeacherId { get; set; }
        public int EducationId { get; set; }

        public Teacher Teacher { get; set; }
        public Education Education { get; set; }
    }
}