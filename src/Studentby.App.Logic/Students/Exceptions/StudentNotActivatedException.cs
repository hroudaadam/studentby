using Studentby.Shared.Exceptions;

namespace Studentby.App.Logic.Students.Exceptions;

internal class StudentNotActivatedException : BadLogicException
{
    public StudentNotActivatedException() : base("StudentNotActivatedException")
    {

    }
}
