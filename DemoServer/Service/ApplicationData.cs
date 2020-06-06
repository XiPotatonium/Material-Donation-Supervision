using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Threading;

namespace MDS.Server.Service
{
    enum TransactionType
    {
        APPLICATION,
        DONATION
    }

    public class ApplicationDataService
    {
        public int UserId { get; set; }

        public GetApplicationListResponse HandleGetApplicationListRequest(GetApplicationListRequest request)
        {
            // 指定SQL语句
            int a = (int)TransactionType.APPLICATION;
            SqlCommand com = new SqlCommand(
                $"select Tranc.TransactionId, Materials.MaterialName, Tranc.MaterialQuantity, Tranc.TransactionState, Tranc.StartTime " +
                $"from Tranc left join Materials " +
                $"on Tranc.MaterialId=Materials.MaterialID " +
                $"where Tranc.UserId={UserId} and Tranc.TransactionType={a}"
                , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);

            using DataSet ds = new DataSet();
            da.Fill(ds, "Tranc");

            List<GetApplicationListResponse.Item> Itema = new List<GetApplicationListResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                Itema.Add(new GetApplicationListResponse.Item()
                {
                    ID = (int)ds.Tables[0].Rows[j]["TransactionId"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Quantity = (int)ds.Tables[0].Rows[j]["MaterialQuantity"],
                    State = (ApplicationState)ds.Tables[0].Rows[j]["TransactionState"],
                    StartTime = (DateTime)ds.Tables[0].Rows[j]["StartTime"]
                });
            }

            return new GetApplicationListResponse()
            {
                Items = Itema
            };
        }

        public GetApplicationDetailResponse HandleGetApplicationDetailRequest(GetApplicationDetailRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand(
                $"select Address " +
                $"from Tranc " +
                $"where TransactionId={request.ApplicationId}"
                , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Tranc");
            return new GetApplicationDetailResponse()
            {
                Address = ds.Tables[0].Rows[0]["Address"].ToString()
            };
        }

        public AvailableApplicationMaterialResponse HandleAvailableApplicationMaterialRequest(AvailableApplicationMaterialRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand("Select MaterialID, MaterialName, MaterialDescription, MaterialConstraint from Materials "
                    , Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Materials");
            List<AvailableApplicationMaterialResponse.Item> Itema = new List<AvailableApplicationMaterialResponse.Item>();

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                Itema.Add(new AvailableApplicationMaterialResponse.Item()
                {
                    Id = (int)ds.Tables[0].Rows[j]["MaterialID"],
                    Name = ds.Tables[0].Rows[j]["MaterialName"].ToString(),
                    Description = ds.Tables[0].Rows[j]["MaterialDescription"].ToString(),
                    Constraint = (int)ds.Tables[0].Rows[j]["MaterialConstraint"]
                });

            }
            return new AvailableApplicationMaterialResponse()
            {
                Items = Itema
            };
        }

        public NewApplicationResponse HandleNewApplicationRequest(NewApplicationRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand(
                $"select MaterialId, MaterialName, MaterilaQuantity " +
                $"from Materials " +
                $"where MaterialId={request.MaterialId}"
                , Connect.Connection);
            // 建立SqlDataAdapter和DataSet对象
            SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
            DataSet dataSet = new DataSet();

            int n = dataAdapter.Fill(dataSet, "Materials");
            if (n != 0 && request.Quantity <= (int)dataSet.Tables[0].Rows[0]["MaterialQuantity"])
            {
                string materialName = dataSet.Tables[0].Rows[0]["MaterialName"].ToString();
                try
                {
                    SqlTransaction transaction = Connect.Connection.BeginTransaction();

                    com = new SqlCommand(
                        $"update Materials " +
                        $"set MaterilaQuantity = MaterialQuantity - {request.Quantity} " + 
                        $"where MaterialId={request.MaterialId}"
                        , Connect.Connection, transaction);

                    com.ExecuteNonQuery();

                    DateTime now = DateTime.Now;

                    com.CommandText = $"insert into Tranc(UserId, Address, MaterialId, MaterialQuantity, TransactionState, TransactionType, StartTime, AdminId) " +
                        $"output inserted.id values({UserId}, {request.Address}, {request.MaterialId}, {request.Quantity}, {(int)ApplicationState.Applying}, {(int)TransactionType.APPLICATION}, {now}, -1";

                    dataAdapter = new SqlDataAdapter(com);
                    dataSet = new DataSet();
                    n = dataAdapter.Fill(dataSet, "Tranc");

                    return new NewApplicationResponse()
                    {
                        Item = new GetApplicationListResponse.Item()
                        {
                            ID = request.MaterialId,
                            Name = materialName,
                            Quantity = request.Quantity,
                            State = ApplicationState.Applying,
                            StartTime = now
                        }
                    };
                }
                catch (Exception ex)
                {
                    if (com.Transaction != null)
                    {
                        com.Transaction.Rollback();
                    }
                    // TODO 错误报告
                    throw ex;
                }
            }
            else
            {
                // TODO 错误报告
                return new NewApplicationResponse() { Item = null };
            }
        }

        public VoidResponse HandleCancelApplicationRequest(CancelApplicationRequest request)
        {
            SqlTransaction transaction = Connect.Connection.BeginTransaction();
            try
            {
                SqlCommand com = new SqlCommand(
                    $"update Tranc " +
                    $"set TransactionState = {(int)ApplicationState.Aborted}" +
                    $"where TransactionId = {request.ApplicationId}"
                    , Connect.Connection, transaction);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                // TODO 错误报告
                throw ex;
            }
            return new VoidResponse();
        }

        public static VoidResponse HandleConfirmApplicationDoneRequest(ConfirmApplicationDoneRequest request)
        {
            SqlTransaction transaction = Connect.Connection.BeginTransaction();
            try
            {
                SqlCommand com = new SqlCommand(
                    $"update Tranc " +
                    $"set TransactionState = {(int)ApplicationState.Done}" +
                    $"where TransactionId = {request.ApplicationId}"
                    , Connect.Connection, transaction);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                // TODO 错误报告
                throw ex;
            }
            return new VoidResponse();
        }
    }
}
