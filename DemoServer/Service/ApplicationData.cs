using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server.Service
{  
    class ApplicationDataService
    {
        public int UserId { get; set; }

        public GetApplicationListResponse HandleGetApplicationListRequest(GetApplicationListRequest request)
        {
            //TODO: 数据库操作
            return new GetApplicationListResponse()
            {

            };
        }

        public GetApplicationDetailResponse HandleGetApplicationDetailRequest(GetApplicationDetailRequest request)
        {
            //TODO：数据库操作
            return new GetApplicationDetailResponse()
            {

            };
        }

        public AvailableApplicationMaterialResponse HandleAvailableApplicationMaterialRequest(AvailableApplicationMaterialRequest request)
        {
            //TODO：数据库操作
            return new AvailableApplicationMaterialResponse()
            {

            };
        }

        public NewApplicationResponse HandleNewApplicationRequest(NewApplicationRequest request)
        {
            //TODO：数据库操作
            return new NewApplicationResponse()
            {

            };
        }

        public VoidResponse HandleCancelApplicationRequest(CancelApplicationRequest request)
        {
            //TODO:数据库操作
            return new VoidResponse();
        }

        public VoidResponse HandleConfirmApplicationDoneRequest(ConfirmApplicationDoneRequest request)
        {
            //TODO:数据库操作
            return new VoidResponse();
        }
    }
}
