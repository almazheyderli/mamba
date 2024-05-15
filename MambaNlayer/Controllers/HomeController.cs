using Mamba.Bussiness.Services.Abstratcs;
using MambaNlayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MambaNlayer.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees=_employeeService.GetAllEmployee();
            return View(employees);
        }

    
    }
}
