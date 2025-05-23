﻿namespace ShopeeFoodClone.WebApi.Products.Application.Models.Requests;

public sealed class VendorUpdateProductStateRequest
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public ProductState State { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}
