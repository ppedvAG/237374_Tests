namespace ppedv.PizzaPizzaPizza.Model
{
    public class Belag : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int KCal { get; set; }
        public virtual ICollection<Pizza> Pizzen { get; set; } = new HashSet<Pizza>();
    }
}
