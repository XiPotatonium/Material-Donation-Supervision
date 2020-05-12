using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;

namespace MDS.Client
{
    public static class NetworkHelper
    {
        static NetworkHelper() {
        }

        // 测试用，正式使用的时候要重写
        //public static Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> req)
        //{
        //    throw new NotImplementedException();
        //}

        // TODO 网络接口
        // req可以传DTO里面的LoginRequest来测试，返回LoginResponse就好了
        public static TResponse Get<TResponse>(IReturn<TResponse> req)
        {
            throw new NotImplementedException();
        }
    }
}
