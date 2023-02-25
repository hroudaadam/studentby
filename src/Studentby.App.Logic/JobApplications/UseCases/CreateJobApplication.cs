using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobApplications.Exceptions;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.App.Logic.JobOffers.Exceptions;
using Studentby.App.Logic.Students.Exceptions;
using Studentby.Shared.Clock;
using Studentby.Shared.Helpers;
using System.Linq;

namespace Studentby.App.Logic.JobApplications.UseCases;

[Authorize(Roles = new[] { UserRole.Student })]
public class CreateJobApplicationRequest : IRequest<JobApplicationRes>
{
    public Guid JobOfferId { get; set; }
}

internal class CreateJobApplicationRequestHandler : IRequestHandler<CreateJobApplicationRequest, JobApplicationRes>
{
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly IClockProvider _clockProvider;
    private readonly IStudentRepository _studentRepository;
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public CreateJobApplicationRequestHandler(
        ISecurityContextProvider securityContextProvider,
        IJobOfferRepository jobOfferRepository,
        IClockProvider clockProvider,
        IStudentRepository studentRepository,
        IJobApplicationRepository jobApplicationRepository)
    {
        _securityContextProvider = securityContextProvider;
        _jobOfferRepository = jobOfferRepository;
        _clockProvider = clockProvider;
        _studentRepository = studentRepository;
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<JobApplicationRes> Handle(CreateJobApplicationRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _securityContextProvider.GetSubjectId();

        Student student = await _studentRepository.GetByUserIdAsync(userId);
        Guard.NotNull(student);

        if (!student.Activated) throw new StudentNotActivatedException();

        JobOffer jobOffer = await _jobOfferRepository.GetByIdAsync(request.JobOfferId);
        if (jobOffer == null) throw new JobOfferNotFoundException();

        // check if job offer start date is in future
        if (jobOffer.Start <= _clockProvider.Now)
        {
            throw new JobOfferAlreadyStartedException();
        }

        if (jobOffer.CountFreeSpaces() <= 0)
        {
            throw new JobOfferFullException();
        }

        // check if job application for this job offer already exists
        if (jobOffer.JobApplications != null &&
            jobOffer.JobApplications.Any(ja => ja.StudentId == student.Id))
        {
            throw new JobApplicationAlreadyExistsException();
        }

        JobApplication jobApplication = new()
        {
            JobOfferId = jobOffer.Id,
            StudentId = student.Id
        };

        jobApplication = await _jobApplicationRepository.CreateAsync(jobApplication);

        return jobApplication.Adapt<JobApplicationRes>();
    }
}
