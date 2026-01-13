using System;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Catalog.Application.Features.SellingPlans.Queries.GetSellingPlanById;

public record GetSellingPlanByIdQuery(int IdSellingPlan) : IRequest<Result<SellingPlanDto>>;

