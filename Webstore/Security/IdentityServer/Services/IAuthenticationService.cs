using IdentityServer.DTOs;
using IdentityServer.Entities;

namespace IdentityServer.Services
{
    public interface IAuthenticationService
    {
        // proverava da li korisnik moze da se uloguje
        Task<User> ValidateUser(UserCredentialsDTO userCredentials);
        Task<AuthenticationModel> CreateAuthenticationModel(User user);

        Task RemoveRefreshToken(User user, string refreshToken);

    }
}
