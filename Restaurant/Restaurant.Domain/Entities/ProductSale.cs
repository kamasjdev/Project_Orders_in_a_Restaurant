using System;

namespace Restaurant.Domain.Entities
{
    public class ProductSale : BaseEntity<Guid>
    {
        private ProductSale()
        {
        }

        public ProductSale(Guid id, Product product, Addition addition = null)
            : base(id)
        {
            ChangeProduct(product);

            if (addition != null)
            {
                ChangeAddition(addition);
            }
        }

        public Product Product { get; private set; }
        public Addition Addition { get; private set; } = null;
        public decimal EndPrice { get; private set; } = decimal.Zero;

        public void ChangeProduct(Product product)
        {
            if (product is null)
            {
                throw new InvalidOperationException("Cannot add null product");
            }

            if (Product != null)
            {
                EndPrice -= Product.Price;
            }

            Product = product;
            EndPrice += product.Price;
        }

        public void ChangeAddition(Addition addition)
        {
            if (addition is null)
            {
                throw new InvalidOperationException("Cannot add null addition");
            }

            if (Addition != null)
            {
                EndPrice -= Addition.Price;
            }

            Addition = addition;
            EndPrice += addition.Price;
        }

        public void RemoveAddion()
        {
            if (Addition is null)
            {
                throw new InvalidOperationException("There is no addition in product");
            }

            EndPrice -= Addition.Price;
            Addition = null;
        }
    }
}
