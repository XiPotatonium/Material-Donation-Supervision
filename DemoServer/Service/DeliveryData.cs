using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    public class DeliveryDataService
    {
        public int UserId { get; set; }

        public DeliveryListNumResponse HandleDeliveryListNumRequest(DeliveryListNumRequest request)
        {
            //TODO:数据库
            return new DeliveryListNumResponse();
        }

        public DeliveryListResponse HandleDeliveryListRequest(DeliveryListRequest request)
        {
            //TODO:数据库
            return new DeliveryListResponse();
        }

        public DeliveryMoveResponse HandleDeliveryMoveRequest(DeliveryMoveRequest request)
        {
            //TODO:数据库
            return new DeliveryMoveResponse();
        }

    }
}
