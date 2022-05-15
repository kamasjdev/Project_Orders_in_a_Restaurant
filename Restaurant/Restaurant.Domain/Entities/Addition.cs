using Restaurant.Domain.Exceptions;
using System;

namespace Restaurant.Domain.Entities
{
    public class Addition : BaseEntity<Guid>
    {
        private Addition()
        {
        }

        public Addition(Guid id, string name, decimal price, ProductKind additionKind) : base(id)
        {
            ChangeAdditionName(name);
            ChangePrice(price);
            ProductKind = additionKind;
        }

        public string AdditionName { get; private set; }
        public decimal Price { get; private set; }
        public ProductKind ProductKind { get; set; }

        public void ChangeAdditionName(string additionName)
        {
            if (string.IsNullOrWhiteSpace(additionName))
            {
                throw new RestaurantException("AdditionName cannot be empty", typeof(Addition).FullName, "AdditionName");
            }

            if (additionName.Length < 3)
            {
                throw new RestaurantException("AdditionName should have at least 3 characters", typeof(Addition).FullName, "AdditionName");
            }

            AdditionName = additionName;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new RestaurantException($"Price '{price}' cannot be negative", typeof(Addition).FullName, "Price");
            }

            Price = price;
        }
    }
}
