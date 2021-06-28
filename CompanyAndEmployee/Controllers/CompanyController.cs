using DataLayer.DbConnection;
using DataLayer.Entities;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace CompanyAndEmployee.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly ApplicationDbContext _dbContext;

        public CompanyController(IGenericRepository<Company> companyRepository, ApplicationDbContext dbContext)
        {
            _companyRepository = companyRepository;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var companies = _companyRepository.GetAll();
            return View(companies);
        }

        public ViewResult Details(int id)
        {
            Company company = _companyRepository.GetById(id);
            ViewBag.Message = company.Size.ToString();
            return View(company);
        }

        public IActionResult Create()
        {
            return View(new Company());
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid && IsUnique(company))
            {
                ViewBag.Message = "Exist";
                return View(company);
            }

            else
            {
                _companyRepository.Add(company);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            Company company = _companyRepository.GetById(id);
            return View(company);
        }

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _companyRepository.Edit(company);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }

            return View(company);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }

            Company company = _companyRepository.GetById(id);
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            _companyRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public bool IsUnique(Company company)
        {
            return _dbContext.Companies.Any(c => c.Name == company.Name);
        }
    }
}
