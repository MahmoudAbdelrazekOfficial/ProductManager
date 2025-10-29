using System;

namespace ProductManager.Application.DTOs;

public record ProductDto(Guid Id, string Name, string? Description, decimal Price, DateTime CreatedAt);

public record CreateProductDto(string Name, string? Description, decimal Price);

public record UpdateProductDto(string Name, string? Description, decimal Price);
