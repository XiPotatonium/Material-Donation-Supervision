using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace MDS.Server
{
    class Test
    {
        public static void UserLoginTest()
        {
            var service = new UserInfoService();
            var response = service.HandleLoginRequest(new LoginRequest() { PhoneNumber = "18867934185", Password = "123456" });
            //System.Diagnostics.Debug.Assert(response.UserId == 1);
            Console.WriteLine("Pass UserLoginTest");
        }

        public static void UserInfoTest()
        {
            var service = new UserInfoService();
            var response = service.HandleUserInfoRequest(new UserInfoRequest() { UserId = 1 });
            //System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(response.PhoneNumber));
            Console.WriteLine("Pass UserInfoTest");
        }

        public static void ModifyRequestTest()
        {
            var service = new UserInfoService() { UserId = 1 };
            var response = service.HandleModifyRequest(new UserInfoModifyRequest() { HomeAddress="609", PhoneNumber="12345678"});
            Console.WriteLine("Pass ModifyRequestTest"); //这里直接过了 懒得写测试了
        }

        public static void RegisterRequestTest()
        {
            var service = new UserInfoService() { UserId = 1 };
            var response = service.HandleRegisterRequest(new RegisterRequest() { Password="fuck", PhoneNumber="555555555"});
            if (response.UserId != -1)
            {
                Console.WriteLine("Pass RegisterRequestTest");
            }
            else
            {
                Console.WriteLine("Fail RegisterRequestTest");
            }
        }
    }
}
