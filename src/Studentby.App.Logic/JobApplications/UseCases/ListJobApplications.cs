using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.Shared.Helpers;
using System.Linq;

namespace Studentby.App.Logic.JobApplications.UseCases;

[Authorize(Roles = new[] { UserRole.Operator, UserRole.Student })]
public class ListJobApplicationsRequest : IRequest<IEnumerable<JobApplicationDetailRes>>
{
    public Guid? JobOfferId { get; set; } = null;
}

internal class ListJobApplicationsRequestHandler : IRequestHandler<ListJobApplicationsRequest, IEnumerable<JobApplicationDetailRes>>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IStudentRepository _studentRepository;

    public ListJobApplicationsRequestHandler(
        IJobApplicationRepository jobApplicationRepository,
        ISecurityContextProvider securityContextProvider,
        IStudentRepository studentRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _securityContextProvider = securityContextProvider;
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<JobApplicationDetailRes>> Handle(ListJobApplicationsRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<JobApplication> jobApplications;

        SecurityContext securityContex = _securityContextProvider.SecurityContext;
        Guard.NotNull(securityContex);

        if (securityContex.Role == UserRole.Student)
        {
            var student = await _studentRepository.GetByUserIdAsync(securityContex.SubjectId);
            Guard.NotNull(student);

            jobApplications = await _jobApplicationRepository.FiltredListAsync(student.Id, request.JobOfferId);
        }
        else
        {
            jobApplications = await _jobApplicationRepository.FiltredListAsync(jobOfferId: request.JobOfferId);
        }

        return jobApplications.Select(jo => jo.Adapt<JobApplicationDetailRes>());
    }
}
