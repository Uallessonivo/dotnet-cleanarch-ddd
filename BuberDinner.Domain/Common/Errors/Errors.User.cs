namespace BuberDinner.Domain.Common.Errors;

public class Errors
{
    private Errors(string message)
    {
        Message = message;
    }

    public string Message { get; }

    public static class User
    {
        public static readonly Errors DuplicateEmail = new Errors("This email already exists.");
        public static readonly Errors InvalidEmailOrPassword = new Errors("Invalid email or password.");
        public static readonly Errors BadError = new Errors("Something went wrong.");

        public static Errors Validation(string propertyName, string errorMessage) =>
            new Errors($"error: {propertyName}, message: {errorMessage}");
    }
}