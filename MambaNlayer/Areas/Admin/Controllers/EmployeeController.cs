using Mamba.Bussiness.Exceptions;
using Mamba.Bussiness.Services.Abstratcs;
using Mamba.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MambaNlayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees=_employeeService.GetAllEmployee();
            return View(employees);
        }
        public IActionResult Create () {
        return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _employeeService.AddEmployee(employee);
            }
            catch(FileContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var existEmployee=_employeeService.GetEmployee(x=>x.Id == id);
            if (existEmployee == null) return NotFound();
            _employeeService.RemoveEmployee(existEmployee.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var oldEmployee= _employeeService.GetEmployee(x=>x.Id==id); 
            if (oldEmployee == null) return NotFound();
            return View(oldEmployee);


        }
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            if (!ModelState.IsValid) return View();
            _employeeService.Update(employee.Id, employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
