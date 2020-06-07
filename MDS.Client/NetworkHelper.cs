using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;

namespace MDS.Client
{
    public static class NetworkHelper
    {
        private static string ServerURL = "http://localhost:6666";

        static NetworkHelper() {
        }

        // 线程化
        public static async Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> req)
        {
            return await Task.Run(() => { return Get(req); });
        }

        private static TResponse Get<TResponse>(IReturn<TResponse> req)
        {
            string requestData = objecttostring(req);
            Console.WriteLine(requestData.Length);
            // 实例化请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServerURL);
            request.Method = "POST";
            request.ContentType = "text/html";
            request.ContentLength = requestData.Length;// 2 *requestData.Length+4; //有没有比+8更好的办法？
            request.Headers["UserId"] = UserInfo.Id.ToString();         // 这里加了UserId
            request.Timeout = 10000;        // 超时10s钟

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
            TResponse result = (TResponse)DeserializeObject(rbytes);
            return result;
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
                inputString += b.ToString("X2");
            }
            return inputString;
        }
    }
}
