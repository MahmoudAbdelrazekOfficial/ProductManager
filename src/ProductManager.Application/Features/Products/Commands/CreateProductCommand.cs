using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Common;

namespace ProductManager.Application.Features.Products.Commands;

public record CreateProductCommand(CreateProductDto Dto) : IRequest<Result<ProductDto>>;
