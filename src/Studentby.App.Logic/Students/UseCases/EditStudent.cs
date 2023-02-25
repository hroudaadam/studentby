using MediatR;
using Studentby.App.Data.Database.Entities.Fields;
using Studentby.App.Data.Database.Repositories;
using Studentby.App.Logic.Common.Attributes;
using Studentby.App.Logic.Students.Exceptions;
using Studentby.App.Logic.Students.Models;

namespace Studentby.App.Logic.Students.UseCases;

[Authorize(Roles = new[] { UserRole.Operator })]
public class EditStudentRequest : IRequest<StudentRes>
{
    public Guid Id { get; set; }
    public bool Activated { get; set; }
}

internal class EditStudentRequestHandler : IRequestHandler<EditStudentRequest, StudentRes>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public EditStudentRequestHandler(
        IStudentRepository studentRepository,
        IJobApplicationRepository jobApplicationRepository)
    {
        _studentRepository = studentRepository;
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<StudentRes> Handle(EditStudentRequest request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);
        if (student == null) throw new StudentNotFoundException();

        if (student.Activated && !request.Activated)
        {
            var aliveJobApplications = await _jobApplicationRepository.ListAliveByStudentIdAsync(student.Id);
            foreach (var ja in aliveJobApplications)
            {
                ja.State = JobApplicationState.Denied;
            }
        }

        student.Activated = request.Activated;
        student = await _studentRepository.UpdateAsync(student);
        return student.Adapt<StudentRes>(s =>
        {
            s.Email = student.User.Email;
        });
    }
}