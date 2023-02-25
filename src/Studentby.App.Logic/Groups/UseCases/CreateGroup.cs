using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Groups.Exceptions;
using Studentby.App.Logic.Groups.Models;

namespace Studentby.App.Logic.Groups.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class CreateGroupRequest : IRequest<GroupRes>
{
    public string Name { get; set; }
}

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}

internal class CreateGroupRequestHandler : IRequestHandler<CreateGroupRequest, GroupRes>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupRequestHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GroupRes> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        if (await _groupRepository.AnyWithNameAsync(request.Name))
        {
            throw new GroupNameAlreadyTakenException();
        }

        Group group = request.Adapt<Group>();
        group = await _groupRepository.CreateAsync(group);

        return group.Adapt<GroupRes>();
    }
}
