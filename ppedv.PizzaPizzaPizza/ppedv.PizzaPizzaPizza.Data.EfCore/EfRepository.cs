using ppedv.PizzaPizzaPizza.Model;
using ppedv.PizzaPizzaPizza.Model.Contracts;

namespace ppedv.PizzaPizzaPizza.Data.EfCore
{
    public class EfRepository : IRepository
    {
        private readonly PizzaContext pizzaContext;

        public EfRepository(string conString)
        {
            pizzaContext = new PizzaContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            pizzaContext.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            pizzaContext.Remove<T>(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return pizzaContext.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return pizzaContext.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            pizzaContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            pizzaContext.Update<T>(entity);
        }
    }
}
