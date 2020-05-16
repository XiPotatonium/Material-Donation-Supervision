using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum DeliveryState
    {
        Waiting,    // 待接单
        Processing, // 配送中
        Finished    // 已完成
    }

    [Serializable]
    public class DeliveryListRequest : IReturn<DeliveryListResponse>
    {
        public int DelivererId { set; get; }
    }

    [Serializable]
    public class DeliveryListResponse
    {
        public List<Item> Items { set; get; }

        public class Item
        {
            public string GUID { set; get; }
            public string Name { set; get; }
            public int Quantity { set; get; }
            public int StartID { set; get; }
            public int FinishID { set; get; }
            public string Departure { set; get; }
            public string Destination { set; get; }
            public DeliveryState State { set; get; }
            public DateTime StartTime { set; get; }
            public DateTime FinishTime { set; get; }
        }
    }
}
