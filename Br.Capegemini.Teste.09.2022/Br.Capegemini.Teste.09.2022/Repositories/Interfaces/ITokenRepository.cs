using Br.Capegemini.Teste._09._2022.Models;

namespace Br.Capegemini.Teste._09._2022.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task<Tokens> GenerateToken(UserRegistration user,Tokens token);
        Task<Tokens> ValidateToken(ValidateToken token);
        Task<UserRegistration> GetCostumer(int CardId);
    }
}
