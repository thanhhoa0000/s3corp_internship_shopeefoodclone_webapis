﻿namespace ShopeeFoodClone.WebApi.Cart.Application.Models.Dtos;

public class CartDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}
