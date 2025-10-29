using MediatR;
using ProductManager.Application.Features.Products.Commands;
using ProductManager.Domain.Common;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Application.Features.Products.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public DeleteProductHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var repo = _uow.Repository<Product>();
        var product = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            return Result.Failure("Product not found");

        repo.Remove(product);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
