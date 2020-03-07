using System;

namespace backend.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public string Email { get; set; }
        public string ImageUrl
        {
            get
            {
                return $"/images/{this.Name}.jpg";
            }
        }
        public Int16? DateOfBirth { get; set; }
    }
    public enum GenderType
    {
        Male = 0,
        Female = 1
    }
}