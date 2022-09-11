using Br.Capegemini.Teste._09._2022.Repositories;
using Br.Capegemini.Teste._09._2022.Services;
using Br.Capegemini.Teste._09._2022.Services.Interfaces;
using Xunit;
using Moq;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Br.Capegemini.teste._09._2022.test
{
    public class TokenTest
    {
        private readonly ITokenService _Service;
        public TokenTest()
        {

            _Service = new TokenService(new TokenRepository(new Teste._09._2022.Contexts.TokenContext().mockContext().Result.Object), new SymmetricSecurityKey(Encoding.ASCII.GetBytes("eb9435ab-3b47-4749-be3f-5b12945011f4")));
        }

        [Fact]
        public async void GenerateTokenTest()
        {
            Assert.NotNull(await _Service.GenerateToken(new Teste._09._2022.Models.UserRegistration{ CardNumber = 1234567891123456 ,CVV = 12345, Amount = (decimal)5.00}));
        }

        [Fact]
        public async void ValidateTokenTest()
        {
            Assert.True(await _Service.ValidateToken(new Teste._09._2022.Models.ValidateToken{ Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJWYWx1ZSI6IiA2MzQ1IiwibmJmIjoxNjYyODU3MjU3LCJleHAiOjE2NjMwMzAwNTQsImlhdCI6MTY2Mjg1NzI1N30.BewPp2i96D7nMkhpAlNAbip9ny0D3znz3PU43Sz5Gnc", CardId = 1 , CVV = 12345, CostumerId = 1  }));
        }
    }
}