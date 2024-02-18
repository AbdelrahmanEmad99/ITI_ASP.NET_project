
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sunrise.Models
{
    public class Employee
    {
        public int Id { get; set; }

        //[Display(Name= ("Name")]
        [DisplayName("Name")]
        [Required(ErrorMessage ="You have to provide a valid full name.")]
        [MinLength(12, ErrorMessage ="Full name mustn't be less than 12 characters.")]
        [MaxLength(70, ErrorMessage ="Full name mustn't exceepd 70 characters.")]
        public string FullName { get; set; }

        //[Display(Name="Occupation")]
        [DisplayName("Occupation")]
        [Required(ErrorMessage ="You have to provide a valid position.")]
        [MinLength(2, ErrorMessage = "Positin mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Position mustn't exceed 20 characters.")]
        public string Position { get; set; }

        [DisplayName("Monthly Salary")]
        [Required(ErrorMessage ="You have to provide a valid salary.")]
        //[Range(5500, 55000, ErrorMessage = "Salary must be between 5500 EGP and 55000 EGP.")]
        public decimal Salary { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Date of Join")]
        public DateTime JoinDate { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }

        //Foreign Key Property
        [Range(1, int.MaxValue, ErrorMessage="Choose a valid department.")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        //Navigation Property
        [ValidateNever]
        public Department Department { get; set; }
    }
}

