using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public enum Gender { Male, Female, Other };
    public class ParentUser : Entity
    {
        // Id from Entity
        public int FakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public Gender Gender { get; set; }
        public string ImagePath { get; set; }
    }
}
