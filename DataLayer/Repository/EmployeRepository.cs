using DataLayer.DbConnection;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public class EmployeRepository : IGenericRepository<Employee>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Employee> employees;
        private readonly DbSet<Company> companies;

        public EmployeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            employees = _dbContext.Employees;
            companies = _dbContext.Companies;
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public Employee GetById(int id)
        {
            return employees.Find(id);
        }

        public void Add(Employee entity)
        {
            employees.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToRemove = employees.Find(id);
            employees.Remove(entityToRemove);
            _dbContext.SaveChanges();
        }

        public void Edit(Employee entity)
        {
            employees.Update(entity);
            _dbContext.SaveChanges();
        }

        public void CountUpd(Employee employee)
        {
            var id = employee.CompanyId;
            var company = companies.Find(id);
            company.Size++;
            _dbContext.Entry(company).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void CountUpdMinus(Employee employee)
        {
            var id = employee.CompanyId;
            var company = companies.Find(id);
            company.Size--;
            _dbContext.Entry(company).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
