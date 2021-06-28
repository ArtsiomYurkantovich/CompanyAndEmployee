using DataLayer.DbConnection;
using DataLayer.Entities;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace CompanyAndEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository<Employee> _employeRepository;
        private readonly ApplicationDbContext _db;

        public EmployeeController(IGenericRepository<Employee> employeRepository, ApplicationDbContext db)
        {
            _db = db;
            _employeRepository = employeRepository;
        }

        public IActionResult Index()
        {
            var employees = _db.Employees.Include(empl => empl.Company);
            return View(employees);
        }

        public ViewResult Details(int id)
        {
            Employee employee = _employeRepository.GetById(id);
            ViewBag.NameCompany = CompanyName(employee);
            return View(employee);
        }

        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_db.Companies, "Id", "Name");
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeRepository.Add(employee);
                    CountEmployeeAdd(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }

            return View(employee);
        }
        public ActionResult Edit(int id)
        {
            ViewData["CompanyId"] = new SelectList(_db.Companies, "Id", "Name");

            Employee employee = _employeRepository.GetById(id);
            CountEmployeeDelete(employee);
            UpdateDate(employee);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeRepository.Edit(employee);
                    CountEmployeeAdd(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            return View(employee);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            Employee employee = _employeRepository.GetById(id);
            ViewBag.NameCompany = CompanyName(employee);
            CountEmployeeDelete(employee);

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            _employeRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public void CountEmployeeAdd(Employee employee)
        {
            var id = employee.CompanyId;
            var company = _db.Companies.Find(id);
            company.Size++;
            _db.Entry(company).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void CountEmployeeDelete(Employee employee)
        {
            var id = employee.CompanyId;
            var company = _db.Companies.Find(id);
            company.Size--;
            _db.Entry(company).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateDate(Employee employee)
        {
            employee.GetJob = DateTime.Now;
            _db.Entry(employee).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public string CompanyName(Employee employee)
        {
            var id = employee.CompanyId;
            var company = _db.Companies.Find(id);
            var nameCompany = company.Name;

            return nameCompany;
        }
    }
}
