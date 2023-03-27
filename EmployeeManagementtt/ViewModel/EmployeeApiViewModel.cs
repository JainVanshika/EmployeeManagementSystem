using EmployeeManagementtt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.ViewModel
{
    public class EmployeeApiViewModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentID { get; set; }
        public string PhotoPath { get; set; }
    }
}
