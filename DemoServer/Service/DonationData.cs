using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server
{

	public class DonationDataService
	{
		
		public int UserId { get; set; }
		public static int DonationID = 100000;
		public static GetDonationListResponse HandleGetDonationListRequest(GetDonationListRequest request)
		{

			//TODO: 数据库操作
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand
				("select DonateID,DonateGUID,MaterialName,MaterialQuantity,DonationState,StateIndex,StartTime from Donation where DonatorID= "
					+ UserId + "", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Donation");
			if (n != 0)
			{
				List<Item> Itema = new List<Item>();

				for (int j = 0; j < ds.table[0].Rows.Count; j++)
				{
					Itema.Add(
						new Item()
						{
							ID = ds.table[0].Rows[j]["DonateID"],
							GUID = ds.table[0].Rows[j]["DonateGUID"].ToString(),
							Name = ds.table[0].Rows[j]["MaterialName"].ToString(),
							Quantity = ds.table[0].Rows[j]["MaterialQuantity"],
							State = (DonationState)ds.table[0].Rows[j]["StateIndex"],

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




		public static GetDonationDetailResponse HandleGetDonationDetailRequest(GetDonationDetailRequest request)
		{
			//TODO: 数据库
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand("Select DonateID,DonateGUID,DonatorAddress from Donation where DonateID="
					+ request.DonationId + "", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Donation");
			if (n != 0)
			{
				return new GetApplicationListResponse()
				{
					Address = ds.table[0].Rows[0]["DonatorAddress"]
				};

			}
		}





		public static AvailableDonationMaterialResponse HandleAvailableDonationMaterialRequest(AvailableDonationMaterialRequest request)
		{
			//TODO: 数据库
			//TODO：数据库操作
			string constr = "Server=.;DataBase=Material;" +
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




		
		public static NewDonationResponse HandleNewDonationRequest(NewDonationRequest request)
		{
			//TODO: 数据库查询
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			
			
			SqlCommand com = new SqlCommand("Select MaterialID,MaterialName,MaterialQuantity from Materials where " +
				"MaterialId = " + request.MaterialId + " ", con);
            
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();

			int n = da.Fill(ds, "Materials");
			if (n != 0)
			{
				DonationID = DonationID + 1;
				string d = System.Guid.NewGuid().ToString("D");
				SqlCommand coma = new SqlCommand("Update Materials set MaterilaQuantity = MaterialQuantity + " +
						request.Quantity + " where MaterialId = " + request.MaterialId + " ", con);

				SqlCommand comb = new SqlCommand("insert into Donations(DonateID,DonateGUID, DonatorId, DonatorAddress,MaterialName,MaterialQuantity,DonationState，StateIndex," +
					"StartTime) values("
				+ DonationID + ",'" + d + "','" + request.Address + "','" + ds.table[0].Rows[0]["MaterialName"]
				+ "'," + request.Quantity + ",'Applying" + "', 1 ,'" + DataTime.Now.ToString() + "')", con);
				con.close();
				return new NewApplicationResponse()
				{
					Item.ID = DonationID ,
					Item.GUID = d ,
					Item.Name = ds.table[0].Rows[0]["MaterialName"],
					Item.Quantity = request.MaterialQuantity,
					Item.State = Applying,
					Item.StartTime = DataTime.Now


				};
				//con.close();
				//}
			}
			else {
				DonationID = DonationID + 1;
				string d = System.Guid.NewGuid().ToString("D");
				SqlCommand comc = new SqlCommand("insert into Materials(MaterialId,MaterialName,MaterialQuantity) Values" +
					"(" + request.MaterialId + "," + request.Quantity + ")", con);

				SqlCommand comb = new SqlCommand("insert into Donations(DonateID,DonateGUID, DonatorId, DonatorAddress,MaterialName,MaterialQuantity,DonationState，StateIndex," +
					"StartTime) values("
				+ DonationID + ",'" + d + "','" + request.Address + "','" + ds.table[0].Rows[0]["MaterialName"]
				+ "'," + request.Quantity + ",'Applying" + "', 1 ,'" + DataTime.Now.ToString() + "')", con);

				con.close();
				return new NewApplicationResponse()
				{
					Item.ID = DonationID,
					Item.GUID = d,
					Item.Name = ds.table[0].Rows[0]["MaterialName"],
					Item.Quantity = request.MaterialQuantity,
					Item.State = Applying,
					Item.StartTime = DataTime.Now


				};


			}
		}







		public static VoidResponse HandleCancelDonationRequest(CancelDonationRequest request)
		{
			//TODO: 数据库
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			SqlCommand com = new SqlCommand("Update Donations set DonationState = Aborted ,StateIndex = 0 where DonateGUID = " + request.DonationId + "", con);
			con.close();
			return new VoidResponse();
			//return new VoidResponse();
		}
	}

}
