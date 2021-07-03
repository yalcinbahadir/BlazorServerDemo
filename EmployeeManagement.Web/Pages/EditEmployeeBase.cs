using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase :ComponentBase
    {
        public EmployeeModel Model { get; set; } = new EmployeeModel();
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        protected string pageName = "Edit";

        protected override async Task OnInitializedAsync()
        {
            Departments = (await DepartmentService.GetDepartments()).ToList();
            
        }


        protected async override Task OnParametersSetAsync()
        {
            if (int.TryParse(Id, out int id))
            {
                pageName = "Edit";
                Model = MapToModel(await EmployeeService.Get(id));
            } 
            else
            {
                pageName = "Add";
                Model = new EmployeeModel() { DepartmentId = "1", DateOfBrith = DateTime.Now, PhotoPath = "images/nophoto.png" };
            }   
        }

      

        protected async Task HandelValidSubmit()
        {
            if (Model.EmployeeId==0)
            {
                var employee = MapToEmployee(Model);
                var result=await EmployeeService.Add(employee);
                if (result != null)
                {
                    Navigator.NavigateTo("/employeelist");
                }
            }
            else
            {
                var employee = await EmployeeService.Get(Model.EmployeeId);
                MapToEmployee(employee,Model);
                var result=await EmployeeService.Update(employee);
                if (result !=null)
                {
                    Navigator.NavigateTo("/employeelist");
                }
            }
            
        }

        protected async Task Delete()
        {
            var empToDelete = MapToEmployee(Model);
            if (empToDelete !=null)
            {
                var result=await EmployeeService.Delete(empToDelete);
                if (result)
                {
                    Navigator.NavigateTo("/employeelist",true);
                }
            }
            
        }

        private EmployeeModel MapToModel(Employee employee)
        {
            var model = new EmployeeModel();
            model.EmployeeId = employee.EmployeeId;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.DateOfBrith = employee.DateOfBrith;
            model.Email = employee.Email;
            model.Gender = employee.Gender;
            model.PhotoPath = employee.PhotoPath;
            model.DepartmentId = employee.DepartmentId.ToString();
            model.Department.DepartmentName = employee.Department.DepartmentName;
            return model;
        }

        private Employee MapToEmployee(EmployeeModel model)
        {
            var employee = new Employee();
            employee.EmployeeId = model.EmployeeId;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.DateOfBrith = model.DateOfBrith;
            employee.Email = model.Email;
            employee.Gender = model.Gender;
            employee.PhotoPath = model.PhotoPath;
            employee.DepartmentId = int.Parse(model.DepartmentId);
            return employee;
        }
        private Employee MapToEmployee(Employee employee,EmployeeModel model)
        {           
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.DateOfBrith = model.DateOfBrith;
            employee.Email = model.Email;
            employee.Gender = model.Gender;
            employee.PhotoPath = model.PhotoPath;
            employee.DepartmentId = int.Parse(model.DepartmentId);
            return employee;
        }
    }
}
