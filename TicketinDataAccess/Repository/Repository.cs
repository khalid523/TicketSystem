using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketinsystems.data;

namespace TicketinDataAccess.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        TicketinContext db = new TicketinContext();
        public void Delete(int? Id)
        {
            T obj = db.Set<T>().Find(Id);
            db.Set<T>().Remove(obj);
            db.SaveChanges();
        }

        public void Deletet(T obj)
        {
           
            db.Set<T>().Remove(obj);
            db.SaveChanges();

        }

        public void Deletetdfd(IEnumerable<T> obj)
        {

            db.Set<T>().RemoveRange(obj);
            db.SaveChanges();

        }
        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }

        public T Load(int Id)
        {
            return db.Set<T>().Find(Id);
        }

        public List<T> LoadAll()
        {
            return db.Set<T>().ToList();
        }

        public void Update(T obj)
        {
            db.Set<T>().Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }


    }
}
