using System;

namespace DTO
{
    [Serializable]
    public class LoginRequest : IReturn<LoginResponse>
    {
        public string PhoneNumber { set; get; }
        public string Password { set; get; }
    }

    [Serializable]
    public class LoginResponse
    {
        /// <summary>
        /// 登陆失败返回-1
        /// </summary>
        public int UserId { set; get; } = -1;
    }

    [Serializable]
    public class RegisterRequest : IReturn<RegisterResponse>
    {
        public string PhoneNumber { set; get; }
        public string Password { set; get; }
    }

    [Serializable]
    public class RegisterResponse
    {
        /// <summary>
        /// 登陆失败返回-1
        /// </summary>
        public int UserId { set; get; } = -1;
    }
}
