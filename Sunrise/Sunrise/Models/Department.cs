using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Sunrise.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Property
        [ValidateNever]
        public List<Employee> Employees { get; set; }
    }
}
