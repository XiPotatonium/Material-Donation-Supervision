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
                $"select DonateID, DonateGUID, MaterialName, MaterialQuantity, DonationState, StateIndex, StartTime " +
                $"from Donation " +
                $"where DonatorID= {UserId}"
                , Connect.Connection);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Donation");
            List<GetDonationListResponse.Item> Itema = new List<GetDonationListResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                Itema.Add(new GetDonationListResponse.Item()
                {
                    ID = (int)ds.Tables[0].Rows[j]["DonateID"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Quantity = (int)ds.Tables[0].Rows[j]["MaterialQuantity"],
                    State = (DonationState)ds.Tables[0].Rows[j]["StateIndex"],
                    StartTime = Convert.ToDateTime(ds.Tables[0].Rows[j]["StartTime"].ToString())
                });

            }
            return new GetDonationListResponse()
            {
                Items = Itema
            };
        }

        public GetDonationDetailResponse HandleGetDonationDetailRequest(GetDonationDetailRequest request)
        {
            SqlCommand com = new SqlCommand(
                $"Select DonateID, DonateGUID, DonatorAddress " +
                $"from Donation " +
                $"where DonateID={request.DonationId}", Connect.Connection);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Donation");
            return new GetDonationDetailResponse()
            {
                Address = ds.Tables[0].Rows[0]["DonatorAddress"].ToString()
            };
        }

        public AvailableDonationMaterialResponse HandleAvailableDonationMaterialRequest(AvailableDonationMaterialRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand(
                $"select MaterialID, MaterialName, MaterialDescription, MaterialConstraint from Materials"
                , Connect.Connection);

            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Materials");
            List<AvailableDonationMaterialResponse.Item> Itema = new List<AvailableDonationMaterialResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                Itema.Add(new AvailableDonationMaterialResponse.Item()
                {
                    Id = (int)ds.Tables[0].Rows[j]["MaterialID"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Description = ds.Tables[0].Rows[j]["MaterialDescription"].ToString()
                });
            }

            return new AvailableDonationMaterialResponse()
            {
                Items = Itema
            };
        }

        public NewDonationResponse HandleNewDonationRequest(NewDonationRequest request)
        {
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
                com = new SqlCommand(
                    $"update Materials " +
                    $"set MaterilaQuantity = MaterialQuantity + {request.Quantity}" +
                    $"where MaterialId = {request.MaterialId}", 
                    Connect.Connection);

                DateTime now = DateTime.Now;

                com.CommandText = $"insert into Tranc(UserId, Address, MaterialId, MaterialQuantity, TransactionState, TransactionType, StartTime, AdminId) " +
                    $"values({UserId}, {request.Address}, {request.MaterialId}, {request.Quantity}, {(int)DonationState.Applying}, {(int)TransactionType.DONATION}, {now}, -1";

                return new NewDonationResponse()
                {
                    Item = new GetDonationListResponse.Item()
                    {
                        Name = ds.Tables[0].Rows[0]["MaterialName"].ToString(),
                        Quantity = request.Quantity,
                        State = DonationState.Applying,
                        StartTime = now
                    }
                };
            }
            else
            {
                return new NewDonationResponse() { };
            }
        }

        public static VoidResponse HandleCancelDonationRequest(CancelDonationRequest request)
        {
            SqlCommand com = new SqlCommand(
                $"update Donations " +
                $"set DonationState = Aborted, StateIndex = 0 " +
                $"where DonateGUID = {request.DonationId}"
                , Connect.Connection);
            return new VoidResponse();
        }
    }

}
