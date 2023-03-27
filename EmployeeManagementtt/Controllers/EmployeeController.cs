using EmployeeManagementtt.interfaces;
using EmployeeManagementtt.Models;
using EmployeeManagementtt.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementtt.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IemployeeRepo _employeeRepo;
        private readonly IHostingEnvironment hostingEnvironment;
        public EmployeeController(IemployeeRepo employeeRepo, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepo = employeeRepo;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel Model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (Model.PhotoPath != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Model.PhotoPath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Model.PhotoPath.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Gender = Model.Gender,
                    DEPARTMENT = Model.DEPARTMENT,
                    EMAIL = Model.EMAIL,
                    DOB = Model.DOB,
                    MotherName = Model.MotherName,
                    FatherName = Model.FatherName,
                    PhoneNo = Model.PhoneNo,
                    Country = Model.Country,
                    State = Model.State,
                    City = Model.City,
                    Address = Model.Address,
                    PhotoPath = uniqueFileName,
                };
                _employeeRepo.Add(newEmployee);
                return RedirectToAction("AllList", "Employee");
            }
            return View();
        }
        public IActionResult List(int ID)
        {
            var data = _employeeRepo.GetEmployee(ID);
            return View(data);
        }
        public IActionResult AllList()
        {
            var data = _employeeRepo.GetallEmployee();
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            Employee obj = _employeeRepo.GetEmployee(ID);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                ID=obj.ID,
                FirstName=obj.FirstName,
                LastName=obj.LastName,
                Gender=obj.Gender,
                DEPARTMENT=obj.DEPARTMENT,
                EMAIL=obj.EMAIL,
                DOB=obj.DOB,
                PhoneNo=obj.PhoneNo,
                FatherName=obj.FatherName,
                MotherName=obj.MotherName,
                Country=obj.Country,
                State=obj.State,
                City=obj.City,
                Address=obj.Address,
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                Employee employee = _employeeRepo.GetEmployee(model.ID);
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Gender = model.Gender;
                employee.DEPARTMENT = model.DEPARTMENT;
                employee.EMAIL = model.EMAIL;
                employee.DOB = model.DOB;
                employee.PhoneNo = model.PhoneNo;
                employee.FatherName = model.FatherName;
                employee.MotherName = model.MotherName;
                employee.Country = model.Country;
                employee.State = model.State;
                employee.City = model.City;
                employee.Address = model.Address;

                Employee updatedEmployee = _employeeRepo.Update(employee);
                return RedirectToAction("AllList", "Employee");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int ID)
        {
            var de = _employeeRepo.GetEmployee(ID);
            _employeeRepo.Delete(ID);
            return RedirectToAction("AllList", "Employee");
        }

    }
}
