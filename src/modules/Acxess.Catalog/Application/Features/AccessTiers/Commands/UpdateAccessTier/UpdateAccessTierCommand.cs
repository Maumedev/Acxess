using System;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Commands.UpdateAccessTier;

public record UpdateAccessTierCommand
(
    int Id,
    string Name,
    string? Description
): IRequest<Result<string>>;
