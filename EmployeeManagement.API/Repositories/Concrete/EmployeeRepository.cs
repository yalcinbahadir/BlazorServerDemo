using EmployeeManagement.API.Data;
using EmployeeManagement.API.Repositories.Abstract;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Repositories.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
           
        }

        public async Task<Employee> Get(int id)
        {
            return await _context.Employees.Include(e=>e.Department).FirstOrDefaultAsync(e=>e.EmployeeId==id);
        }

        public async Task<Employee> GetByEmail(string email)
        {
            return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> Add(Employee entity)
        {
            var result = await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(Employee entity)
        {
            var result = await Get(entity.EmployeeId);
            if (result!=null)
            {
                _context.Employees.Remove(result);              
            }

            return await _context.SaveChangesAsync()>0;
        }

        public async Task<Employee> Update(Employee entity)
        {
            var empToUpdate = await Get(entity.EmployeeId);
            if (empToUpdate !=null)
            {
                empToUpdate.FirstName = entity.FirstName;
                empToUpdate.LastName = entity.LastName;
                empToUpdate.DateOfBrith = entity.DateOfBrith;
                empToUpdate.Gender = entity.Gender;
                empToUpdate.Email = entity.Email;
                empToUpdate.DepartmentId = entity.DepartmentId;
                await _context.SaveChangesAsync();
            }

            return empToUpdate;
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            var query = _context.Employees.Include(e => e.Department).AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(
                                    e => e.FirstName.ToUpper().Contains(name.ToUpper()) ||
                                         e.LastName.ToUpper().Contains(name.ToUpper())
                                   );
            }

            if (gender !=null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }


    }
}
