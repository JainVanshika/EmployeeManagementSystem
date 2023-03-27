using EmployeeManagementtt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.interfaces
{
    public interface IemployeeRepo
    {
        Employee GetEmployee(int ID);
        Employee Add(Employee emp);
        IEnumerable<Employee> GetallEmployee();
        Employee Update(Employee EmployeeChanges);
        Employee Delete(int ID);
    }

}
