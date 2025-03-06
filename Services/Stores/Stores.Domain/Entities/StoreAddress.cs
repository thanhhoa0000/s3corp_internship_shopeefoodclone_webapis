namespace ShopeeFoodClone.WebApi.Stores.Domain.Entities;

public class StoreAddress : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
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
    
    public required Store Store { get; set; }
}
