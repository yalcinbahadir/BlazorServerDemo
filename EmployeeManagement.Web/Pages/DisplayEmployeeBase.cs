using EmployeeManagement.Models;
using EmployeeManagement.Web.Services.EmployeeService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProjectLibrary.Component;
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
        [Parameter]
        public EventCallback<int> OnEmployeeDeleted { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }

        protected ConfirmBase DeleteConfirmation { get; set; }

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

        //protected async Task Delete(MouseEventArgs e)
        //{
        //    var result = await EmployeeService.Delete(Employee);
        //    await EmployeeDeletedEvent.InvokeAsync(result);
        //}
       
        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                var result = await EmployeeService.Delete(Employee);
                await EmployeeDeletedEvent.InvokeAsync(result);
            }
        }
    }
}
