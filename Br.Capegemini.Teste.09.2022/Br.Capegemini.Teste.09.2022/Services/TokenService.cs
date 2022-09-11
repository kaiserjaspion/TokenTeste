using Br.Capegemini.Teste._09._2022.Models;
using Br.Capegemini.Teste._09._2022.Repositories.Interfaces;
using Br.Capegemini.Teste._09._2022.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Br.Capegemini.Teste._09._2022.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _repository;
        private SymmetricSecurityKey _key;
        public TokenService(ITokenRepository repository, SymmetricSecurityKey key)
        {
            _repository = repository;
            _key = key;
        }

        public async Task<Tokens> GenerateToken(UserRegistration user)
        {
            try
            {

                var token = new Tokens();

                token.Token = await this.TokenCreationAssistent(user.CardNumber,user.CVV);

                return await _repository.GenerateToken(user,token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ValidateToken(ValidateToken token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var readableToken = tokenHandler.ReadJwtToken(token.Token);
                if (readableToken.ValidTo < DateTime.Now) return false;

                var customer = await _repository.GetCostumer(token.CardId);
                if (customer.CostumerId != token.CostumerId) return false;
                Console.WriteLine(customer.CardNumber);

                var tokenClaims = readableToken.Claims.Where(x => x.Type == "Value").First().Value;
                var tokenGenerated = tokenHandler.ReadJwtToken(await TokenCreationAssistent(customer.CardNumber, token.CVV)).Claims.Where(x => x.Type == "Value").First().Value;
                if (tokenClaims != tokenGenerated) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task<string> TokenCreationAssistent(long CardNumber, int CVV)
        {

            var CardArray = CardNumber.ToString().Substring(CardNumber.ToString().Length - 4).ToCharArray();

            for(var i = 0; i < CVV; i++)
            {
                char t = CardArray[CardArray.Length - 1];
                for (int p = CardArray.Length - 1; p > 0; p--)
                    CardArray[p] = CardArray[p - 1];
                CardArray[0] = t;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(_key,
                            SecurityAlgorithms.HmacSha256Signature
                        )
            };
            claimsIdentity.AddClaim(new Claim("Value", $" {CardArray[0]}{CardArray[1]}{CardArray[2]}{CardArray[3]}"));

            tokenDescriptor.Subject = claimsIdentity;

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token).ToString();
        }
    }
}
