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
		public static LoginResponse HandleLoginRequest(LoginRequest request)
        {
			//TODO: 数据库查询
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand
				("select PhoneNumber,Passwords,UserId from Users where PhoneNumber='" 
				    + request.PhoneNumber + "' and Passwords='" + 
					request.Password + "'", con);
			// 建立SqlDataAdapter和DataSet对象
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Users");
			if (n != 0)
			{				
                con.Close();
				return new LoginResponse()
				{
					UserId = ds.table[0].Rows[0]["UserId"].ToString()
					//UserId = (request.Password + request.PhoneNumber).Length
				};
			}
			
		}
  
		



        public static UserInfoResponse HandleUserInfoRequest(UserInfoRequest request)
        {
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			SqlCommand com = new SqlCommand("select PhoneNumber, Passwords, HomeAddress, UserType from Users where UserId= "
				  + request.UserId + " ", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Users");
			if (n != 0)
				
                con.Close();
				//TODO: 数据库查询
				return new UserInfoResponse()
				{
					PhoneNumber = ds.table[0].Rows[0]["PhoneNumber"].ToString(),
					Password = ds.table[0].Rows[0]["Passwords"].ToString(),
					HomeAddress = ds.table[0].Rows[0]["HomeAddress"].ToString(),
					UserType = ds.table[0].Rows[0]["UserType"].ToString()

            };
			
		}






        public static VoidResponse HandleModifyRequest(UserInfoModifyRequest request)
        {
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			SqlCommand com = new SqlCommand
				("select PhoneNumber from Users where PhoneNumber='"
					+ request.PhoneNumber +  "'", con);
			// 建立SqlDataAdapter和DataSet对象
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Users");
			if (n != 0)
				SqlCommand com = new SqlCommand("Update Users Set HomeAddress='" + request.HomeAddress +
				 "'where PhoneNumber='" + request.PhoneNumber + "'", con);
            con.Close();
			//TODO: 数据库操作
			return new VoidResponse();
        }

		




		public static RegisterResponse HandleRegisterRequest(RegisterRequest request)
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
		}
	}
}
