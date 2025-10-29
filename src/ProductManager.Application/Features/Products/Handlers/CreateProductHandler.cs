using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Application.Features.Products.Commands;
using ProductManager.Domain.Common;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Application.Features.Products.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    private readonly IUnitOfWork _uow;

    public CreateProductHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var product = new Product(dto.Name, dto.Price, dto.Description);

        var repo = _uow.Repository<Product>();
        await repo.AddAsync(product, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        var resultDto = new ProductDto(product.Id, product.Name, product.Description, product.Price, product.CreatedAt);
        return Result<ProductDto>.Success(resultDto);
    }
}
