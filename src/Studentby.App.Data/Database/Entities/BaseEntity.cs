﻿namespace Studentby.App.Data.Database.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public byte[] Version { get; set; }
}