global using System.Text;
global using System.Text.Json.Serialization;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;

global using NLog;
global using NLog.Web;

global using Asp.Versioning;

global using ShopeeFoodClone.WebApi.Orders.Presentation.Configurations;
    
global using ShopeeFoodClone.WebApi.Orders.Infrastructure.Persistence;
global using ShopeeFoodClone.WebApi.Orders.Infrastructure;

global using ShopeeFoodClone.WebApi.Orders.Application.Models.Dtos;
global using ShopeeFoodClone.WebApi.Orders.Application.Models.Requests;
global using ShopeeFoodClone.WebApi.Orders.Application.Models.Responses;
global using ShopeeFoodClone.WebApi.Orders.Application.Interfaces;

global using ShopeeFoodClone.WebApi.EventBus.RabbitMQ;
