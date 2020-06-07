using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server.Service
{
    public class DeliveryDataService
    {
		public int UserId { set; get; }

		public DeliveryListNumResponse HandleDeliveryListNumRequest(DeliveryListNumRequest request)
		{
			SqlCommand com = new SqlCommand
				($"select DeliveryId from Delivery where DeliverymanId={request.DelivererId} and DeliveryState={(int)request.State}"
				, Connect.Connection);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "DeliveryListNum");
			int n = ds.Tables[0].Rows.Count;
			ds.Dispose();
			return new DeliveryListNumResponse() {
				Num = n
			};

		}
		public DeliveryListResponse HandleDeliveryListRequest(DeliveryListRequest request)
		{
			SqlCommand com = new SqlCommand
				($"select * from Delivery inner join Tranc on Delivery.TransactionId = Tranc.TransactionId inner join Materials on Tranc.MaterialId=Materials.MaterialId inner join Users on Delivery.DeliveryAdminId=Users.UserID where DeliverymanId={(int)request.DelivererId} and DeliveryState={(int)request.State}"
				, Connect.Connection);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "DeliveryList");

			List<Item> items = new List<Item>();

			for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
			{
				Item it = new Item()
				{
					GUID = (int)ds.Tables[0].Rows[j]["DeliveryId"],
					Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
					Quantity = (int)ds.Tables[0].Rows[j]["MaterialQuantity"],
					State = (DeliveryState)ds.Tables[0].Rows[j]["TransactionState"]
				};
				if ((TransactionType)ds.Tables[0].Rows[j]["TransactionType"] == TransactionType.DONATION)
				{
					it.StartID = (int)ds.Tables[0].Rows[j]["UserId"];
					it.FinishID = (int)ds.Tables[0].Rows[j]["DeliveryAdminId"];
					it.Departure = ds.Tables[0].Rows[j]["Address"].ToString();
					it.Destination = ds.Tables[0].Rows[j]["HomeAddress"].ToString();
				}
				else
				{
					it.StartID = (int)ds.Tables[0].Rows[j]["DeliveryAdminId"];
					it.FinishID = (int)ds.Tables[0].Rows[j]["UserId"];
					it.Departure = ds.Tables[0].Rows[j]["HomeAddress"].ToString();
					it.Destination = ds.Tables[0].Rows[j]["Address"].ToString();
				}
				items.Add(it);
			}
			ds.Dispose();
			return new DeliveryListResponse() { Items = items };
		}
        public DeliveryMoveResponse HandleDeliveryMoveRequest(DeliveryMoveRequest request)
		{
			SqlCommand com = new SqlCommand
				($"select * from Delivery inner join Tranc on Delivery.TransactionId = Tranc.TransactionId where DeliverymanId={(int)request.DelivererId} and TransactionId={(int)request.GUID}"
				, Connect.Connection);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "DeliveryMove");

			if (ds.Tables[0].Rows.Count == 0)
			{
				return new DeliveryMoveResponse()
				{
					Check = 3 //表示其它错误
				};
			}
			int secureId;
			int quantity = (int)ds.Tables[0].Rows[0]["MateriaQuantity"];
			int materialId = (int)ds.Tables[0].Rows[0]["MaterialId"];
			if ((DeliveryState)ds.Tables[0].Rows[0]["DeliveryState"] == DeliveryState.Waiting)
			{
				if ((TransactionType)ds.Tables[0].Rows[0]["TransactionType"] == TransactionType.DONATION)
				{
					secureId = (int)ds.Tables[0].Rows[0]["UserId"];
					if (request.SecureId == secureId)
					{
						SqlCommand cmd2 = new SqlCommand(
							$"update Delivery " +
							$"set DeliveryState = {DeliveryState.Processing}" +
							$"where TransacyionId = {(int)request.GUID}",
							Connect.Connection);
						cmd2.ExecuteNonQuery();
						return new DeliveryMoveResponse()
						{
							Check = 0 //表示成功
						};
					}
				}
				else
				{
					secureId = (int)ds.Tables[0].Rows[0]["DeliveryAdminId"];
					if (request.SecureId == secureId)
					{
						SqlCommand cmd = new SqlCommand(
							$"update Materials " +
							$"set MaterialQuantity = MaterialQuantity - {quantity}" +
							$"where MaterialId = {materialId}",
							Connect.Connection);
						cmd.ExecuteNonQuery();
						SqlCommand cmd2 = new SqlCommand(
							$"update Delivery " +
							$"set DeliveryState = {DeliveryState.Processing}" +
							$"where TransacyionId = {(int)request.GUID}",
							Connect.Connection);
						cmd2.ExecuteNonQuery();
						return new DeliveryMoveResponse()
						{
							Check = 0 //表示成功
						};
					}
				}
			}
			else if ((DeliveryState)ds.Tables[0].Rows[0]["DeliveryState"] == DeliveryState.Processing)
			{
				if ((TransactionType)ds.Tables[0].Rows[0]["TransactionType"] == TransactionType.DONATION)
				{
					secureId = (int)ds.Tables[0].Rows[0]["DeliveryAdminId"];
					if (request.SecureId == secureId)
					{
						SqlCommand cmd = new SqlCommand(
							$"update Materials " +
							$"set MaterialQuantity = MaterialQuantity + {quantity}" +
							$"where MaterialId = {materialId}",
							Connect.Connection);
						cmd.ExecuteNonQuery();
						SqlCommand cmd2 = new SqlCommand(
							$"update Delivery " +
							$"set DeliveryState = {DeliveryState.Finished}" +
							$"where TransacyionId = {(int)request.GUID}",
							Connect.Connection);
						cmd2.ExecuteNonQuery();
						return new DeliveryMoveResponse()
						{
							Check = 0 //表示成功
						};
					}
				}
				else
				{
					secureId = (int)ds.Tables[0].Rows[0]["UserId"];
					SqlCommand cmd2 = new SqlCommand(
						$"update Delivery " +
						$"set DeliveryState = {DeliveryState.Finished}" +
						$"where TransacyionId = {(int)request.GUID}",
						Connect.Connection);
					cmd2.ExecuteNonQuery();
					if (request.SecureId == secureId)
					{
						return new DeliveryMoveResponse()
						{
							Check = 0 //表示成功
						};
					}
				}
			}
			else
			{
				return new DeliveryMoveResponse()
				{
					Check = 2 //表示订单状态非Waiting或Processing
				};
			}
			return new DeliveryMoveResponse()
			{
				Check = 1 //表示验证ID错误
			};
		           
        }
		public DeliveryMoveResponse HandleDeliveryApplyRequest(DeliveryApplyRequest request)
		{
			
			SqlCommand com = new SqlCommand
				($"select * from Delivery where TransactionId={(int)request.TransactionId} and DeliveryState={DeliveryState.Alone}"
				, Connect.Connection);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "DeliveryApply");
			if (ds.Tables[0].Rows.Count == 0)
			{
				return new DeliveryMoveResponse()
				{
					Check = 3 //表示其它错误
				};
			}
			SqlCommand cmd = new SqlCommand(
							$"update Delivery " +
							$"set DelivermanId = {request.DelivermanId}" +
							$", DeliveryState = {DeliveryState.Checking}" +
							$"where TransactionId = {request.TransactionId}",
							Connect.Connection);
			cmd.ExecuteNonQuery();
			return new DeliveryMoveResponse()
			{
				Check = 0
			};
		}

	}
}
