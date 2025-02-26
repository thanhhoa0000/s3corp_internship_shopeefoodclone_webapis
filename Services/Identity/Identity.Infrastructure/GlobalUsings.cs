global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Linq.Expressions;

global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.JsonWebTokens;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using ShopeeFoodClone.WebApi.Identity.Infrastructure.Identity;
global using ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence;
global using ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence.Repositories;

global using ShopeeFoodClone.WebApi.Identity.Application.Interfaces;
global using ShopeeFoodClone.WebApi.Identity.Application;

global using ShopeeFoodClone.WebApi.Identity.Domain.Entities;
global using ShopeeFoodClone.WebApi.Identity.Domain.Enums;
global using ShopeeFoodClone.WebApi.Identity.Domain.Interfaces;
