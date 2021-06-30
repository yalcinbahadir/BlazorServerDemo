using EmployeeManagement.API.Repositories.Abstract;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _repository.GetAll();
                if (!employees.Any())
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data from the database. "+ ex.Message);
            }
            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _repository.Get(id);
                if (employee ==null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                var existing = await _repository.GetByEmail(employee.Email);
                if (existing !=null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var result = await _repository.Add(employee);
                return CreatedAtAction(nameof(GetEmployee), new {id=result.EmployeeId }, result);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding resource to the database. " + ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _repository.Get(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var isDeleted=await _repository.Delete(employee);
                if (isDeleted)
                {
                    return Ok(employee);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data. " + ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                if (id != employee.EmployeeId)
                {
                    return BadRequest("Ids mismatch.");
                }

                var existing = await _repository.Get(employee.EmployeeId);
                if (existing == null)
                {
                    return BadRequest("Not found.");
                }

                var result = await _repository.Update(employee);
                if (result ==null)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating resource. " + ex.Message);
            }

        }

        //[HttpGet("search/{name}/{gender}")] //https://localhost:44368/api/employees/search/bahadir-edit/1
        [HttpGet("search")] //https://localhost:44368/api/employees/search?name=bahadir-edit&gender=1
        public async Task<ActionResult<Employee>> Search(string name, Gender? gender)
        {
            try
            {
                var employees = await _repository.Search(name,gender);
                if (!employees.Any())
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database. " + ex.Message);
            }

        }

    }
}
