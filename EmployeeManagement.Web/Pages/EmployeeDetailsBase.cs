using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }

        protected string Coordiates { get; set; }

        protected string photoPath { get; set; }
        protected string[] photoPaths { get; set; }
        protected int index = 0;
        protected Employee SelectedEmployee = new Employee();
        protected List<Employee> Employees = new List<Employee>();
        protected bool show;
        protected string isvisible= "invisible";
        protected string btnText = "Show";
        protected async override Task OnParametersSetAsync()
        {

            if (int.TryParse(Id, out int id))
            {
                Employee = (await EmployeeService.Get(id));
                SelectedEmployee = Employee;
                photoPath = Employee.PhotoPath;
                photoPaths =new string[]{ "images/sara.png", "images/mary.png", "images/sam.jpg", "images/john.png" };
                Employees = (await EmployeeService.GetAll()).ToList();
            }
            else
            {
                Navigator.NavigateTo("/");
            }
        }

        
    }
}
