using Br.Capegemini.Teste._09._2022.Contexts;
using Br.Capegemini.Teste._09._2022.Models;
using Br.Capegemini.Teste._09._2022.Repositories.Interfaces;
using Moq;
using System.Data.Entity;

namespace Br.Capegemini.Teste._09._2022.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly TokenContext _context;
        public TokenRepository(TokenContext context)
        {
            _context = context.mockContext().Result.Object;
        }

        public async Task<Tokens> GenerateToken(UserRegistration user, Tokens token)
        {
            try
            {
                _context.Add<Tokens>(token);
                await _context.SaveChangesAsync();
                
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tokens> ValidateToken(ValidateToken token)
        {
            try
            {
                return await _context.Tokens.Where(x => x.CardId == token.CardId).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserRegistration> GetCostumer (int CardId)
        {
            try
            {
                return await _context.Users.Where(x => x.CardId == CardId).FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
