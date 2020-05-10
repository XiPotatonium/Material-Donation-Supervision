using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //student requestData;
            string requestData = "1234";                 // 发送的数据
            //requestData.mts = 2;
            //requestData.explain = "1234567";
            
            string url = "http://localhost:6666";  // 服务器地址

            //string result = HttpGet(url, requestData);    // GET请求
            string result = HttpPost(url, requestData);     // POST请求

            Console.WriteLine("响应结果：" + result);
            Console.ReadKey();
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
            StreamReader sReader = new StreamReader(responseStream, Encoding.Default);
            String result = sReader.ReadToEnd();
            sReader.Close();
            responseStream.Close();

            return result;
        }

        private static String HttpPost(string url, string requestData)
        {
            // 实例化请求对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestData.Length;

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
            String result = sReader.ReadToEnd();
            sReader.Close();
            responseStream.Close();

            return result;
        }
    }
}