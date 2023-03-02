using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Entities;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = new();

    public User? GetUserByEmail(string email)
    {
        return Users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        Users.Add(user);
    }
}