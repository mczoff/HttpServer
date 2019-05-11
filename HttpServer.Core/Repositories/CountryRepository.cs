using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Repositories
{
    public class CountryRepository
        : IRepository<int, Country>
    {
        List<Country> _countries;

        public CountryRepository()
        {
            _countries = new List<Country> { new Country { Id = 0, Name = "Russian" }, new Country { Id = 1, Name = "Ukraine" } };
        }

        public void Create(Country item)
            => _countries.Add(item);

        public void Delete(int id)
            => _countries.RemoveAt(id);

        public Country Get(int id)
            => _countries.ElementAt(id);

        public IEnumerable<Country> GetList()
            => _countries.AsEnumerable();

        public void Save()
            => throw new NotSupportedException();

        public void Update(Country item)
            => _countries.ElementAt(item.Id).Name = item.Name;
    }
}
