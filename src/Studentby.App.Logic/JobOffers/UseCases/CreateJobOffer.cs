using FluentValidation;
using MediatR;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Models;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobOffers.Models;
using Studentby.Shared.Helpers;

namespace Studentby.App.Logic.JobOffers.UseCases;

[Authorize(Roles = new[] { UserRole.Employer })]
public class CreateJobOfferRequest : IRequest<JobOfferRes>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Wage { get; set; }
    public int Spaces { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public AddressReq Address { get; set; }
}

public class CreateJobOfferRequestValidator : AbstractValidator<CreateJobOfferRequest>
{
    public CreateJobOfferRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}

internal class CreateJobOfferRequestHandler : IRequestHandler<CreateJobOfferRequest, JobOfferRes>
{
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IJobOfferRepository _jobOfferRepository;
    private readonly IEmployerRepository _employerRepository;

    public CreateJobOfferRequestHandler
        (ISecurityContextProvider securityContextProvider,
        IJobOfferRepository jobOfferRepository,
        IEmployerRepository employerRepository)
    {
        _securityContextProvider = securityContextProvider;
        _jobOfferRepository = jobOfferRepository;
        _employerRepository = employerRepository;
    }

    public async Task<JobOfferRes> Handle(CreateJobOfferRequest request, CancellationToken cancellationToken)
    {
        Guid userId = _securityContextProvider.GetSubjectId();
        var employer = await _employerRepository.GetByUserIdAsync(userId);
        Guard.NotNull(employer);

        JobOffer jobOffer = request.Adapt<JobOffer>(jo =>
        {
            jo.GroupId = employer.GroupId;
        });

        jobOffer = await _jobOfferRepository.CreateAsync(jobOffer);
        return jobOffer.Adapt<JobOfferRes>();
    }
}
