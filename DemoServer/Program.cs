using DTO;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DemoServer
{
    class Program
    {
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

        static void Main(string[] args)
        {
            try
            {
                using (HttpListener listener = new HttpListener())
                {
                    listener.Prefixes.Add("http://localhost:6666/");
                    listener.Start();
                    Console.WriteLine("开始监听");
                    while (true)
                    {
                        try
                        {
                            HttpListenerContext context = listener.GetContext();//阻塞
                            HttpListenerRequest request = context.Request;

                            string postData = new StreamReader(request.InputStream, Encoding.UTF8).ReadToEnd();
                            byte[] rbytes = HexStringToByteArray(postData);

                            object recv = DeserializeObject(rbytes);
                            Console.WriteLine("收到请求：" + recv);

                            HttpListenerResponse response = context.Response;//响应
                            string resp = "响应";
                            string responseBody = objecttostring(resp);

                            if (recv is LoginRequest)
                            {
                                responseBody = objecttostring(HandleLoginRequest((LoginRequest)recv));
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
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("程序异常，请重新打开程序：" + err.Message);
            }
        }

        private static LoginResponse HandleLoginRequest(LoginRequest request)
        {
            return new LoginResponse()
            {
                UserId = (request.Password + request.UserName).Length
            };
        }
    }
}
