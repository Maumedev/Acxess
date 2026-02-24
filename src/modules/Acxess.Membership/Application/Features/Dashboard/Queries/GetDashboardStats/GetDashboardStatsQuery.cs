using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Dashboard.Queries.GetDashboardStats;

public record GetDashboardStatsQuery : IRequest<Result<DashboardStatsDto>>;