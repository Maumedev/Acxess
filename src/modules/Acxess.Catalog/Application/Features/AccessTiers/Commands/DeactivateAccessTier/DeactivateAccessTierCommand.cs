using System;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.AccessTiers.Commands.DeactivateAccessTier;

public record DeactivateAccessTierCommand(int Id) : IRequest<Result<string>>;