namespace Restaurant.Domain.Entities
{
    public class BaseEntity<T> where T : struct
    {
        public T Id { get; protected set; }
    }
}
