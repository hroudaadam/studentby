using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Groups.Models;
using System.Data;
using System.Linq;

namespace Studentby.App.Logic.Groups.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class ListGroupsRequest : IRequest<IEnumerable<GroupRes>>
{
}

internal class ListGroupsRequestHandler : IRequestHandler<ListGroupsRequest, IEnumerable<GroupRes>>
{
    private readonly IGroupRepository _groupRepository;

    public ListGroupsRequestHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<GroupRes>> Handle(ListGroupsRequest request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.ListAsync();
        return groups.Select(g => g.Adapt<GroupRes>());
    }
}