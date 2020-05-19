using System;
using DTO;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    class UserInfoService
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

        public static VoidResponse HandleModifyRequest(UserInfoModifyRequest request)
        {
            //TODO: 数据库操作
            return new VoidResponse();
        }
    }
}
