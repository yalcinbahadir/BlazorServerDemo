using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase :ComponentBase
    {
        protected IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public bool ShowFooter { get; set; } = true;
        public int SelectedEmployesCount { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetAll();
        }


        protected void FireParentMethod(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployesCount++;
            } 
            else
            {
                SelectedEmployesCount--;
            }
        }

        protected async Task HandelDelete(bool isDeleted)
        {
            if (isDeleted)
            {
                Employees = await EmployeeService.GetAll();
            }

        }







        #region Hardcoded data
        //private void LoadEmployees()
        //{
        //    System.Threading.Thread.Sleep(3000);
        //    Employee e1 = new Employee
        //    {
        //        EmployeeId = 1,
        //        FirstName = "John",
        //        LastName = "Hastings",
        //        Email = "David@pragimtech.com",
        //        DateOfBrith = new DateTime(1980, 10, 5),
        //        Gender = Gender.Male,
        //        Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
        //        PhotoPath = "images/john.png"
        //    };

        //    Employee e2 = new Employee
        //    {
        //        EmployeeId = 2,
        //        FirstName = "Sam",
        //        LastName = "Galloway",
        //        Email = "Sam@pragimtech.com",
        //        DateOfBrith = new DateTime(1981, 12, 22),
        //        Gender = Gender.Male,
        //        Department = new Department { DepartmentId = 2, DepartmentName = "HR" },
        //        PhotoPath = "images/sam.jpg"
        //    };

        //    Employee e3 = new Employee
        //    {
        //        EmployeeId = 3,
        //        FirstName = "Mary",
        //        LastName = "Smith",
        //        Email = "mary@pragimtech.com",
        //        DateOfBrith = new DateTime(1979, 11, 11),
        //        Gender = Gender.Female,
        //        Department = new Department { DepartmentId = 1, DepartmentName = "IT" },
        //        PhotoPath = "images/mary.png"
        //    };

        //    Employee e4 = new Employee
        //    {
        //        EmployeeId = 3,
        //        FirstName = "Sara",
        //        LastName = "Longway",
        //        Email = "sara@pragimtech.com",
        //        DateOfBrith = new DateTime(1982, 9, 23),
        //        Gender = Gender.Female,
        //        Department = new Department { DepartmentId = 3, DepartmentName = "Payroll" },
        //        PhotoPath = "images/sara.png"
        //    };

        //    Employees = new List<Employee> { e1, e2, e3, e4 };

        //}
        #endregion
    }
}
