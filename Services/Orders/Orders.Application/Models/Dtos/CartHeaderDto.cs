﻿namespace ShopeeFoodClone.WebApi.Orders.Application.Models.Dtos;

public class CartHeaderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal TotalPrice { get; set; } = 0;
}
