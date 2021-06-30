using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
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
    }
}
