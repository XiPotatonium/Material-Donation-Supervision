using System;
using DTO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server
{
    class UserInfoService
    {
        public int UserId { get; set; }
        public LoginResponse HandleLoginRequest(LoginRequest request)
        {
            //TODO: 数据库查询			
            // 指定SQL语句
            SqlCommand com = new SqlCommand
                ($"select UserId from Users where PhoneNumber='{request.PhoneNumber}' and Passwords='{request.Password}'"
                , Connect.Connection);
            // 建立SqlDataAdapter和DataSet对象
            SqlDataAdapter da = new SqlDataAdapter(com);
            using (DataSet ds = new DataSet())
            {
                da.Fill(ds, "Users");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    return new LoginResponse()
                    {
                        UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString())
                    };
                }
                else
                {
                    return new LoginResponse()
                    {
                        UserId = -1
                    };
                }
            }

        }


        public UserInfoResponse HandleUserInfoRequest(UserInfoRequest request)
        {
            SqlCommand com = new SqlCommand($"select PhoneNumber, HomeAddress, UserType from Users where UserId = {UserId}", Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            using (DataSet ds = new DataSet())
            {
                da.Fill(ds, "Users");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    var rows = ds.Tables[0].Rows;
                    return new UserInfoResponse()
                    {
                        HomeAddress = rows[0]["HomeAddress"].ToString(),
                        PhoneNumber = rows[0]["PhoneNumber"].ToString(),
                        UserType = (UserType)int.Parse(rows[0]["UserType"].ToString())
                    };
                }
                else
                {
                    return new UserInfoResponse();
                }
            }
        }

        public VoidResponse HandleModifyRequest(UserInfoModifyRequest request)
        {
            SqlCommand com = new SqlCommand($"UPDATE Users SET HomeAddress = '{request.HomeAddress}', PhoneNumber = '{request.PhoneNumber}' WHERE UserID = {UserId} ", Connect.Connection);
            com.ExecuteNonQuery();
            return new VoidResponse();
        }

        public RegisterResponse HandleRegisterRequest(RegisterRequest request)
		{
            SqlCommand com = new SqlCommand($"insert into Users(PhoneNumber,Passwords,HomeAddress) values('{request.PhoneNumber}','{request.Password}','暂无')"
                ,Connect.Connection);
            com.ExecuteNonQuery();
            SqlCommand ncom = new SqlCommand($"select UserID from Users where PhoneNumber = '{request.PhoneNumber}' and Passwords = '{request.Password}'"
                , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(ncom);
            using (DataSet ds = new DataSet())
            {
                da.Fill(ds, "Users");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    var rows = ds.Tables[0].Rows;
                    return new RegisterResponse()
                    {
                        UserId = int.Parse(rows[0]["UserID"].ToString())
                    };
                }
                else
                {
                    return new RegisterResponse()
                    {
                        UserId = -1
                    };
                }
            }
        }

    }
}
