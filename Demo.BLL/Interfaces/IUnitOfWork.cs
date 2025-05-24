using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }
        public Task<int> CompleteAsync();
    }
}
