using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace MDS.Server.Service
{

    public class DonationDataService
    {
        public int UserId { get; set; }

        public GetDonationListResponse HandleGetDonationListRequest(GetDonationListRequest request)
        {
            SqlCommand com = new SqlCommand(
                $"select Tranc.TransactionId, Materials.MaterialName, Tranc.MaterialQuantity, Tranc.TransactionState, Tranc.StartTime " +
                $"from Tranc left join Materials " +
                $"on Tranc.MaterialId=Materials.MaterialID " +
                $"where Tranc.UserId={UserId} and Tranc.TransactionType={(int)TransactionType.DONATION}"
                , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Tranc");
            List<GetDonationListResponse.Item> items = new List<GetDonationListResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                items.Add(new GetDonationListResponse.Item()
                {
                    ID = (int)ds.Tables[0].Rows[j]["TransactionId"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Quantity = (int)ds.Tables[0].Rows[j]["MaterialQuantity"],
                    State = (DonationState)ds.Tables[0].Rows[j]["TransactionState"],
                    StartTime = (DateTime)ds.Tables[0].Rows[j]["StartTime"]
                });

            }
            return new GetDonationListResponse()
            {
                Items = items
            };
        }

        public GetDonationDetailResponse HandleGetDonationDetailRequest(GetDonationDetailRequest request)
        {
            SqlCommand com = new SqlCommand(
                $"select Address " +
                $"from Tranc " +
                $"where TransactionId={request.DonationId}"
                , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Tranc");
            return new GetDonationDetailResponse()
            {
                Address = ds.Tables[0].Rows[0]["Address"].ToString()
            };
        }

        public AvailableDonationMaterialResponse HandleAvailableDonationMaterialRequest(AvailableDonationMaterialRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand("Select MaterialID, MaterialName, MaterialDescription, MaterialConstraint from Materials "
                    , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            using DataSet ds = new DataSet();

            da.Fill(ds, "Materials");
            List<AvailableDonationMaterialResponse.Item> items = new List<AvailableDonationMaterialResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                items.Add(new AvailableDonationMaterialResponse.Item()
                {
                    Id = (int)ds.Tables[0].Rows[j]["MaterialID"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Description = ds.Tables[0].Rows[j]["MaterialDescription"].ToString()
                });

            }
            return new AvailableDonationMaterialResponse()
            {
                Items = items
            };
        }

        public NewDonationResponse HandleNewDonationRequest(NewDonationRequest request)
        {
            NewDonationResponse ret;
            SqlCommand com = new SqlCommand(
                $"select MaterialID, MaterialName, MaterialQuantity " +
                $"from Materials " +
                $"where " +
                $"MaterialId = {request.MaterialId}", 
                Connect.Connection);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            int n = da.Fill(ds, "Materials");
            if (n != 0)
            {
                DateTime now = DateTime.Now;

                com.CommandText = $"INSERT INTO Tranc (UserId, Address, MaterialId, MaterialQuantity, TransactionState, TransactionType, StartTime, AdminId) " +
                            $"OUTPUT INSERTED.TransactionId values ({UserId}, '{request.Address}', {request.MaterialId}, {request.Quantity}, " +
                            $"{(int)ApplicationState.Applying}, {(int)TransactionType.DONATION}, '{now}', -1)";

                // https://stackoverflow.com/questions/18373461/execute-insert-command-and-return-inserted-id-in-sql
                int modified = Convert.ToInt32(com.ExecuteScalar());

                ret = new NewDonationResponse()
                {
                    Item = new GetDonationListResponse.Item()
                    {
                        ID = modified,
                        Name = ds.Tables[0].Rows[0]["MaterialName"].ToString(),
                        Quantity = request.Quantity,
                        State = DonationState.Applying,
                        StartTime = now
                    }
                };
            }
            else
            {
                Console.WriteLine($"DEBUG: material {request.MaterialId} doesn't exist");
                ret = new NewDonationResponse() { };
            }
            return ret;
        }

        public static VoidResponse HandleCancelDonationRequest(CancelDonationRequest request)
        {
            SqlCommand com = new SqlCommand(
                $"update Tranc " +
                $"set TransactionState = {(int)DonationState.Aborted}" +
                $"where TransactionId = {request.DonationId}"
                , Connect.Connection);
            if (com.ExecuteNonQuery() == 0)
            {
                Console.WriteLine($"DEBUG: unable to abort donation {request.DonationId}");
            }
            return new VoidResponse();
        }
    }

}
