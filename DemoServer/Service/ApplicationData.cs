using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server.Service
{
    class ApplicationDataService
    {
        public static GetApplicationListResponse HandleGetApplicationListRequest(GetApplicationListRequest request)
        {
            //TODO: 数据库操作
            return new GetApplicationListResponse()
            {

            };
        }

        public static GetApplicationDetailResponse HandleGetApplicationDetailRequest(GetApplicationDetailRequest request)
        {
            //TODO：数据库操作
            return new GetApplicationDetailResponse()
            {

            };
        }

        public static AvailableApplicationMaterialResponse HandleAvailableApplicationMaterialRequest(AvailableApplicationMaterialRequest request)
        {
            //TODO：数据库操作
            return new AvailableApplicationMaterialResponse()
            {

            };
        }

        public static NewApplicationResponse HandleNewApplicationRequest(NewApplicationRequest request)
        {
            //TODO：数据库操作
            return new NewApplicationResponse()
            {

            };
        }

        public static VoidResponse HandleCancelApplicationRequest(CancelApplicationRequest request)
        {
            //TODO:数据库操作
            return new VoidResponse();
        }

        public static VoidResponse HandleConfirmApplicationDoneRequest(ConfirmApplicationDoneRequest request)
        {
            //TODO:数据库操作
            return new VoidResponse();
        }
    }
}
