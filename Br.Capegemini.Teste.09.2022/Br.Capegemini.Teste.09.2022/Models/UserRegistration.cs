using System.Configuration;

namespace Br.Capegemini.Teste._09._2022.Models
{
    public class UserRegistration
    {
        public int CostumerId { get; set; }
        [LongValidator(MinValue = 1000000000000000, MaxValue = 9999999999999999, ExcludeRange = true)]
        public long CardNumber { get; set; }
        [IntegerValidator(MinValue = 100, MaxValue = 99999, ExcludeRange = true)]
        public int CVV { get; set; }
        public int CardId { get; set; }

    }
}
