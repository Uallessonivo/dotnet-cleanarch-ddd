using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using MediatR;
using OneOf;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginCommandHandler : IRequestHandler<LoginQuery, OneOf<AuthenticationResult, DuplicateEmailError>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<OneOf<AuthenticationResult, DuplicateEmailError>> Handle(LoginQuery query,
        CancellationToken cancellationToken)
    {
        // Validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new Exception("Email or password is incorrect.");
        }

        // Valid the password is correct
        if (user.Password != query.Password)
        {
            throw new Exception("Email or password is incorrect");
        }

        // Create Jwt Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
};