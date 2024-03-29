﻿using Studentby.App.Data.Database.Entities.Fields;

namespace Studentby.App.Logic.Common.Security;

public class SecurityContext
{
    public Guid SubjectId { get; init; }
    public UserRole Role { get; init; }

    public SecurityContext(Guid subjectId, UserRole role)
    {
        SubjectId = subjectId;
        Role = role;
    }
}
