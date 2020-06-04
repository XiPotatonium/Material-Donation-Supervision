using System;
using DTO;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    class UserInfoService
    {
        public int UserId { get; set; }

        public LoginResponse HandleLoginRequest(LoginRequest request)
        {
            //TODO: 数据库查询
            return new LoginResponse()
            {

            };
        }

        public UserInfoResponse HandleUserInfoRequest(UserInfoRequest request)
        {
            //TODO: 数据库查询
            return new UserInfoResponse()
            {

            };
        }

        public VoidResponse HandleModifyRequest(UserInfoModifyRequest request)
        {
            //TODO: 数据库操作 这个不需要填返回信息
            return new VoidResponse();
        }

        public RegisterResponse HandleRegisterRequest(RegisterRequest request)
        {
            //TODO:数据库
            return new RegisterResponse()
            {

            };
        }
    }
}
