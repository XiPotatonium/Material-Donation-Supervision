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
        public static int ApplicationID = 10000;


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
            SqlCommand com = new SqlCommand
                ("select ApplyID,ApplyGUID,ApplierAddress from Transaction where ApplyID="
                    + request.ApplicationId + "", Connect.Connection);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();

            da.Fill(ds, "Transaction");
            return new GetApplicationDetailResponse()
            {
                Address = ds.Tables[0].Rows[0]["ApplierAddress"].ToString()
            };
        }

        public AvailableApplicationMaterialResponse HandleAvailableApplicationMaterialRequest(AvailableApplicationMaterialRequest request)
        {
            // 指定SQL语句
            SqlCommand com = new SqlCommand("Select MaterialID,MaterialName,MaterialDescription,MaterialConstraint from Materials "
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
            SqlCommand com = new SqlCommand
                ("select MaterialId,MaterialName,MaterilaQuantity from Materials where MaterialId= "
                    + request.MaterialId + "", Connect.Connection);
            // 建立SqlDataAdapter和DataSet对象
            SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet, "Materials");
            if (request.Quantity <= (int)dataSet.Tables[0].Rows[0]["MaterialQuantity"])
            {
                try
                {
                    SqlTransaction transaction = Connect.Connection.BeginTransaction();

                    com = new SqlCommand("Update Materials set MaterilaQuantity = MaterialQuantity -" +
                        request.Quantity + " where MaterialId = " + request.MaterialId + "", Connect.Connection, transaction);

                    com.ExecuteNonQuery();

                    com.CommandText = "insert into Transaction(ApplyID,ApplyGUID, ApplierId, ApplierAddress,MaterialName,MaterialQuantity,ApplicationState,StateIndex,StartTime) values("
                        + ApplicationID + "'," + UserId + ",'" + request.Address + "','" + dataSet.Tables[0].Rows[0]["MaterialName"]
                        + "'," + request.Quantity + ",'Applying" + "', 1 ,'" + DateTime.Now.ToString() + "')";

                    com.ExecuteNonQuery();

                    return new NewApplicationResponse()
                    {
                        Item = new GetApplicationListResponse.Item()
                        {
                            ID = ApplicationID,
                            Name = dataSet.Tables[0].Rows[0]["MaterialName"].ToString(),
                            Quantity = request.Quantity,
                            State = ApplicationState.Applying,
                            StartTime = DateTime.Now                    
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
            SqlCommand com = new SqlCommand($"Update Transaction set ApplicationState = Aborted ,StateIndex = 0 where ApplyID = {request.ApplicationId}"
                , Connect.Connection);
            return new VoidResponse();
        }

        public static VoidResponse HandleConfirmApplicationDoneRequest(ConfirmApplicationDoneRequest request)
        {
            SqlCommand com = new SqlCommand($"Update Transaction set ApplicationState = Done, StateIndex = 4  where ApplyID = {request.ApplicationId}"
                , Connect.Connection);
            return new VoidResponse();

        }
    }
}
