using System;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public long PriceBeforeDot { get; set; }
        public long PriceAfterDot { get; set; }
        public string Email { get; set; }
    }
}
