using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string ProductName { get; set; }
        public long PriceBeforeDot { get; set; }
        public long PriceAfterDot { get; set; }
        public ISet<Order> Orders { get; set; }
    }
}
