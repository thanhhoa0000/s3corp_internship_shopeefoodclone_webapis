namespace ShopeeFoodClone.WebApi.Cart.Application.Dtos;

public class CartHeaderDto
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string? SessionId { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal TotalPrice { get; set; }
}
