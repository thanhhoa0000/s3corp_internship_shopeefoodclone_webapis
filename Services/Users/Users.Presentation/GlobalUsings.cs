global using System.Text;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;

global using NLog;
global using NLog.Web;

global using Asp.Versioning;

global using ShopeeFoodClone.WebApi.Users.Presentation.Configurations;

global using ShopeeFoodClone.WebApi.Users.Application.Dtos;
global using ShopeeFoodClone.WebApi.Users.Application.Interfaces;

global using ShopeeFoodClone.WebApi.Users.Infrastructure.Persistence;
global using ShopeeFoodClone.WebApi.Users.Infrastructure;

global using ShopeeFoodClone.WebApi.Users.Domain.Entities;