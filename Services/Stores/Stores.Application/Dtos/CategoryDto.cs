namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    public Guid ConcurrencyStamp  { get; set; }
    
    public ICollection<StoreDto> Stores { get; set; } = new List<StoreDto>();
}