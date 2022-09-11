using Br.Capegemini.Teste._09._2022.Models;

namespace Br.Capegemini.Teste._09._2022.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Tokens> GenerateToken(UserRegistration user);
        Task<bool> ValidateToken(ValidateToken token);
    }
}
