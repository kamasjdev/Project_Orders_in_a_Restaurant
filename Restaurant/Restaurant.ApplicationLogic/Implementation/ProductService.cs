using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.ApplicationLogic.DTO;
using System;
using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.Implementation
{
    internal class ProductService : IProductService
    {
        public Guid Add(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public ProductDto Get(Guid id)
        {
            return new ProductDto();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
