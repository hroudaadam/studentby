using Mapster;
using Moq;
using Studentby.App.Data.Database.Entities;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Security;
using Studentby.App.Logic.JobApplications.Models;
using Studentby.App.Logic.JobApplications.UseCases;
using Studentby.App.Logic.JobOffers.Exceptions;
using Studentby.Shared.Clock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Studentby.App.Logic.UnitTest.JobApplications;

public class CreateJobApplicationTest
{
    public CreateJobApplicationTest()
    {
        SetupMapper();
    }

    [Fact]
    public async Task Handle_JobOfferFull()
    {
        // Arrange
        JobOffer jobOffer = CreateFullJobOffer();
        Student student = CreateStudent();
        JobApplication jobApplication = CreateNewJobApplication();

        var mockJobOfferRepository = new Mock<IJobOfferRepository>();
        mockJobOfferRepository
            .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(jobOffer);

        var mockStudentRepository = new Mock<IStudentRepository>();
        mockStudentRepository
            .Setup(m => m.GetByUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(student);

        var mockSecurityContextProvider = new Mock<ISecurityContextProvider>();
        mockSecurityContextProvider
            .Setup(m => m.GetSubjectId())
                .Returns(Guid.NewGuid());

        var mockClockProvider = new Mock<IClockProvider>();
        mockClockProvider
            .Setup(m => m.Now)
                .Returns(DateTime.UtcNow);

        var mockJobApplicationRepository = new Mock<IJobApplicationRepository>();
        mockJobApplicationRepository
            .Setup(m => m.CreateAsync(It.IsAny<JobApplication>(), true))
                .ReturnsAsync(jobApplication);


        CreateJobApplicationRequestHandler handler = new(
            mockSecurityContextProvider.Object,
            mockJobOfferRepository.Object,
            mockClockProvider.Object,
            mockStudentRepository.Object,
            mockJobApplicationRepository.Object);
        CreateJobApplicationRequest request = new()
        {
            JobOfferId = jobOffer.Id
        };

        // Act
        async Task<JobApplicationRes> act()
        {
            return await handler.Handle(request, CancellationToken.None);
        }


        // Assert
        await Assert.ThrowsAsync<JobOfferFullException>(act);
    }


    private void SetupMapper()
    {
        var mapperConfig = TypeAdapterConfig.GlobalSettings;
        mapperConfig.Scan(Assembly.GetAssembly(typeof(DependencyInjection)));
    }

    private Student CreateStudent()
    {
        Student student = new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Jan",
            LastName = "Novák",
            Activated = true,
            DateOfBirth = DateTime.UtcNow.AddYears(-21),
            Address = new()
            {
                Id = Guid.NewGuid(),
                Country = "Česká republika",
                City = "Praha",
                Street = "Oblá",
                Number = "2"
            },
            User = new()
            {
                Id = Guid.NewGuid(),
                Email = "jan.novak@gmail.com",
                Role = UserRole.Student
            }
        };
        return student;
    }

    private JobOffer CreateFullJobOffer()
    {
        Guid jobOfferId = Guid.NewGuid();

        JobOffer jobOffer = new()
        {
            Id = jobOfferId,
            Title = "Práce na skladu",
            Description = "Lorem ipsum",
            Spaces = 1,
            Start = DateTime.UtcNow.AddHours(24),
            End = DateTime.UtcNow.AddHours(28),
            Wage = 200.0,
            JobApplications = new JobApplication[]
            {
                new JobApplication()
                {
                    Id = Guid.NewGuid(),
                    JobOfferId = jobOfferId,
                    State = JobApplicationState.Approved,
                    StudentId = Guid.NewGuid()
                }
            }
        };
        return jobOffer;
    }

    private JobApplication CreateNewJobApplication()
    {
        JobApplication jobApplication = new()
        {
            Id = Guid.NewGuid(),
            JobOfferId = Guid.NewGuid(),
            State = JobApplicationState.Approved,
            StudentId = Guid.NewGuid()
        };
        return jobApplication;
    }
}
