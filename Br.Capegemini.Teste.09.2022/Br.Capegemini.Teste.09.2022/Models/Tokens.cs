namespace Br.Capegemini.Teste._09._2022.Models
{
    public class Tokens
    {
        public int CardId { get; set; }
        public string Token { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
