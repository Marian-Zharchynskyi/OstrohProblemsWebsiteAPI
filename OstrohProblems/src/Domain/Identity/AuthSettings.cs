﻿namespace Domain.Identity;

public static class AuthSettings
{
    public const string UserRole = "User";
    public const string AdminRole = "Administrator";

    public static readonly List<string> ListOfRoles = new()
    {
        UserRole,
        AdminRole
    };
}