using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum DeliveryState
    {
        Waiting,    // 待接单（配送员尚未取得物资）
        Processing, // 配送中（物资在配送员手中）
        Finished    // 已完成
    }
    [Serializable]
    public class Item
    {
        public int GUID { set; get; }
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

    [Serializable]
    public class DeliveryListRequest : IReturn<DeliveryListResponse>
    {
        public int DelivererId { set; get; }        // 配送员id
        public DeliveryState State { set; get; }    // 需要获取的状态
    }

    [Serializable]
    public class DeliveryListResponse
    {
        public List<Item> Items { set; get; }        
    }

    [Serializable]
    public class DeliveryListNumRequest : IReturn<DeliveryListNumResponse>
    {
        public int DelivererId { set; get; }        // 配送员id
        public DeliveryState State { set; get; }    // 需要获取的状态
    }

    [Serializable]
    public class DeliveryListNumResponse
    {
        public int Num { set; get; }
    }

    [Serializable]
    public class DeliveryMoveRequest : IReturn<DeliveryMoveResponse>
    {
        public int DelivererId { set; get; }        // 配送员id
        public int GUID { set; get; }            // 订单号
        public int SecureId { set; get; }        // 验证id
    }

    [Serializable]
    public class DeliveryMoveResponse
    {
        public int Check { set; get; }             // 操作结果号
    }
}
