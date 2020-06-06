using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server
{
    public class AdminDataService
    {
        public int UserId { get; set; }
		
		/*public AssignTasksResponse HandleAssignTasksRequest(AssignTasksRequest request)
		{
			string constr = "Server=.;DataBase=Material;" +
			  "Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句

			SqlCommand com = new SqlCommand
				("select ApplyGUID,ApplierAddress from Applications where ApplyGUID= '"
					+ request.TaskID + "'", con);
			SqlCommand coma = new SqlCommand
				("select HomeAddress from Users where UserID= "
					+ request.AdminID + "", con);
			SqlCommand comb = new SqlCommand
				("select DonateGUID,DonatorAddress from Donation where DonateGUID= '"
					+ request.TaskID + "'", con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			SqlDataAdapter da2 = new SqlDataAdapter(coma);
			DataSet ds2 = new DataSet();
			SqlDataAdapter da3 = new SqlDataAdapter(comb);
			DataSet ds3 = new DataSet();
			da2.Fill(ds2, "Users");
			da3.Fill(ds3, "Donation");
			int n = da.Fill(ds, "Applications");
			if (n != 0)
			{
				SqlCommand comc = new SqlCommand
				("insert into Delivery(DeliveryGUID,DeliveryerId,AdminId,DeliveryState,StateIndex,DeliverDeparture,DeliverDestination)" +
					  " values ('" + request.TaskID + "', " + request.DeliverID + ","+ request.AdminID +",'Waiting'" + "," + 0 + ",'" + ds2.table[0].Rows[0]["HomeAddress"]
						  + "','" + ds.table[0].Rows[0]["ApplierAddress"] + "')", con);


				return new AssignTasksResponse()
				{
					result = 0

				};
			}

			else
			{

				SqlCommand comc = new SqlCommand
				("insert into Delivery(DeliveryGUID,DeliveryerId,AdminId,DeliveryState,StateIndex,DeliverDeparture,DeliverDestination,DeliverStartTime)" +
					  " values ('" + request.TaskID + "', " + request.DeliverID + "," + request.AdminID + ",'Waiting'" + "," + 0 + ",'" + ds3.table[0].Rows[0]["DonatorAddress"]
						  + "','" + ds2.table[0].Rows[0]["HomeAddress"] +"','" + DataTime.Now.ToString() + "')", con);


				return new AssignTasksResponse()
				{
					result = 0

				};


			}

		}*/

		

	}
	
}
