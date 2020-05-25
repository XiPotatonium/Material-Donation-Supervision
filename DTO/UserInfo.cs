using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum UserType
    {
        NORMAL, ADMIN, DELIVERER
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
        // public string Name { set; get; }
        // 2020-05-25，不再区分PhoneNumber和Name，就用PhoneNumber登陆
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
