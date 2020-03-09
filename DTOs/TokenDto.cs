using System;

namespace ReactStartApi.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}