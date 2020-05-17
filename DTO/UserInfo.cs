using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum UserType
    {
        NORMAL, ADMIN, DELIVERER
    }

    [Serializable]
    public class UserInfoRequest : IReturn<UserInfoResponse>
    {
        public int UserId { set; get; }
    }

    [Serializable]
    public class UserInfoResponse
    {
        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public string HomeAddress { set; get; }
        public UserType UserType { set; get; }
    }

    [Serializable]
    public class UserInfoModifyRequest : IReturn<VoidResponse>
    {
        public string PhoneNumber { set; get; }
        public string HomeAddress { set; get; }
    }
}
