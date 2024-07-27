using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataTable1.Models
{
    public class Employee
    {
        //[Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }



        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Name must be between 8 and 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain English letters.")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Position is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Position must be between 3 and 20 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Position can only contain English letters.")]
        public string Position { get; set; }



        [Required(ErrorMessage = "Office Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Office Name must be between 3 and 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Office Name can only contain English letters.")]
        public string Office { get; set; }



        [Required(ErrorMessage = "Age is required.")]
        [Range(20, 40, ErrorMessage = "Age must be between 24 and 40.")]
        public int Age { get; set; }



        [Required(ErrorMessage = "Salary is required.")]
        [Range(20000, 150000, ErrorMessage = "Salary must be between 20,000 and 150,000.")]
        public int Salary { get; set; }

    }
}