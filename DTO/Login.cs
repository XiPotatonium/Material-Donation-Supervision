using System;

namespace DTO
{
    public class LoginRequest : IReturn<LoginResponse>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    public class LoginResponse
    {
        public int UserId { set; get; } = -1;
    }
}
