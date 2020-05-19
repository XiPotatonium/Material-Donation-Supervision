using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    public class DomationDataService
    {
        public static GetApplicationDetailResponse HandleGetApplicationDetailRequest(GetApplicationDetailRequest request)
        {
            //TODO: 数据库
            return new GetApplicationDetailResponse();
        }

        public static GetApplicationListResponse HandleGetApplicationListRequest(GetApplicationListRequest request)
        {
            //TODO: 数据库
            return new GetApplicationListResponse();
        }
    }
   
}
