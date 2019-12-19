using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HTTPServer.Core.Contexts;

namespace HTTPServer.Core.Repositories
{
    public class CountryRepository
        : IRepository<int, Country>
    {
        private DatabaseContext _databaseContext;

        public CountryRepository()
        {
            _databaseContext = new DatabaseContext();
        }

        public void Create(Country item)
            => _databaseContext.Countries.Add(item);

        public void Delete(int id)
            => _databaseContext.Countries.Remove(new Country { Id = id });

        public Country Get(int id)
            => _databaseContext.Countries.Find(id);

        public IEnumerable<Country> GetList()
            => _databaseContext.Countries.ToList();

        public void Save()
            => _databaseContext.SaveChanges();

        public void Update(Country item)
            => _databaseContext.Countries.ElementAt(item.Id).Name = item.Name;
    }
}
