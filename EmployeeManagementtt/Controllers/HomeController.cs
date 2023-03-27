using EmployeeManagementtt.Models;
using EmployeeManagementtt.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementtt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetEmployee(int id)
        {
            EmployeeApiViewModel employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.168:82/api/employees/" + id);
                //HTTP GET
                var responseTask = client.GetAsync("http://192.168.1.168:82/api/employees/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeApiViewModel>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else  
                {

                    //employees = Enumerable.Empty<EmployeeApiViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }
        public IActionResult GetEmployees()
        {
            IEnumerable<EmployeeApiViewModel> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.168:82/api/employees");
                //HTTP GET
                var responseTask = client.GetAsync("http://192.168.1.168:82/api/employees");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeApiViewModel>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    employees = Enumerable.Empty<EmployeeApiViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employees);
        }
        public IActionResult CreateAPI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAPI(EmployeeApiViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.168:82/api/employees");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmployeeApiViewModel>("http://192.168.1.168:82/api/employees", employee);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetEmployees");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(employee);
        }
        public IActionResult EditAPI(int id)
        {
            EmployeeApiViewModel employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53377/api/employees");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:53377/api/employees/" +id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeApiViewModel>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult EditAPI(EmployeeApiViewModel employee)
        {
            int id = employee.EmployeeID;
            using(var client=new HttpClient())
            {
                client.BaseAddress=new Uri("http://localhost:53377/api/employees");

                var putTask = client.PutAsJsonAsync<EmployeeApiViewModel>("http://localhost:53377/api/employees/"+id, employee);
                putTask.Wait();
                var result = putTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetEmployees");
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(employee);
            }
        }

        public IActionResult DeleteAPI(int id)
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53377/api/employees");
                var deleteTask = client.DeleteAsync("http://localhost:53377/api/employees/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetEmployees");
                }
            }
            return RedirectToAction("GetEmployees");
        }

        public IActionResult Paypal()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
