global using System.ComponentModel.DataAnnotations;
global using System.Text.Json;

global using Microsoft.Extensions.DependencyInjection;

global using AutoMapper;

global using ShopeeFoodClone.WebApi.Cart.Application.Models.Dtos;
global using ShopeeFoodClone.WebApi.Cart.Application.Models.Enums;
global using ShopeeFoodClone.WebApi.Cart.Application.Models.Requests;
global using ShopeeFoodClone.WebApi.Cart.Application.Models.Responses;
global using ShopeeFoodClone.WebApi.Cart.Application.Interfaces;
global using ShopeeFoodClone.WebApi.Cart.Application.Services;
global using ShopeeFoodClone.WebApi.Cart.Application.Mappings;

global using ShopeeFoodClone.WebApi.Cart.Domain.Interfaces;
global using ShopeeFoodClone.WebApi.Cart.Domain.Entities;

global using ShopeeFoodClone.WebApi.EventBus.RabbitMQ.Messaging;
