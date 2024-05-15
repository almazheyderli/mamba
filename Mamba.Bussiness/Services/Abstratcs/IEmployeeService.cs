using Mamba.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Bussiness.Services.Abstratcs
{
    public  interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        void RemoveEmployee(int id);
        void Update(int id,Employee employee);
        Employee GetEmployee(Func<Employee, bool>? func=null);
        List<Employee> GetAllEmployee(Func<Employee,bool>? func=null );
    }
}
