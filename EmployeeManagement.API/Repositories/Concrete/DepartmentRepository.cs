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
    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Department> Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Department entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> Get(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d=>d.DepartmentId==id);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public Task<Department> Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
