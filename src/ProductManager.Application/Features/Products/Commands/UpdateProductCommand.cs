using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Common;

namespace ProductManager.Application.Features.Products.Commands;

public record UpdateProductCommand(System.Guid Id, UpdateProductDto Dto) : IRequest<Result<ProductDto>>;
