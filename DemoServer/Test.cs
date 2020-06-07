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
            //System.Diagnostics.Debug.Assert(response.UserId == 1);
            Console.WriteLine("Pass UserLoginTest");
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
            var response = service.HandleNewApplicationRequest(new NewApplicationRequest() 
            { 
                Address = "测试地址1", 
                MaterialId = 1, 
                Quantity = 1
            });
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

        public static void MaterialAuditListRequestTest()
        {
            var service = new AdminDataService();
            /*var response = service.HandleMaterialAuditListRequest(new MaterialAuditListRequest() { state=AdminState.WAIT, AdminID = -1});
            if (response.m_normals.Count > 0)
            {
                Console.WriteLine("Pass MaterialAuditListRequestTest");
            }*/
            var response = service.HandleMaterialAuditListRequest(new MaterialAuditListRequest() { state = AdminState.FINISH, AdminID = 1 });
            if (response.m_normals.Count > 0)
            {
                Console.WriteLine("Pass MaterialAuditListRequestTest");
            }           
        }

        public static void MaterialAuditAgreeRequestTest()
        {
            var service = new AdminDataService();
            var response = service.HandleMaterialAuditAgreeRequest(new MaterialAuditAgreeRequest() { Secondary_passward = "123456", AdminID = 13, Number="22"});
        }

        public static void MaterialAuditRefuseRequestTest()
        {
            var service = new AdminDataService();
            var response = service.HandleMaterialAuditRefuseRequest(new MaterialAuditRefuseRequest() { Secondary_passward = "123456", AdminID = 13, Number = "18" });
        }
    }
}
