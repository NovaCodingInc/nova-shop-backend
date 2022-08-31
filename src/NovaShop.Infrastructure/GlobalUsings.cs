﻿global using System.Text;
global using System.Reflection;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Identity;
global using NovaShop.SharedKernel;
global using NovaShop.SharedKernel.Interfaces;
global using NovaShop.Infrastructure.Data;
global using NovaShop.Infrastructure.Identity.Users;
global using NovaShop.ApplicationCore;
global using NovaShop.ApplicationCore.CatalogAggregate;
global using Ardalis.Specification.EntityFrameworkCore;
global using Autofac;
global using MediatR;
global using MediatR.Pipeline;