using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum UserType
    {
        NORMAL,     //普通用户
        ADMIN,      // 管理员
        DELIVERER   // 配送员
    }

    /// <summary>
    /// 客户端请求用户数据的请求包
    /// </summary>
    [Serializable]
    public class UserInfoRequest : IReturn<UserInfoResponse>
    {
        public int UserId { set; get; }
    }

    /// <summary>
    /// 客户端请求用户数据的返回包
    /// </summary>
    [Serializable]
    public class UserInfoResponse
    {
        public string PhoneNumber { set; get; }
        public string HomeAddress { set; get; }
        public UserType UserType { set; get; }
    }

    /// <summary>
    /// 用户请求修改个人信息
    /// </summary>
    [Serializable]
    public class UserInfoModifyRequest : IReturn<VoidResponse>
    {
        public string PhoneNumber { set; get; }
        public string HomeAddress { set; get; }
    }
}
