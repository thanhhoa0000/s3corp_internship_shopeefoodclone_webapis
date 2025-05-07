﻿global using System.Text;
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

global using ShopeeFoodClone.WebApi.Payment.Presentation.Configurations;
    
global using ShopeeFoodClone.WebApi.Payment.Infrastructure.Persistence;
global using ShopeeFoodClone.WebApi.Payment.Infrastructure;

global using ShopeeFoodClone.WebApi.Payment.Application.Models.Dtos;
global using ShopeeFoodClone.WebApi.Payment.Application.Models.Requests;
global using ShopeeFoodClone.WebApi.Payment.Application.Models.Responses;
global using ShopeeFoodClone.WebApi.Payment.Application.Interfaces;