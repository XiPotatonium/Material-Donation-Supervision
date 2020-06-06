using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using MDS.Server.Service;

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

        public static void HandleGetApplicationListRequestTest()
        {
            var service = new ApplicationDataService() { UserId = 1024 };
            var response = service.HandleGetApplicationListRequest(new GetApplicationListRequest() { });
        }

        public static void HandleGetApplicationDetailRequestTest()
        {
            var service = new ApplicationDataService() { UserId = 1024 };
            var response = service.HandleGetApplicationDetailRequest(new GetApplicationDetailRequest() { ApplicationId = 1 });
        }

        public static void HandleAvailableApplicationMaterialRequestTest()
        {
            var service = new ApplicationDataService() { UserId = 1024 };
            var response = service.HandleAvailableApplicationMaterialRequest(new AvailableApplicationMaterialRequest() { });
        }

        public static void HandleNewApplicationRequestTest()
        {
            var service = new ApplicationDataService() { UserId = 1024 };
            var response = service.HandleNewApplicationRequest(new NewApplicationRequest() { });
        }

        public static void UserInfoTest()
        {
            var service = new UserInfoService();
            var response = service.HandleUserInfoRequest(new UserInfoRequest() { UserId = 1024 });
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(response.PhoneNumber));
        }
    }
}
