using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Groups.Exceptions;
using Studentby.App.Logic.Groups.Models;

namespace Studentby.App.Logic.Groups.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class GetGroupDetailRequest : IRequest<GroupRes>
{
    public Guid Id { get; set; }

    public GetGroupDetailRequest(Guid id)
    {
        Id = id;
    }
}

internal class GetGroupDetailRequestHandler : IRequestHandler<GetGroupDetailRequest, GroupRes>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupDetailRequestHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GroupRes> Handle(GetGroupDetailRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id);
        if (group == null) throw new GroupNotFoundException();

        return group.Adapt<GroupRes>();
    }
}
