using System;
using DTO;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    class Service
    {
        public static LoginResponse HandleLoginRequest(LoginRequest request)
        {
            //TODO: 数据库查询
            return new LoginResponse()
            {
                UserId = (request.Password + request.UserName).Length
            };
        }
        
        public static UserInfoResponse HandleUserInfoRequest(UserInfoRequest request)
        {
            //TODO: 数据库查询
            return new UserInfoResponse()
            {

            };
        }
    }
}
