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

        public Pizza? GetPizzaWithMostKCal()
        {
            return repo.GetAll<Pizza>().Where(x => x.Belaege.Any())
                                       .OrderByDescending(x => CalcKCal(x))
                                       .ThenBy(x => x.Belaege.Count())
                                       .ThenBy(x => x.Name)
                                       .FirstOrDefault();
        }

        public int CalcKCal(Pizza pizza)
        {
            ArgumentNullException.ThrowIfNull(pizza);

            return pizza.Belaege.Sum(x => x.KCal);
        }
    }
}
