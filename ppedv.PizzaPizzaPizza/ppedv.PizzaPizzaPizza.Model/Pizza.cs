namespace ppedv.PizzaPizzaPizza.Model
{
    public class Pizza : Entity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Preis { get; set; }
        public virtual ICollection<Belag> Belaege { get; set; } = new HashSet<Belag>();
    }

}
