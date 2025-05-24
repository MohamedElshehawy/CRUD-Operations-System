using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Lazy<IDepartmentRepository> departments;
        private readonly Lazy<IEmployeeRepository> employees;
        private readonly CompanyDbContext _context;

        public UnitOfWork(CompanyDbContext context)
        {
            departments = new Lazy<IDepartmentRepository>(new DepartmentRepository(context));
            employees = new Lazy<IEmployeeRepository>(new EmployeeRepository(context));
            _context = context;
        }
        public IDepartmentRepository Departments => departments.Value ; 

        public IEmployeeRepository Employees => employees.Value ;

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();



    }
}
