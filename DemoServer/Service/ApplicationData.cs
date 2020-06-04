using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace MDS.Server.Service
{
	
	//public int ApplicationID = 10000;
	class ApplicationDataService
	{
		public int UserId { get; set; }
		public static int ApplicationID = 10000;
		public enum ApplicationState
		{
			Aborted,        // 撤销的申请
			Applying,
			Delivering,
			Received,
			Done
		}

		public static GetApplicationListResponse HandleGetApplicationListRequest(GetApplicationListRequest request)
		{

			//TODO: 数据库操作
			string constr = "Server=.;DataBase=物资调配;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand
				("select ApplyID,ApplyGUID,MaterialName,MaterialQuantity,ApplicationState,StateIndex,StartTime from Applications where ApplierID= "
					+ UserId + "", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Applications");
			if (n != 0)
			{
				List<Item> Itema = new List<Item>();

				for (int j = 0; j < ds.table[0].Rows.Count; j++)
				{
					Itema.Add(
						new Item()
					{
						ID = ds.table[0].Rows[j]["ApplyID"],
					    GUID = ds.table[0].Rows[j]["ApplyGUID"].ToString(),
						Name = ds.table[0].Rows[j]["MaterialName"].ToString(),
						Quantity = ds.table[0].Rows[j]["MaterialQuantity"],
						State = ( ApplicationState )ds.table[0].Rows[j]["StateIndex"],
                      
						StartTime = Convert.ToDateTime(ds.table[0].Rows[j]["StartTime"].ToString())

						
					}
	               );

				}
				return new GetApplicationListResponse()
				{
					Items = Itema.clone()


			    };

			}
		}




		public static GetApplicationDetailResponse HandleGetApplicationDetailRequest(GetApplicationDetailRequest request)
		{
			//TODO：数据库操作
			string constr = "Server=.;DataBase=物资调配;" +
							"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand
				("select ApplyID,ApplyGUID,ApplierAddress from Applications where ApplyID="
					+ request.ApplicationId + "", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Applications");
			if (n != 0)
			{
				return new GetApplicationListResponse()
				{
					Address = ds.table[0].Rows[0]["ApplierAddress"]
				};

			}
		}





		public static AvailableApplicationMaterialResponse HandleAvailableApplicationMaterialRequest(AvailableApplicationMaterialRequest request)
		{
			//TODO：数据库操作
			string constr = "Server=.;DataBase=物资调配;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand("Select MaterialID,MaterialName,MaterialDescription,MaterialConstraint from Materials "
					, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			da.Fill(ds, "Materials");
			List<Item> Itema = new List<Item>();

			for (int j = 0; j < ds.table[0].Rows.Count; j++)
			{
				Itema.Add(
					new Item()
					{
						Id = ds.table[0].Rows[j]["MaterialID"],

						Name = ds.table[0].Rows[j]["MaterialName"].ToString(),
						Description = ds.table[0].Rows[j]["MaterialDescription"].ToString(),
						Constraint = ds.table[0].Rows[j]["MaterialConstraint"].ToString()


					}
			   );

			}
			return new AvailableDonationMaterialResponse()
			{
				Items = Itema.clone()
			};
		}






	


		public static NewApplicationResponse HandleNewApplicationRequest(NewApplicationRequest request)
		{
			
			//TODO: 数据库查询
			string constr = "Server=.;DataBase=物资调配;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand
				("select MaterialId,MaterialName,MaterilaQuantity from Materials where MaterialId= "
					+ request.MaterialId + "", con);
			// 建立SqlDataAdapter和DataSet对象
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Materials");
			if (n != 0) {



				if (request.MaterialQuantity <= ds.table[0].Rows[0]["MaterialQuantity"])
				{
					ApplicationID = ApplicationID + 1;

					string a = System.Guid.NewGuid().ToString("D");


					SqlCommand coma = new SqlCommand("Update Materials set MaterilaQuantity = MaterialQuantity -" +
						request.MaterialQuantity + " where MaterialId = " + request.MaterialId + "",con);
					SqlCommand comb = new SqlCommand("insert into Applications(ApplyID,ApplyGUID, ApplierId, ApplierAddress,MaterialName,MaterialQuantity,ApplicationState,StateIndex,StartTime) values("
					+ ApplicationID + ",'" + a + "'," + UserId + ",'" + request.Address + "','" + ds.table[0].Rows[0]["MaterialName"]
                    + "',"+ request.MaterialQuantity + ",'Applying"+ "', 1 ,'" + DataTime.Now.ToString() + "')", con);
					con.close();

				
					return new NewApplicationResponse()
					{
					    Item.ID= ApplicationID,
						Item.GUID = a ,
						Item.Name = ds.table[0].Rows[0]["MaterialName"],
						Item.Quantity = request.MaterialQuantity,
						Item.State = applying,
						Item.StartTime = DataTime.Now


					};
					//con.close();
				}
		   }
	    }





		public static VoidResponse HandleCancelApplicationRequest(CancelApplicationRequest request)
		{
			//TODO:数据库操作
			//TODO: 数据库查询
			string constr = "Server=.;DataBase=物资调配;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			SqlCommand com = new SqlCommand("Update Applications set ApplicationState = Aborted ,StateIndex = 0 where ApplyID = " + request.ApplicationId + "", con);
			con.close();
			return new VoidResponse();
		}





		public static VoidResponse HandleConfirmApplicationDoneRequest(ConfirmApplicationDoneRequest request)
		{
			//TODO:数据库操作
			string constr = "Server=.;DataBase=物资调配;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			SqlCommand com = new SqlCommand("Update Applications set ApplicationState = Done, StateIndex = 4  where ApplyID = " + request.ApplicationId + "", con);
			con.close();
			return new VoidResponse();
			
		}
	}
}
