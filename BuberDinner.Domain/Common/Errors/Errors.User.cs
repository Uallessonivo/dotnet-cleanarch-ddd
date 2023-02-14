﻿namespace BuberDinner.Domain.Common.Errors;

public class Errors
{
    private Errors(string message)
    {
    }

    public static class User
    {
        public static readonly Errors DuplicateEmail = new Errors("This email already exists.");
        public static readonly Errors InvalidEmailOrPassword = new Errors("Invalid email or password.");
    }
}