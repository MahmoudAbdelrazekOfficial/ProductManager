using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Application.Features.Products.Commands;
using ProductManager.Domain.Common;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Application.Features.Products.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    private readonly IUnitOfWork _uow;

    public UpdateProductHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<Product>();
        var product = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            return Result<ProductDto>.Failure("Product not found");

        product.Update(request.Dto.Name, request.Dto.Price, request.Dto.Description);
        repo.Update(product);
        await _uow.SaveChangesAsync(cancellationToken);

        var dto = new ProductDto(product.Id, product.Name, product.Description, product.Price, product.CreatedAt);
        return Result<ProductDto>.Success(dto);
    }
}
