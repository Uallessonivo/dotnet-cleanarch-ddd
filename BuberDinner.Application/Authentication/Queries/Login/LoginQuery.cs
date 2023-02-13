using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Errors;
using MediatR;
using OneOf;

namespace BuberDinner.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<OneOf<AuthenticationResult, DuplicateEmailError>>;