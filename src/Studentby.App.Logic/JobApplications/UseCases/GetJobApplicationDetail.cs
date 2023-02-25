using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobApplications.Exceptions;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.Shared.Exceptions;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.JobApplications.UseCases;

[Authorize(Roles = new[] { UserRole.Operator, UserRole.Student })]
public class GetJobApplicationDetailRequest : IRequest<JobApplicationDetailRes>
{
    public Guid Id { get; set; }

    public GetJobApplicationDetailRequest(Guid id)
    {
        Id = id;
    }
}

internal class GetJobApplicationDetailRequestHandler : IRequestHandler<GetJobApplicationDetailRequest, JobApplicationDetailRes>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IStudentRepository _studentRepository;

    public GetJobApplicationDetailRequestHandler(
        IJobApplicationRepository jobApplicationRepository,
        ISecurityContextProvider securityContextProvider,
        IStudentRepository studentRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _securityContextProvider = securityContextProvider;
        _studentRepository = studentRepository;
    }

    public async Task<JobApplicationDetailRes> Handle(GetJobApplicationDetailRequest request, CancellationToken cancellationToken)
    {
        SecurityContext securityContex = _securityContextProvider.SecurityContext;
        Guard.NotNull(securityContex);

        JobApplication jobApplication = await _jobApplicationRepository.GetByIdAsync(request.Id);
        if (jobApplication == null) throw new JobApplicationNotFoundException();

        if (securityContex.Role == UserRole.Student && jobApplication.Student.UserId != securityContex.SubjectId)
        {
            throw new NotAuthorizedException();
        }

        return jobApplication.Adapt<JobApplicationDetailRes>();
    }
}
