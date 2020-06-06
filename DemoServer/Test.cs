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
            System.Diagnostics.Debug.Assert(response.UserId == 1024);
        }
    }
}
