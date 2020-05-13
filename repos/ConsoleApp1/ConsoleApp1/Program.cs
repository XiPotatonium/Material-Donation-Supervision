using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        { 
            double rd = 12345;                 // 发送的数据
            
            string url = "http://localhost:6666";  // 服务器地址
            //string requestData = objecttostring(rd);
            //string result = HttpGet(url, requestData);    // GET请求
            object result = HttpPost(url, rd);     // POST请求

            Console.WriteLine("响应结果：" + result);
            Console.ReadKey();
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
        public static byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
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
               inputString += b.ToString("X2") ;
            }
            return inputString;
        }
        private static String HttpGet(string url, string requestData)
        {
            // 实例化请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + requestData);
            request.Method = "GET";
            request.ContentType = "text/html; charset=UTF-8";

            // 实例化响应对象，获取响应信息
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8);
            String result = sReader.ReadToEnd();
            sReader.Close();
            responseStream.Close();

            return result;
        }

        private static object HttpPost(string url, object rd)
        {
            string requestData = objecttostring(rd);
            Console.WriteLine(requestData.Length);
            // 实例化请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/html";
            request.ContentLength = requestData.Length;// 2 *requestData.Length+4; //有没有比+8更好的办法？

            // 发送请求数据
            Stream requestStream = request.GetRequestStream();
            StreamWriter sWriter = new StreamWriter(requestStream, Encoding.Default);
            sWriter.Write(requestData);
            sWriter.Flush();
            sWriter.Close();
            requestStream.Close();

            // 实例化响应对象，获取响应信息
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader sReader = new StreamReader(responseStream, Encoding.Default);
            string responsestr = sReader.ReadToEnd();
            sReader.Close();
            responseStream.Close();
            byte[] rbytes = HexStringToByteArray(responsestr);
            object result = DeserializeObject(rbytes);
            return result;
        }
    }
}