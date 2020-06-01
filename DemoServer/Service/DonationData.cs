using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    public class DonationDataService
    {
        public static GetDonationListResponse HandleGetDonationListRequest(GetDonationListRequest request)
        {
            //TODO: 数据库
            return new GetDonationListResponse()
            {

            };
        }

        public static GetDonationDetailResponse HandleGetDonationDetailRequest(GetDonationDetailRequest request)
        {
            //TODO: 数据库
            return new GetDonationDetailResponse()
            {

            };
        }

        public static AvailableDonationMaterialResponse HandleAvailableDonationMaterialRequest(AvailableDonationMaterialRequest request)
        {
            //TODO: 数据库
            return new AvailableDonationMaterialResponse()
            {

            };
        }

        public static NewDonationResponse HandleNewDonationRequest(NewDonationRequest request)
        {
            //TODO: 数据库
            return new NewDonationResponse()
            {

            };
        }

        public static VoidResponse HandleCancelDonationRequest(CancelDonationRequest request)
        {
            //TODO: 数据库
            return new VoidResponse();
        }
    }

}
