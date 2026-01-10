using Acxess.Shared.ResultManager;
using MediatR;


namespace Acxess.Catalog.Application.Features.SellingPlans.Queries.GetSellingPlans;
public record GetSellingPlanQuery(bool IncludeInactives) 
    : IRequest<Result<List<SellingPlanDto>>>;