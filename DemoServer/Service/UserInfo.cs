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
            //TODO: 数据库操作 这个不需要填返回信息
            return new VoidResponse();
        }

        public static RegisterResponse HandleRegisterRequest(RegisterRequest request)
        {
            //TODO:数据库
            return new RegisterResponse()
            {

            };
        }
    }
}
