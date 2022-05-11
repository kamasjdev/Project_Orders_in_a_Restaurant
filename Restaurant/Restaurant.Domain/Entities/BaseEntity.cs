using System;

namespace Restaurant.Domain.Entities
{
    public class BaseEntity<T> where T : struct
    {
        protected BaseEntity() { }

        public BaseEntity(T id)
        {
            Id = id;
        }

        public T Id { get; }
    }
}
