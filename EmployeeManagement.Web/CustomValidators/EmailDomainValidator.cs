using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; }
        protected override ValidationResult IsValid(object value, 
                                                    ValidationContext validationContext)
        {
            if (value !=null)
            {
                string[] parts = value.ToString().Split("@");
                if (parts.Length > 1 && parts[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null;
                }
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
            return null;
        }
    }
}
