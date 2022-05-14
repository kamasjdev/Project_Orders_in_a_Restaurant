using Restaurant.ApplicationLogic.DTO;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.Domain.Repositories;
using System;

namespace Restaurant.ApplicationLogic.Implementation
{
    internal class ProductSaleService : IProductSaleService
    {
        private readonly IProductSaleRepository _productSaleRepository;

        public ProductSaleService(IProductSaleRepository productSaleRepository)
        {
            _productSaleRepository = productSaleRepository;
        }

        public Guid Add(ProductSaleDto productSaleDto)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductSaleDto productSaleDto)
        {
            throw new NotImplementedException();
        }
    }
}
