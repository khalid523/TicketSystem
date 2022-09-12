using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketinDataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        void Delete(int? Id);
        void Deletet(T obj);
        void Insert(T obj);
        T Load(int Id);
        List<T> LoadAll();
        void Update(T obj);
        void Deletetdfd(IEnumerable<T> obj);

    }
}
