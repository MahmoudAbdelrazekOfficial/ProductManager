using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Common;

namespace ProductManager.Application.Features.Products.Queries;

public record GetProductByIdQuery(System.Guid Id) : IRequest<Result<ProductDto>>;
