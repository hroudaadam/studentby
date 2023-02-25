using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.JobApplications.Exceptions;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.App.Logic.JobOffers.Exceptions;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.JobApplications.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class EditJobApplicationRquest : IRequest<JobApplicationRes>
{
    public Guid Id { get; set; }
    public JobApplicationState State { get; set; }
}

internal class EditJobApplicationRquestHandler : IRequestHandler<EditJobApplicationRquest, JobApplicationRes>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly IJobOfferRepository _jobOfferRepository;

    public EditJobApplicationRquestHandler(
        IJobApplicationRepository jobApplicationRepository,
        IJobOfferRepository jobOfferRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _jobOfferRepository = jobOfferRepository;
    }

    public async Task<JobApplicationRes> Handle(EditJobApplicationRquest request, CancellationToken cancellationToken)
    {
        var jobApplication = await _jobApplicationRepository.GetByIdAsync(request.Id);
        if (jobApplication == null) throw new JobApplicationNotFoundException();

        if (!IsJobApplicationStateChangeValid(jobApplication.State, request.State))
        {
            throw new InvalidJobApplicationStateException();
        }

        var jobOffer = await _jobOfferRepository.GetByIdAsync(jobApplication.JobOfferId);
        Guard.NotNull(jobOffer);

        if (request.State == JobApplicationState.Approved && jobOffer.CountFreeSpaces() <= 0)
        {
            throw new JobOfferFullException();
        }

        jobApplication.State = request.State;
        jobApplication = await _jobApplicationRepository.UpdateAsync(jobApplication);
        return jobApplication.Adapt<JobApplicationRes>();
    }

    private bool IsJobApplicationStateChangeValid(JobApplicationState currentState, JobApplicationState newState)
    {
        bool toPenErr = newState == JobApplicationState.Pending;
        bool toAppErr = (newState == JobApplicationState.Approved) &&
            (currentState != JobApplicationState.Pending);
        bool toDenErr = (newState == JobApplicationState.Denied) &&
            (currentState != JobApplicationState.Pending);
        bool toAbsErr = (newState == JobApplicationState.Absent) &&
            (currentState != JobApplicationState.Attended) &&
            (currentState != JobApplicationState.Approved);
        bool toAttErr = (newState == JobApplicationState.Attended) &&
            ((currentState != JobApplicationState.Absent) &&
            (currentState != JobApplicationState.Approved));

        return !toPenErr && !toAppErr && !toDenErr && !toAbsErr && !toAttErr;
    }
}
