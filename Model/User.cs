﻿namespace Model;

public class User
{
    public int Id { get; set; }
    public int? OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}