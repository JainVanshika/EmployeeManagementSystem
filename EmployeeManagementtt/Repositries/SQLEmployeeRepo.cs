using EmployeeManagementtt.data;
using EmployeeManagementtt.interfaces;
using EmployeeManagementtt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.Repositries
{
    public class SQLEmployeeRepo : IemployeeRepo
    {
        private readonly AppDBcontext _context;
        public SQLEmployeeRepo(AppDBcontext context)
        {
            _context = context;
        }
        public Employee Add(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return emp;
        }

        public Employee Delete(int ID)
        {
            Employee employee = _context.Employees.Find(ID);
            if(employee!=null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetallEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int ID)
        {
            return _context.Employees.Find(ID);
        }

        public Employee Update(Employee EmployeeChanges)
        {
            var employee = _context.Employees.Attach(EmployeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return EmployeeChanges;
        }
    }
}
