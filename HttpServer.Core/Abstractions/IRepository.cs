using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer.Core.Abstractions
{
    public interface IRepository<TUid, TModel>
    {
        IEnumerable<TModel> GetList();
        TModel Get(TUid id);
        void Create(TModel item);
        void Update(TModel item);
        void Delete(TUid id);
        void Save();
    }
}
