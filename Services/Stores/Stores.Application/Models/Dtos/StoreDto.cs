﻿namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Dtos;

public class StoreDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid UserId { get; set; }
    [MaxLength(20)]
    public string? WardCode { get; set; }
    [MaxLength(100)]
    public string? StreetName { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public string? Name { get; set; }
    [Required]
    public TimeOnly OpeningHour { get; set; }
    [Required]
    public TimeOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    public int Sold { get; set; } = 0;
    public bool IsPromoted { get; set; } = false;
    public StoreState State { get; set; } = StoreState.Active;
    public Guid ConcurrencyStamp  { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    
    public WardDto? Ward { get; set; }
    public ICollection<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    public ICollection<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
}
