using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccesPatterns.models
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
        [Display(Name = "Mannelijk")]
        Male = 0,
        [Display(Name = "Vrouwelijk")]
        Female = 1
    }
}