using HTTPServer.Core.Abstractions;
using HTTPServer.Core.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Repositories
{
    public class PeopleRepository
        : IRepository<int, People>
    {
        List<People> _peoples;

        public PeopleRepository()
        {
            _peoples = new List<People> { new People { Id = 0, Name = "Vanya" }, new People { Id = 1, Name = "Vasya" } };
        }

        public void Create(People item)
            => _peoples.Add(item);

        public void Delete(int id)
            => _peoples.RemoveAt(id);

        public People Get(int id)
            => _peoples.ElementAt(id);

        public IEnumerable<People> GetList()
            => _peoples.AsEnumerable();

        public void Save()
            => throw new NotSupportedException();

        public void Update(People item)
            => _peoples.ElementAt(item.Id).Name = item.Name;
    }
}
