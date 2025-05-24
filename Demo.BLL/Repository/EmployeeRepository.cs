using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {

        }
       
        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> expression)
        {
            return await _context.Employees.Include(e=>e.Department).Where(expression).ToListAsync();

        }

        public Task<IEnumerable<Employee>> GetAllAsync(string Address)
        {
            throw new NotImplementedException();
        }
    }
}
