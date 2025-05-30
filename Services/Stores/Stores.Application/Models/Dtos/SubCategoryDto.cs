﻿namespace ShopeeFoodClone.WebApi.Stores.Application.Models.Dtos;

public class SubCategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
    public Guid ConcurrencyStamp { get; set; }
    public CategoryState State { get; set; } = CategoryState.InUse;
    
    public CategoryDto? Category { get; set; }
    public ICollection<StoreDto> Stores { get; set; } = new List<StoreDto>();
}
