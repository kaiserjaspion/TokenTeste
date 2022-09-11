using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Br.Capegemini.Teste._09._2022.Models
{
    public class UserRegistration
    {
        public int CostumerId { get; set; }
        [Range(1000000000000000, 9999999999999999)]
        [LongValidator(MinValue = 1000000000000000,MaxValue = 9999999999999999),Required]
        public long CardNumber { get; set; }
        [Range(100, 99999)]
        [IntegerValidator(MinValue = 100, MaxValue = 99999), Required]
        public int CVV { get; set; }
        public int CardId { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        public decimal Amount { get; set; }

    }
}
