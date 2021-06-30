using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repositories.Abstract
{
    public interface IEmployeeRepository :IRepository<Employee>
    {
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);
        Task<Employee> GetByEmail(string email);
    }
}
