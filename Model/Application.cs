namespace ReactAndJwt.Model
{
    public class Application
    {
        public int Id { get; set; }
      
        public string? Name { get; set; }
        public DateOnly DOB { get; set; }
        public string? PAN { get; set; }
        public decimal Income { get; set; }
        public int CreditScore  { get; set; }
        public int Creditlimit { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }

    }
}
