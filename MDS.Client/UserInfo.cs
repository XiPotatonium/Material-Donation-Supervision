using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Client
{
    public static class UserInfo
    {
        public static int Id { set; get; } = -1;
        public static string PhoneNumber { set; get; }
        public static string HomeAddress { set; get; }
        public static UserType UserType { set; get; }
    }
}
