using DTO;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using MDS.Server;
using System.Threading;
using System.Threading.Tasks;
using MDS.Server.Service;

namespace DemoServer
{
    class Program
    {
        static void PrintConsoleLog(string s)
        {
            Console.WriteLine(DateTime.Now.ToString() + ":" + s);
        }
        public static object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }
        public static string objecttostring(object obj)
        {
            if (obj == null)
                return null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            string inputString = null;
            foreach (byte b in bytes)
            {
                inputString += b.ToString("X2");
            }
            return inputString;
        }

        public static byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public static void HandleRequest(HttpListenerContext context)
        {
            try
            {
                HttpListenerRequest request = context.Request;
                string postData = new StreamReader(request.InputStream, Encoding.UTF8).ReadToEnd();
                byte[] rbytes = HexStringToByteArray(postData);
                object recv = DeserializeObject(rbytes);
                Console.WriteLine("收到请求：" + recv);
                int id = int.Parse(request.Headers["UserId"]);
                HttpListenerResponse response = context.Response;//响应
                
                string resp = "响应";
                string responseBody = objecttostring(resp);
                // UsrInfoService
                UserInfoService UserInfoService = new UserInfoService() { UserId = id };
                DeliveryDataService DeliveryDataService = new DeliveryDataService() { UserId = id };
                ApplicationDataService ApplicationDataService = new ApplicationDataService() { UserId = id };
                DonationDataService DonationDataService = new DonationDataService() { UserId = id };
                if (recv is LoginRequest loginRequest)
                {
                    responseBody = objecttostring(UserInfoService.HandleLoginRequest(loginRequest));
                }
                else if (recv is UserInfoRequest userInfoRequest)
                {
                    responseBody = objecttostring(UserInfoService.HandleUserInfoRequest(userInfoRequest));
                }
                else if (recv is RegisterRequest registerRequest)
                {
                    responseBody = objecttostring(UserInfoService.HandleRegisterRequest(registerRequest));
                }
                else if (recv is UserInfoModifyRequest userInfoModifyRequest)
                {
                    responseBody = objecttostring(UserInfoService.HandleModifyRequest(userInfoModifyRequest));
                }
                else if (recv is GetDonationListRequest getDonationListRequest)
                {
                    responseBody = objecttostring(DonationDataService.HandleGetDonationListRequest(getDonationListRequest));
                }
                else if (recv is GetDonationDetailRequest getDonationDetailRequest)
                {
                    responseBody = objecttostring(DonationDataService.HandleGetDonationDetailRequest(getDonationDetailRequest));
                }
                else if (recv is AvailableDonationMaterialRequest availableDonationMaterialRequest)
                {
                    responseBody = objecttostring(DonationDataService.HandleAvailableDonationMaterialRequest(availableDonationMaterialRequest));
                }
                else if (recv is NewDonationRequest newDonationRequest)
                {
                    responseBody = objecttostring(DonationDataService.HandleNewDonationRequest(newDonationRequest));
                }
                else if (recv is CancelDonationRequest cancelDonationRequest)
                {
                    responseBody = objecttostring(DonationDataService.HandleCancelDonationRequest(cancelDonationRequest));
                }
                
                // DeliveryDataService
                else if (recv is DeliveryListNumRequest deliveryListNumRequest)
                {
                    responseBody = objecttostring(DeliveryDataService.HandleDeliveryListNumRequest(deliveryListNumRequest));
                }
                else if (recv is DeliveryListRequest deliveryListRequest)
                {
                    responseBody = objecttostring(DeliveryDataService.HandleDeliveryListRequest(deliveryListRequest));
                }
                else if (recv is DeliveryMoveRequest deliveryMoveRequest)
                {
                    responseBody = objecttostring(DeliveryDataService.HandleDeliveryMoveRequest(deliveryMoveRequest));
                }
                else if (recv is DeliveryApplyRequest deliveryApplyRequest)
                {
                    responseBody = objecttostring(DeliveryDataService.HandleDeliveryApplyRequest(deliveryApplyRequest));
                }

                else if (recv is AvailableApplicationMaterialRequest availableApplicationMaterialRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleAvailableApplicationMaterialRequest(availableApplicationMaterialRequest));
                }
                else if (recv is GetApplicationDetailRequest getApplicationDetailRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleGetApplicationDetailRequest(getApplicationDetailRequest));
                }
                else if (recv is GetApplicationListRequest getApplicationListRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleGetApplicationListRequest(getApplicationListRequest));
                }
                else if (recv is NewApplicationRequest newApplicationRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleNewApplicationRequest(newApplicationRequest));
                }
                else if (recv is CancelApplicationRequest cancelApplicationRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleCancelApplicationRequest(cancelApplicationRequest));
                }
                else if (recv is ConfirmApplicationDoneRequest confirmApplicationDoneRequest)
                {
                    responseBody = objecttostring(ApplicationDataService.HandleConfirmApplicationDoneRequest(confirmApplicationDoneRequest));
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                }

                response.ContentLength64 = responseBody.Length;
                response.ContentType = "text/html";
                //输出响应内容
                Stream output = response.OutputStream;
                using (StreamWriter sw = new StreamWriter(output))
                {
                    sw.Write(responseBody);
                    sw.Flush();
                    sw.Close();
                }
                Console.WriteLine("响应结束");
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
           
        }
        static void test()
        {
            // Test.HandleNewApplicationRequestTest();
            // Test.HandleAvailableApplicationMaterialRequestTest();
            // Test.HandleGetApplicationDetailRequestTest();
            // Test.HandleGetApplicationListRequestTest();
            // Test.UserLoginTest();
            // Test.UserInfoTest();
            // Test.ModifyRequestTest();
            // Test.RegisterRequestTest();
        }       
        static void Main(string[] args)
        {
            try
            {
                Connect.ConnectDatabase();
                PrintConsoleLog(DateTime.Now.ToString() + ":成功连接到云数据库");
                test();
                using (HttpListener listener = new HttpListener())
                {
                    listener.Prefixes.Add("http://localhost:6666/");
                    listener.Start();
                    PrintConsoleLog("开始监听");
                    while (true)
                    {
                        try
                        {
                            HttpListenerContext context = listener.GetContext();//阻塞
                            Task.Factory.StartNew(() => HandleRequest(context));                           
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                PrintConsoleLog("程序异常，请重新打开程序：" + err.Message);
            }
        }
      
    }
}
