namespace DataAccesPatterns.models
{
    public class Student : Person
    {
        public int? EducationId { get; set; }
        public Education Education { get; set; }
    }
}