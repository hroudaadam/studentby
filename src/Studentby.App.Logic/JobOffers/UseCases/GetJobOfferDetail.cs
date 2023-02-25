using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobOffers.Exceptions;
using Studentby.App.Logic.JobOffers.Models;
using Studentby.Shared.Exceptions;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.JobOffers.UseCases;

[Authorize(Roles = new[] { UserRole.Operator, UserRole.Student, UserRole.Employer })]
public class GetJobOfferDetailRequest : IRequest<JobOfferDetailRes>
{
    public Guid Id { get; set; }
    
    public GetJobOfferDetailRequest(Guid id)
    {
        Id = id;
    }
}

internal class GetJobOfferDetailRequestHandler : IRequestHandler<GetJobOfferDetailRequest, JobOfferDetailRes>
{
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IEmployerRepository _employerRepository;

    public GetJobOfferDetailRequestHandler(
        IJobOfferRepository jobOfferRepository,
        ISecurityContextProvider securityContextProvider,
        IEmployerRepository employerRepository)
    {
        _jobOfferRepository = jobOfferRepository;
        _securityContextProvider = securityContextProvider;
        _employerRepository = employerRepository;
    }

    public async Task<JobOfferDetailRes> Handle(GetJobOfferDetailRequest request, CancellationToken cancellationToken)
    {
        SecurityContext securityContex = _securityContextProvider.SecurityContext;
        Guard.NotNull(securityContex);

        JobOffer jobOffer = await _jobOfferRepository.GetByIdAsync(request.Id);
        if (jobOffer == null) throw new JobOfferNotFoundException();

        if (securityContex.Role == UserRole.Employer)
        {
            var employer = await _employerRepository.GetByUserIdAsync(securityContex.SubjectId);
            Guard.NotNull(employer);

            if (jobOffer.GroupId != employer.GroupId) throw new NotAuthorizedException();
        }

        return jobOffer.Adapt<JobOfferDetailRes>(jo =>
        {
            jo.FreeSpaces = jobOffer.CountFreeSpaces();
        });
    }
}
