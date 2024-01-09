using ppedv.PizzaPizzaPizza.Model;

namespace ppedv.PizzaPizzaPizza.Logic
{
    public class PizzaService
    {
        public int CalcKCal(Pizza pizza)
        {
            ArgumentNullException.ThrowIfNull(pizza);

            return pizza.Belaege.Sum(x => x.KCal);
        }
    }
}
