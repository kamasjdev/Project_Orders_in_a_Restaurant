using Restaurant.Domain.Entities;

namespace Restaurant.ApplicationLogic.DTO
{
    public class AdditionDto : BaseDto
    {
        public string AdditionName { get; set; }
        public decimal Price { get; set; }
        public ProductKind AdditionKind { get; set; }
    }
}
