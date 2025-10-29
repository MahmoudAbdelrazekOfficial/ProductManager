using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Common;
using System.Collections.Generic;

namespace ProductManager.Application.Features.Products.Queries;

public record GetProductsQuery : IRequest<Result<List<ProductDto>>>;
