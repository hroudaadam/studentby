﻿namespace Studentby.App.Data.Database.Entities;

public class Address : BaseEntity
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
}