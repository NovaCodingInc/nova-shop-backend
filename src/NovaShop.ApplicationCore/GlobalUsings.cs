﻿global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using NovaShop.ApplicationCore.Exceptions;
global using NovaShop.ApplicationCore.CatalogAggregate.Events;
global using NovaShop.ApplicationCore.CatalogAggregate;
global using NovaShop.ApplicationCore.CatalogAggregate.Specification;
global using NovaShop.ApplicationCore.OrderAggregate.Specification;
global using NovaShop.SharedKernel;
global using NovaShop.SharedKernel.Interfaces;
global using Ardalis.Specification;
global using Ardalis.GuardClauses;
global using MediatR;
global using Autofac;