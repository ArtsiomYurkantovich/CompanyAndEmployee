using DataLayer.DbConnection;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repository
{
    public class CompanyRepository : IGenericRepository<Company>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Company> companies;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            companies = _dbContext.Companies;
        }

        public IEnumerable<Company> GetAll()
        {
            return companies.ToList();
        }

        public Company GetById(int id)
        {
            return companies.Find(id);
        }

        public void Add(Company entity)
        {
            companies.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToRemove = companies.Find(id);
            companies.Remove(entityToRemove);
            _dbContext.SaveChanges();
        }

        public void Edit(Company entity)
        {
            companies.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
