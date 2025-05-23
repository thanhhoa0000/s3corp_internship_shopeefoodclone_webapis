﻿namespace ShopeeFoodClone.WebApi.Identity.Application.Models.Dtos;

public sealed class LoginRequest
{
    [Required]
    public required string UserName { get; set; }
    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }
}