using System;

namespace DTO
{
    [Serializable]
    public class LoginRequest : IReturn<LoginResponse>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
    }

    [Serializable]
    public class LoginResponse
    {
        public int UserId { set; get; } = -1;
    }
}
