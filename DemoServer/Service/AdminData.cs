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
		
		public MaterialAuditAgreeResponse HandleMaterialAuditAgreeRequest(MaterialAuditAgreeRequest request)
		{
			return new MaterialAuditAgreeResponse();
		}

		public MaterialAuditListResponse HandleMaterialAuditListRequest(MaterialAuditListRequest request)
		{
			if (request.state == AdminState.WAIT)
			{

			}
			else
			{

			}

			return new MaterialAuditListResponse();
		}


	}
	
}
