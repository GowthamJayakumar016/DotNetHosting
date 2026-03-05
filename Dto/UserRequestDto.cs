namespace ReactAndJwt.Dto
{
    public class UserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
