using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public const string clientName = "blazorClient";
        private IHttpClientFactory _clientFactory;

        public EmployeeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }




        public async Task<IEnumerable<Employee>> GetAll()
        {
            HttpClient httpClient = _clientFactory.CreateClient(clientName);
            var response=await httpClient.GetAsync("employees");
            IList<Employee> employees = new List<Employee>();
            if (response.IsSuccessStatusCode)
            {
                employees = response.Content.ReadAsAsync<IList<Employee>>().Result;
            }
            return employees;
        }

        public async Task<Employee> Get(int id)
        {
                HttpClient httpClient = _clientFactory.CreateClient(clientName);
                var response = await httpClient.GetAsync($"employees/{id}");
                Employee employee = new Employee();
                if (response.IsSuccessStatusCode)
                {
                    employee = response.Content.ReadAsAsync<Employee>().Result;
                }
                return employee;
        }


        public async Task<Employee> Add(Employee entity)
        {
            HttpClient httpClient = _clientFactory.CreateClient(clientName);
            var response = await httpClient.PostAsJsonAsync<Employee>($"employees", entity);
            Employee employee = new Employee();
            if (response.IsSuccessStatusCode)
            {
                employee = response.Content.ReadAsAsync<Employee>().Result;
            }
            return employee;
        }
        public Task<bool> Delete(Employee entity)
        {
            throw new NotImplementedException();
        }




        public Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> Update(Employee entity)
        {
            HttpClient httpClient = _clientFactory.CreateClient(clientName);
            var response = await httpClient.PutAsJsonAsync<Employee>($"employees/{entity.EmployeeId}", entity);
            Employee employee = new Employee();
            if (response.IsSuccessStatusCode)
            {
                employee = response.Content.ReadAsAsync<Employee>().Result;
            }
            return employee;
        }


    }
}
