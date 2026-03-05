namespace ReactAndJwt.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? HashedPassword { get; set; }
        public string? Role { get; set; }

        public List<Application> application { get; set; }
    }
}
