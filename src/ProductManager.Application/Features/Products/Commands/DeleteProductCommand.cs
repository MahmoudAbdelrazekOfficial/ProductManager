using MediatR;
using ProductManager.Domain.Common;

namespace ProductManager.Application.Features.Products.Commands;

public record DeleteProductCommand(System.Guid Id) : IRequest<Result>;
