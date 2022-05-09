using Restaurant.Domain.Entities;
using System.Collections.Generic;

namespace Restaurant.Domain.Repositories
{
    public interface IRepository<I, T>
        where I : struct
        where T : BaseEntity<I>
    {
        I Add(T entity);
        void Update(T entity);
        void Delete(I id);
        T Get(I id);
        ICollection<T> GetAll();
    }
}
