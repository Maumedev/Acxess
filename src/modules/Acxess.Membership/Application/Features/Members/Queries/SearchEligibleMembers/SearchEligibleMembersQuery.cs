using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Membership.Application.Features.Members.Queries.SearchEligibleMembers;

public record SearchEligibleMembersQuery(string Term) : IRequest<Result<List<EligibleMemberDto>>>;

public record EligibleMemberDto(
    int IdMember, 
    string FirstName, 
    string LastName, 
    string? Phone, 
    string? Email, 
    bool IsEligible, 
    string IneligibilityReason
);