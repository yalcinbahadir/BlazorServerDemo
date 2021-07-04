using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services.EmployeeService
{
    public class DepartmentService : IDepartmentService
    {

        public const string clientName = "blazorClient";
        private IHttpClientFactory _clientFactory;

        public DepartmentService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Department> GetDepartment(int id)
        {

            HttpClient httpClient = _clientFactory.CreateClient(clientName);
            var response = await httpClient.GetAsync($"departments/{id}");
           Department department = new Department();
            if (response.IsSuccessStatusCode)
            {
                department = await response.Content.ReadAsAsync<Department>();
            }
            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            HttpClient httpClient = _clientFactory.CreateClient(clientName);
            var response = await httpClient.GetAsync("departments");
            IEnumerable<Department> departments=new List<Department>();
            if (response.IsSuccessStatusCode)
            {
                departments = await response.Content.ReadAsAsync<IEnumerable<Department>>();
            }
            return departments;
        }
    }
}
