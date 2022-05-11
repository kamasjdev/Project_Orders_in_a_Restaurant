namespace Restaurant.ApplicationLogic.DTO
{
    public class ProductSaleDetailsDto : ProductSaleDto
    {
        public OrderDto Order { get; set; }
    }
}
