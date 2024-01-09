using ppedv.PizzaPizzaPizza.Model;
using ppedv.PizzaPizzaPizza.Model.Contracts;

namespace ppedv.PizzaPizzaPizza.Logic
{
    public class PizzaService
    {
        private IRepository repo;
        public PizzaService(IRepository repo)
        {
            this.repo = repo;
        }

        public Pizza GetPizzaWithMostKCal()
        {
            return repo.GetAll<Pizza>().OrderBy(x => CalcKCal(x)).FirstOrDefault();
        }

        public int CalcKCal(Pizza pizza)
        {
            ArgumentNullException.ThrowIfNull(pizza);

            return pizza.Belaege.Sum(x => x.KCal);
        }
    }
}
