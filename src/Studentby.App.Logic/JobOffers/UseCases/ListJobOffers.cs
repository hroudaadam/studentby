using FluentValidation;
using MediatR;
using Studentby.App.Data.Cache;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobOffers.Models;
using Studentby.Shared.Helpers;
using Studentby.Shared.Structures;
using System.Linq;

namespace Studentby.App.Logic.JobOffers.UseCases;

[Authorize(Roles = new[] { UserRole.Operator, UserRole.Student, UserRole.Employer })]
public class ListJobOffersRequest : IRequest<PaginatedList<JobOfferRes>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string City { get; set; } = null;
    public DateTime? Date { get; set; } = null;
}

public class ListJobOffersRequestValidator : AbstractValidator<ListJobOffersRequest> 
{
    public ListJobOffersRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);
        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}

internal class ListJobOffersRequestHandler : IRequestHandler<ListJobOffersRequest, PaginatedList<JobOfferRes>>
{
    private readonly IJobOfferRepository _cachedJobOfferRepository;
    private readonly ISecurityContextProvider _securityContextProvider;
    private readonly IEmployerRepository _employerRepository;

    public ListJobOffersRequestHandler(
        Cached<IJobOfferRepository> cachedJobOfferRepository,
        ISecurityContextProvider securityContextProvider,
        IEmployerRepository employerRepository)
    {
        _cachedJobOfferRepository = cachedJobOfferRepository.Value;
        _securityContextProvider = securityContextProvider;
        _employerRepository = employerRepository;
    }

    public async Task<PaginatedList<JobOfferRes>> Handle(ListJobOffersRequest request, CancellationToken cancellationToken)
    {
        PaginatedList<JobOffer> jobOffers;

        SecurityContext securityContex = _securityContextProvider.SecurityContext;
        Guard.NotNull(securityContex);

        if (securityContex.Role == UserRole.Employer)
        {
            var employer = await _employerRepository.GetByUserIdAsync(securityContex.SubjectId);
            Guard.NotNull(employer);

            jobOffers = await _cachedJobOfferRepository.FiltredListAsync(request.Page, request.PageSize, employer.GroupId);
        }
        else
        {
            jobOffers = await _cachedJobOfferRepository.FiltredListAsync(request.Page, request.PageSize);
        }

        return jobOffers.TransformDataType(jobOffers.Data.Select(jo => jo.Adapt<JobOfferRes>()));
    }
}
