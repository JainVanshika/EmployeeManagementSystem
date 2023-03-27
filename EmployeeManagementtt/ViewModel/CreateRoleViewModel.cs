using EmployeeManagementtt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public String RoleName { get; set; }

    }
}
