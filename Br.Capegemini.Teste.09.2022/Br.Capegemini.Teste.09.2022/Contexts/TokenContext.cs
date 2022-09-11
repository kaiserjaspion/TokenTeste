using Br.Capegemini.Teste._09._2022.Helpers;
using Br.Capegemini.Teste._09._2022.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Infrastructure;

namespace Br.Capegemini.Teste._09._2022.Contexts
{
    public class TokenContext : DbContext
    {
        private List<Tokens> tokens = new List<Tokens>
        {
            new Tokens{
                CardId= 1,
                Token = "",
                RegistrationDate = DateTime.Now

            }

        };

        private List<UserRegistration> usersList = new List<UserRegistration>
        {
            new UserRegistration{
                CostumerId = 1,
                CardId= 1,
                CardNumber = 1234567891123456 ,
                CVV = 12345
            }

        };
        public TokenContext()
        {

        }
        public TokenContext(DbContextOptions<TokenContext> options) : base(options)
        {

        }

        public virtual DbSet<Tokens> Tokens { get; set; }

        public virtual DbSet<UserRegistration> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task<Mock<TokenContext>> mockContext()
        {
            var mockContent = new Mock<DbSet<Tokens>>();
            var mockContentUser = new Mock<DbSet<UserRegistration>>();

            mockContent.As<IDbAsyncEnumerable<Tokens>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<Tokens>(tokens.AsQueryable().GetEnumerator()));
            mockContent.As<IQueryable<Tokens>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Tokens>(tokens.AsQueryable().Provider));

            mockContent.As<IQueryable<Tokens>>().Setup(m => m.Expression).Returns(tokens.AsQueryable().Expression);
            mockContent.As<IQueryable<Tokens>>().Setup(m => m.ElementType).Returns(tokens.AsQueryable().ElementType);
            mockContent.As<IQueryable<Tokens>>().Setup(m => m.GetEnumerator()).Returns(tokens.AsQueryable().GetEnumerator());

            mockContentUser.As<IDbAsyncEnumerable<UserRegistration>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<UserRegistration>(usersList.AsQueryable().GetEnumerator()));
            mockContentUser.As<IQueryable<UserRegistration>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<UserRegistration>(usersList.AsQueryable().Provider));

            mockContentUser.As<IQueryable<UserRegistration>>().Setup(m => m.Expression).Returns(usersList.AsQueryable().Expression);
            mockContentUser.As<IQueryable<UserRegistration>>().Setup(m => m.ElementType).Returns(usersList.AsQueryable().ElementType);
            mockContentUser.As<IQueryable<UserRegistration>>().Setup(m => m.GetEnumerator()).Returns(usersList.AsQueryable().GetEnumerator());
            var context = new Mock<TokenContext>();
            context.Setup(c => c.Tokens).Returns(mockContent.Object);
            context.Setup(c => c.Users).Returns(mockContentUser.Object);
            return context;
        }
        
    }
}
