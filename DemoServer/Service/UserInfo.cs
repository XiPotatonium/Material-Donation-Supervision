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
            SqlCommand com = new SqlCommand($"select PhoneNumber, HomeAddress, UserType from Users where UserId = {request.UserId}", Connect.Connection);
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
            SqlCommand com = new SqlCommand($"UPDATE userinfo SET HomeAddress = '{request.HomeAddress}' and PhoneNumber = ", Connect.Connection);
            com.ExecuteNonQuery();
            return new VoidResponse();
        }

        /*public static RegisterResponse HandleRegisterRequest(RegisterRequest request)
		{
			//TODO:数据库
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			int c = (int)(Convert.ToInt64(request.PhoneNumber) % pow(10, 9));
			SqlCommand com = new SqlCommand("insert into Users(PhoneNumber,Passwords,UserId) values ('"
					+ request.PhoneNumber + "','" + request.Password + "'," +  c +
					+ ")", con);
            con.Close();
			return new RegisterResponse()
			{
				UserID = c
			};
		}*/
    }
}
