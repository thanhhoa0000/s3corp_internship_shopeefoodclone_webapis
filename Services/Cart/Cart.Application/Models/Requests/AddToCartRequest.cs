﻿namespace ShopeeFoodClone.WebApi.Cart.Application.Models.Requests;

public sealed class AddToCartRequest
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
