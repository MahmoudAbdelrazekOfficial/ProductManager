using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Application.Features.Products.Queries;
using ProductManager.Domain.Common;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Application.Features.Products.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IUnitOfWork _uow;

    public GetProductByIdHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<Product>();
        var product = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (product == null) return Result<ProductDto>.Failure("Product not found");

        var dto = new ProductDto(product.Id, product.Name, product.Description, product.Price, product.CreatedAt);
        return Result<ProductDto>.Success(dto);
    }
}
