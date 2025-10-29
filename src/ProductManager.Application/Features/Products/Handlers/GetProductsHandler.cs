using MediatR;
using ProductManager.Application.DTOs;
using ProductManager.Application.Features.Products.Queries;
using ProductManager.Domain.Common;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Application.Features.Products.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly IUnitOfWork _uow;

    public GetProductsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<Product>();
        var list = await repo.GetAllAsync(cancellationToken);
        var dtos = list.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.CreatedAt)).ToList();
        return Result<List<ProductDto>>.Success(dtos , "Products retrieved successfully");
    }
}
