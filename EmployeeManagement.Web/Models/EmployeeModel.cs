using EmployeeManagement.Models;
using EmployeeManagement.Web.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "blazor.com", ErrorMessage = "Allowed domain is blazor.com")]
        public string Email { get; set; }
        [CompareProperty("Email")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBrith { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        [ValidateComplexType]
        public Department Department { get; set; } = new Department();
        public string PhotoPath { get; set; }
    }
}
