using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    public class DonationDataService
    {
        public int UserId { get; set; }

        public GetDonationListResponse HandleGetDonationListRequest(GetDonationListRequest request)
        {
            //TODO: 数据库
            return new GetDonationListResponse()
            {

            };
        }

        public GetDonationDetailResponse HandleGetDonationDetailRequest(GetDonationDetailRequest request)
        {
            //TODO: 数据库
            return new GetDonationDetailResponse()
            {

            };
        }

        public AvailableDonationMaterialResponse HandleAvailableDonationMaterialRequest(AvailableDonationMaterialRequest request)
        {
            //TODO: 数据库
            return new AvailableDonationMaterialResponse()
            {

            };
        }

        public NewDonationResponse HandleNewDonationRequest(NewDonationRequest request)
        {
            //TODO: 数据库
            return new NewDonationResponse()
            {

            };
        }

        public VoidResponse HandleCancelDonationRequest(CancelDonationRequest request)
        {
            //TODO: 数据库
            return new VoidResponse();
        }
    }

}
