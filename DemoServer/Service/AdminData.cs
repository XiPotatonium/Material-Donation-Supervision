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
                            Content = Material.MaterialMap[int.Parse(ds.Tables[0].Rows[i]["MaterialId"].ToString())] +
                            ": " + ds.Tables[0].Rows[i]["MaterialQuantity"].ToString() + "件",
                            Number = ds.Tables[0].Rows[i]["TransactionId"].ToString(),
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
                ($"SELECT * FROM Tranc WHERE AdminId = {request.AdminID}", Connect.Connection);
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
                            State = AdminState.FINISH,
                            Result = int.Parse(ds.Tables[0].Rows[i]["TransactionState"].ToString()) == 0 ? AdminResult.FAIL : AdminResult.PASS,
                            ReviewerID = request.AdminID,
                            Time = DateTime.Parse(ds.Tables[0].Rows[i]["StartTime"].ToString()),
                            UserID = int.Parse(ds.Tables[0].Rows[i]["UserId"].ToString()),
                            Type = int.Parse(ds.Tables[0].Rows[i]["TransactionType"].ToString()) == (int)TransactionType.APPLICATION
                                 ? ReviewType.APPLY : ReviewType.DONATE

                        });
                    }
                    return new MaterialAuditListResponse() { m_normals = list };
                }
            }            
        }

        public MaterialAuditAgreeResponse HandleMaterialAuditAgreeRequest(MaterialAuditAgreeRequest request)
        {
            SqlCommand com = new SqlCommand
                ($"SELECT * FROM Users WHERE UserID = {request.AdminID} AND SecondaryPasswd = {request.Secondary_passward}", Connect.Connection);
            SqlDataReader reader = com.ExecuteReader();
            if(reader.Read())
            {
                reader.Close();
                var ncom = new SqlCommand($"UPDATE Tranc SET TransactionState = {(int)ApplicationState.WaitingDelivery} ,AdminId = {request.AdminID} WHERE TransactionId = {request.Number}", Connect.Connection);
                if (ncom.ExecuteNonQuery() > 0)
                {
                    var dcom = new SqlCommand($"INSERT INTO Delivery(TransactionId, DeliveryState, DeliveryAdminId) VALUES({request.Number},{(int)DeliveryState.Alone}, {request.AdminID}) ", Connect.Connection);
                    dcom.ExecuteNonQuery();
                    return new MaterialAuditAgreeResponse() { flag = 0 };
                }
                else
                {
                    return new MaterialAuditAgreeResponse() { flag = -1 };
                }
            }
            else
            {
                reader.Close();
                return new MaterialAuditAgreeResponse() { flag = -1 };
            }
        }

        public MaterialAuditRefuseResponse HandleMaterialAuditRefuseRequest(MaterialAuditRefuseRequest request)
        {
            SqlCommand com = new SqlCommand
                ($"SELECT * FROM Users WHERE UserID = {request.AdminID} AND SecondaryPasswd = {request.Secondary_passward}", Connect.Connection);
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                var ncom = new SqlCommand($"UPDATE Tranc SET TransactionState = {(int)ApplicationState.Refused} ,AdminId = {request.AdminID} WHERE TransactionId = {request.Number}", Connect.Connection);
                if (ncom.ExecuteNonQuery() > 0)
                {
                    return new MaterialAuditRefuseResponse() { flag = 0 };
                }
                else
                {
                    return new MaterialAuditRefuseResponse() { flag = -1 };
                }
            }
            else
            {
                reader.Close();
                return new MaterialAuditRefuseResponse() { flag = -1 };
            }
        }

        public SecondaryPasswordChangeResponse HandleSecondaryPasswordChangeRequest(SecondaryPasswordChangeRequest request)
        {
            if (string.IsNullOrEmpty(request.New_password))
            {
                return new SecondaryPasswordChangeResponse() { flag = 2 };
            }
            else if (request.New_password == request.Old_password)
            {
                return new SecondaryPasswordChangeResponse() { flag = 1 };
            }
            else
            {
                var com = new SqlCommand($"SELECT * FROM Users WHERE UserID = {request.AdminID} AND SecondaryPasswd = {request.Old_password}", Connect.Connection);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    var ncom = new SqlCommand($"UPDATE Users SET SecondaryPasswd = {request.New_password} WHERE UserID = {request.AdminID}", Connect.Connection);
                    ncom.ExecuteNonQuery();
                    return new SecondaryPasswordChangeResponse() { flag = 0 };                       
                }
                else
                {
                    reader.Close();
                    return new SecondaryPasswordChangeResponse() { flag = 3 };
                }
            }
        }


    }

}
