using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services.EmployeeService
{
    public interface IEmployeeService 
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> Get(int id);
        Task<Employee> Add(Employee entity);
        Task<Employee> Update(Employee entity);
        Task<bool> Delete(Employee entity);
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);

    }
}
