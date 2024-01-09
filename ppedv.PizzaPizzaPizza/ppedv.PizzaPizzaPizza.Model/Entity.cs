namespace ppedv.PizzaPizzaPizza.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }

}
