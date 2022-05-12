using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Domain.Repositories
{
    public interface IAdditonRepository : IRepository<Guid, Addition>
    {
    }
}
