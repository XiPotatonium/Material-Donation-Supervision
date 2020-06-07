using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Transactions;
using MDS.Server.Service;
using DemoServer;

namespace MDS.Server
{
    public class AdminDataService
    {
        public int UserId { get; set; }

        public MaterialAuditAgreeResponse HandleMaterialAuditAgreeRequest(MaterialAuditAgreeRequest request)
        {
            return new MaterialAuditAgreeResponse();
        }

        public MaterialAuditListResponse HandleMaterialAuditListRequest(MaterialAuditListRequest request)
        {
            if (request.state == AdminState.WAIT)
            {
                SqlCommand com = new SqlCommand
                ($"SELECT * FROM Tranc WHERE (TransactionState = {(int)ApplicationState.Applying} and TransactionType = {(int)TransactionType.APPLICATION})" +
                $"or (TransactionState = {(int)DonationState.Applying} and TransactionType = {(int)TransactionType.DONATION})"
                , Connect.Connection);
                // 建立SqlDataAdapter和DataSet对象
                SqlDataAdapter da = new SqlDataAdapter(com);
                using (DataSet ds = new DataSet())
                {
                    var list = new List<Normal>();
                    da.Fill(ds, "Tranc");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(new Normal()
                        {
                            Content = Material.MaterialMap[int.Parse(ds.Tables[0].Rows[i]["MaterialId"].ToString())],
                            Number = ds.Tables[0].Rows[i]["MaterialQuantity"].ToString(),
                            State = AdminState.WAIT,
                            Result = AdminResult.NONE,
                            ReviewerID = -1,
                            Time = DateTime.Parse(ds.Tables[0].Rows[i]["StartTime"].ToString()),
                            UserID = int.Parse(ds.Tables[0].Rows[i]["UserId"].ToString()),
                            Type = int.Parse(ds.Tables[0].Rows[i]["TransactionType"].ToString()) == (int)TransactionType.APPLICATION
                                 ? ReviewType.APPLY : ReviewType.DONATE

                        });
                    }
                    return new MaterialAuditListResponse() { m_normals = list };
                }
            }
            else
            {
                SqlCommand com = new SqlCommand
                ($"SELECT * FROM Tranc WHERE (TransactionState = {(int)ApplicationState.Applying} and TransactionType = {(int)TransactionType.APPLICATION})" +
                $"or (TransactionState = {(int)DonationState.Applying} and TransactionType = {(int)TransactionType.DONATION})"
                , Connect.Connection);
                return new MaterialAuditListResponse();
            }            
        }


    }

}
