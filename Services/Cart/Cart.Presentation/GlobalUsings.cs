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

global using ShopeeFoodClone.WebApi.Cart.Presentation.Configurations;
    
global using ShopeeFoodClone.WebApi.Cart.Infrastructure.Persistence;
global using ShopeeFoodClone.WebApi.Cart.Infrastructure;

global using ShopeeFoodClone.WebApi.Cart.Application.Dtos;
global using ShopeeFoodClone.WebApi.Cart.Application.Responses;
global using ShopeeFoodClone.WebApi.Cart.Application.Interfaces;

global using ShopeeFoodClone.WebApi.EventBus.RabbitMQ;
