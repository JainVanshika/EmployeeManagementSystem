using EmployeeManagementtt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.ViewModel
{
    public class EmployeeEditViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public enum2 Gender { get; set; }
        public dept DEPARTMENT { get; set; }

        public string EMAIL { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }
        public string PhoneNo { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
