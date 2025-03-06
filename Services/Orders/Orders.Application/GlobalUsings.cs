global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Text.Json;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;

global using AutoMapper;

global using ShopeeFoodClone.WebApi.Orders.Application.Dtos;
global using ShopeeFoodClone.WebApi.Orders.Application.Interfaces;
global using ShopeeFoodClone.WebApi.Orders.Application.Services;
global using ShopeeFoodClone.WebApi.Orders.Application.Mappings;

global using ShopeeFoodClone.WebApi.Orders.Domain.Interfaces;
global using ShopeeFoodClone.WebApi.Orders.Domain.Entities;
global using ShopeeFoodClone.WebApi.Orders.Domain.Enums;

global using ShopeeFoodClone.WebApi.EventBus.RabbitMQ.Messaging;
