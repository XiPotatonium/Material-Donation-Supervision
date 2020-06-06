using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server
{
    public class DeliveryDataService
    {
        public int UserId { get; set; }

        public DeliveryListNumResponse HandleDeliveryListNumRequest(DeliveryListNumRequest request)
        {
            //TODO:数据库
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand("Select DeliveryGUID,AdminId,DeliverDeparture,DeliverDestination,StateIndex,DeliverStartTime from Delivery where DeliveryerId = " +request.DelivererId
				     + "and StateIndex = "  + (int)(request.State)
					, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "Delivery");
            return new DeliveryListNumResponse(){
				   num = ds.table[0].Rows.Count 

				};
        }






        public DeliveryListResponse HandleDeliveryListRequest(DeliveryListRequest request)
        {
            //TODO:数据库
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand("Select DeliveryGUID,AdminId,DeliverDeparture,DeliverDestination,StateIndex,DeliverStartTime,DeliverFinishTime from Delivery where DeliveryerId = " +request.DelivererId
				     + "and StateIndex = "  + (int)(request.State)
					, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "Delivery");


			SqlCommand coma = new SqlCommand("Select MaterialName,MaterialQuantity,ApplierId,ApplierAddress from Applications where ApplyGUID = '" + 
				ds.table[0].Rows[0]["DeliveryGUID"].Tostring() + "'"
					, con);
			SqlDataAdapter da1 = new SqlDataAdapter(coma);
			DataSet ds1 = new DataSet();
			int n = da1.Fill(ds1, "Applications");

			SqlCommand comb = new SqlCommand("Select MaterialName,MaterialQuantity,DonatorId,DonatorAddress from Donations  where DonateGUID = '" + 
				ds.table[0].Rows[0]["DeliveryGUID"].Tostring() + "'"
					, con);
			SqlDataAdapter da2 = new SqlDataAdapter(comb);
			DataSet ds2 = new DataSet();
			 da2.Fill(ds2, "Donation");

			if(n != 0){
				List<Item> Itema = new List<Item>();

				for (int j = 0; j < ds.table[0].Rows.Count; j++)
				{
					Itema.Add(
						new Item()
						{

							GUID = ds.table[0].Rows[j]["DeliveryGUID"].ToString(), 
							Name = ds1.table[0].Rows[0]["MaterialName"].ToStirng(),
							Quantity = ds1.table[0].Rows[0]["MaterialQuantity"],
							StartID =  ds.table[0].Rows[j]["AdminId"],
							FinishID =  ds1.table[0].Rows[0]["ApplierId"],
							Departure =  ds.table[0].Rows[j]["DeliverDeparture"].ToString(),
							Destination = ds.table[0].Rows[j]["DeliverDestination"].ToString(),
							State = (DeliveryState) ds.table[0].Rows[j]["StateIndex"],
							StartTime = Convert.ToDateTime(ds.table[0].Rows[j]["DeliverStartTime"].ToString()),
							FinishTime =  Convert.ToDateTime(ds.table[0].Rows[j]["DeliverFinishTime"].ToString())
							


						}
				   );

				}
				return new DeliveryListResponse(){
					   Items = Itema.clone()
					
					};
				}
			else{
				List<Item> Itema = new List<Item>();

				for (int j = 0; j < ds.table[0].Rows.Count; j++)
				{
					Itema.Add(
						new Item()
						{

							GUID = ds.table[0].Rows[j]["DeliveryGUID"].ToString(), 
							Name = ds2.table[0].Rows[0]["MaterialName"].ToStirng(),
							Quantity = ds2.table[0].Rows[0]["MaterialQuantity"],
							StartID =  ds.table[0].Rows[j]["AdminId"],
							FinishID =  ds2.table[0].Rows[0]["DonatorId"],
							Departure =  ds.table[0].Rows[j]["DeliverDeparture"].ToString(),
							Destination = ds.table[0].Rows[j]["DeliverDestination"].ToString(),
							State = (DeliveryState) ds.table[0].Rows[j]["StateIndex"],
							StartTime = Convert.ToDateTime(ds.table[0].Rows[j]["DeliverStartTime"].ToString()),
							FinishTime =  Convert.ToDateTime(ds.table[0].Rows[j]["DeliverFinishTime"].ToString())

						}
				   );




				}
             return new DeliveryListResponse(){
					   Items = Itema.clone()
					
		     };
			
          }
       }





        public DeliveryMoveResponse HandleDeliveryMoveRequest(DeliveryMoveRequest request)
        {
			string constr = "Server=.;DataBase=Material;" +
				"Integrated Security=True";
			// 建立SqlConnection对象
			SqlConnection con = new SqlConnection(constr);
			// 打开连接
			con.Open();
			// 指定SQL语句
			SqlCommand com = new SqlCommand("Select DeliveryerId,DeliveryGUID,AdminId,DeliveryState,StateIndex from Delivery  where DeliveryerId = " + 
				request.DelivererId + " and DeliveryGUID = '" + request.GUID +"'"
					, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds, "Delivery");


			 SqlCommand coma = new SqlCommand("Select MaterialName,MaterialQuantity,ApplierId,ApplierAddress from Applications where ApplyGUID = '" + 
				ds.table[0].Rows[0]["DeliveryGUID"].Tostring() + "'"
					, con);
			     SqlDataAdapter da1 = new SqlDataAdapter(coma);
			     DataSet ds1 = new DataSet();
			   int  n = da1.Fill(ds1, "Applications");

			SqlCommand comb = new SqlCommand("Select MaterialName,MaterialQuantity,DonatorId,DonatorAddress from Donations  where DonateGUID = '" + 
				ds.table[0].Rows[0]["DeliveryGUID"].Tostring() + "'"
					, con);
			SqlDataAdapter da2 = new SqlDataAdapter(comb);
			DataSet ds2 = new DataSet();
			 da2.Fill(ds2, "Donation");



			if(ds.table[0].Rows[0]["StateIndex"] != 0 && ds.table[0].Rows[0]["StateIndex"] != 1){
				return new DeliveryMoveResponse(){
					    Check = 2
					};
				}
            
			else if(ds.table[0].Rows[0]["StateIndex"] == 0  && n!=0){
				   
				if(request.SecureId != ds.table[0].Rows[0]["AdminId"]){
					   return new DeliveryMoveResponse(){
					    Check = 1
					   };
					}
				else {
					    SqlCommand comc =  new SqlCommand("Update Delivery set DeliveryState = 'Processing',StateIndex = 1,DeliverFinishTime = '"
									      + DataTime.Now.ToString() +"'"
					             , con);
					   
					  return new DeliveryMoveResponse(){
					    Check = 0
					   };
					}


				}
			else if(ds.table[0].Rows[0]["StateIndex"] == 0  && n==0){
				if(request.SecureId != ds2.table[0].Rows[0]["DonatorId"]){
					   return new DeliveryMoveResponse(){
					    Check = 1
					   };
					}
				else {
					    SqlCommand comc =  new SqlCommand("Update Delivery set DeliveryState = 'Processing',StateIndex = 1,DeliverFinishTime = '"
									      + DataTime.Now.ToString() +"'"
					             , con);
					  return new DeliveryMoveResponse(){
					    Check = 0
					   };
					}



				}
			else if(ds.table[0].Rows[0]["StateIndex"] == 1  && n!=0){
				if(request.SecureId != ds1.table[0].Rows[0]["ApplierId"]){
					   return new DeliveryMoveResponse(){
					    Check = 1
					   };
					}
				else {
					    SqlCommand comc =  new SqlCommand("Update Delivery set DeliveryState = 'Finished',StateIndex = 2,DeliverFinishTime = '" 
					           + DataTime.Now.ToString() +"'"   , con);
					  return new DeliveryMoveResponse(){
					    Check = 0
					   };
					}



				}
			else if(ds.table[0].Rows[0]["StateIndex"] == 1  && n==0){
				if(request.SecureId != ds.table[0].Rows[0]["AdminId"]){
					   return new DeliveryMoveResponse(){
					    Check = 1
					   };
					}
				else {
					    SqlCommand comc =  new SqlCommand("Update Delivery set DeliveryState = 'Finished',StateIndex = 2,DeliverFinishTime = '" 
					           + DataTime.Now.ToString() +"'"   , con);
					  return new DeliveryMoveResponse(){
					    Check = 0
					   };
					}



				}
			else {
				return new DeliveryMoveResponse(){
					  Check =3

					};
		    }
            
            
        }

    }
}
