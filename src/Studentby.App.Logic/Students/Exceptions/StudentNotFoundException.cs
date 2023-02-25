using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Students.Exceptions;

internal class StudentNotFoundException : NotFoundException
{
    public StudentNotFoundException() : base("StudentNotFoundException")
    {

    }
}
