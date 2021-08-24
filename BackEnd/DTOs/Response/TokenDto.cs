namespace BackEnd.DTOs.Response
{
    public record TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}