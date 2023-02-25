using Studentby.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentby.App.Logic.Users.Exceptions;
internal class InvalidChangePasswordSecretException : BadLogicException
{
    public InvalidChangePasswordSecretException() : base("InvalidChangePasswordSecretException")
    {

    }
}
