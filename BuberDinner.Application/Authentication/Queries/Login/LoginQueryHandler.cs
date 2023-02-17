using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using MediatR;
using OneOf;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, OneOf<AuthenticationResult, Errors>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<OneOf<AuthenticationResult, Errors>> Handle(LoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.User.InvalidEmailOrPassword;
        }

        // Valid the password is correct
        if (user.Password != query.Password)
        {
            return Errors.User.InvalidEmailOrPassword;
        }

        // Create Jwt Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
};