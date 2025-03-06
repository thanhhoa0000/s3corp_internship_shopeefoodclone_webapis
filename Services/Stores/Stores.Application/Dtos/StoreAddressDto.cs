namespace ShopeeFoodClone.WebApi.Stores.Application.Dtos;

public class StoreAddressDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Province { get; set; }
    [Required]
    [MaxLength(50)]
    public required string District { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Ward { get; set; }
    [Required]
    [MaxLength(50)]
    public required string Street { get; set; }
    [MaxLength(50)]
    public string? HouseNumber { get; set; }
    
    public StoreDto? Store { get; set; }
}
