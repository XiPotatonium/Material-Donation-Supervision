using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDS.Server
{
    class DeliveryDataService
    {
        public static DeliveryListNumResponse HandleDeliveryListNumRequest(DeliveryListNumRequest request)
        {
            //TODO:数据库
            return new DeliveryListNumResponse();
        }

        public static DeliveryListResponse HandleDeliveryListRequest(DeliveryListRequest request)
        {
            //TODO:数据库
            return new DeliveryListResponse();
        }

        public static DeliveryMoveResponse HandleDeliveryMoveRequest(DeliveryMoveRequest request)
        {
            //TODO:数据库
            return new DeliveryMoveResponse();
        }
              
    }
}
