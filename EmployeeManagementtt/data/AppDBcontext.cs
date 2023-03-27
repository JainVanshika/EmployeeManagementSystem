using EmployeeManagementtt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.data
{
    public class AppDBcontext:IdentityDbContext
    {
        public AppDBcontext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
