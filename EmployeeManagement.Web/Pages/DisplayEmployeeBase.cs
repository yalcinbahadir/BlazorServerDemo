using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Parameter]
        public Employee Employee { get; set; }
        [Parameter]
        public bool ShowFooter { get; set; }
        [Parameter]
        public EventCallback<bool> ChildCheckBoxEvent { get; set; }
        [Parameter]
        public EventCallback<bool> EmployeeDeletedEvent { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }
        private bool isChecked;
        protected async Task HandelCheckBoxEvent(ChangeEventArgs e)
        {
            isChecked=(bool)e.Value;
            await ChildCheckBoxEvent.InvokeAsync(isChecked);
        }


        protected string GetDetailsUrl(int id)
        {
           return $"/employeedetails/{id}";
        }

        //protected async Task Delete()
        //{
        //    var result = await EmployeeService.Delete(Employee);
        //    if (result)
        //    {
        //        Navigator.NavigateTo("/", true);
        //    }
        //}
        protected async Task Delete(MouseEventArgs e)
        {
            var result = await EmployeeService.Delete(Employee);
            await EmployeeDeletedEvent.InvokeAsync(result);
        }
    }
}
