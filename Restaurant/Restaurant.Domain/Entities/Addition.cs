using System;

namespace Restaurant.Domain.Entities
{
    public class Addition : BaseEntity<Guid>
    {
        private Addition()
        {
        }

        public Addition(Guid id, string name, decimal price) : base(id)
        {
            ChangeAdditionName(name);
            ChangePrice(price);
        }

        public string AdditionName { get; private set; }
        public decimal Price { get; private set; }

        public void ChangeAdditionName(string additionName)
        {
            if (string.IsNullOrWhiteSpace(additionName))
            {
                throw new InvalidOperationException("AdditionName cannot be empty");
            }

            if (additionName.Length < 3)
            {
                throw new InvalidOperationException("AdditionName should have at least 3 characters");
            }

            AdditionName = additionName;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new InvalidOperationException($"Price '{price}' cannot be negative");
            }

            Price = price;
        }
    }
}
