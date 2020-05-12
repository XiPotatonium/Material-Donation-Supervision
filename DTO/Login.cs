using System;

namespace DTO
{
    public class LoginRequest
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    public class LoginResposne
    {
        public int UserId { set; get; } = -1;
    }
}
