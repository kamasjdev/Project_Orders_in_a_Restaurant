using Restaurant.ApplicationLogic.DTO;
using System;

namespace Restaurant.ApplicationLogic.Interfaces
{
    public interface IProductSaleService : IService
    {
        Guid Add(ProductSaleDto productSaleDto);
        void Update(ProductSaleDto productSaleDto);
    }
}
