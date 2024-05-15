using Mamba.Bussiness.Exceptions;
using Mamba.Bussiness.Services.Abstratcs;
using Mamba.Core.Models;
using Mamba.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Bussiness.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeService(IEmployeeRepository employeeRepository,IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _employeeRepository = employeeRepository;
        }


        //public void AddEmployee(Employee employee)
        //{
        //    if (!employee.ImgFile.ContentType.Contains("image?")) throw new FileContentException("ImgFile", "duzgun format daxil edin");
        //    if (employee.ImgFile.Length > 2097125) throw new FileSizeException("ImgFile", "sekil olcusu cox boyukdur");
        //    string path=_webHostEnvironment.WebRootPath + @"/Upload/Service" + employee.ImgFile.FileName;
        //    using (FileStream stream = new FileStream(path, FileMode.Create))
        //    {
        //        employee.ImgFile.CopyTo(stream);
        //    }
        //    employee.ImgUrl = employee.ImgFile.FileName;
        //    _employeeRepository.Add(employee);
        //    _employeeRepository.Commit();
        //}
        public void AddEmployee(Employee employee)
        {
            if (employee.ImgFile == null || employee.ImgFile.Length == 0)
            {
                throw new ArgumentNullException("ImgFile", "Dosya boş veya geçersiz.");
            }

            if (!employee.ImgFile.ContentType.Contains("image"))
            {
                throw new FileContentException("ImgFile", "Dosya tipi geçersiz.");
            }

            if (employee.ImgFile.Length > 2097125)
            {
                throw new FileSizeException("ImgFile", "Dosya boyutu çok büyük.");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(employee.ImgFile.FileName);
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "Service", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                employee.ImgFile.CopyTo(stream);
            }

            employee.ImgUrl = fileName;

            _employeeRepository.Add(employee);
            _employeeRepository.Commit();
        }


        public List<Employee> GetAllEmployee(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.GetAll(func);
        }

        public Employee GetEmployee(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.Get(func);
        }

        public void RemoveEmployee(int id)
        {
            var employee = _employeeRepository.Get(x => x.Id == id);
            if (employee == null) throw new Exception();
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Service\" + employee.ImgUrl;
            if (!File.Exists(path)) throw new FileNameNotFoundException("ImgUrl", "File not found");

            File.Delete(path);
            _employeeRepository.Delete(employee);
            _employeeRepository.Commit();


        }

        public void Update(int id, Employee employee)
        {
            var oldEmployee=_employeeRepository.Get(x=> x.Id== id);
            if(oldEmployee== null) throw new NullReferenceException();
            if (employee.ImgFile != null)
            {
                string filename = employee.ImgFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Service\" + employee.ImgFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    employee.ImgFile.CopyTo(stream);
                }

                FileInfo fileInfo = new FileInfo(path+oldEmployee.ImgUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                oldEmployee.ImgUrl = filename;
            }
            oldEmployee.FullName=employee.FullName;
            oldEmployee.Description = employee.Description;
            _employeeRepository.Commit();

        }
    }
}
