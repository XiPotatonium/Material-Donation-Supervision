﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum DonationState
    {
        Refused,
        Aborted,
        Applying, 
        WaitingDelivery,
        Delivering,
        Done
    }

    /// <summary>
    /// 用户获取自己的所有捐赠
    /// </summary>
    [Serializable]
    public class GetDonationListRequest : IReturn<GetDonationListResponse>
    {
    }

    [Serializable]
    public class GetDonationListResponse
    {
        [Serializable]
        public class Item
        {
            public int ID { set; get; }     // 字段参考Application
            public string Name { set; get; }
            public int Quantity { set; get; }
            public DonationState State { set; get; }
            public DateTime StartTime { set; get; }
        }

        public List<Item> Items { set; get; }
    }


    /// <summary>
    /// 用户获取某个具体捐赠的详细信息
    /// </summary>
    [Serializable]
    public class GetDonationDetailRequest : IReturn<GetDonationDetailResponse>
    {
        public int UserId { set; get; }
        public int DonationId { set; get; }
    }

    [Serializable]
    public class GetDonationDetailResponse
    {
        public string Address { set; get; }
        // TODO
        // 最好用户能在这里看到捐赠的详细使用情况
    }




    /// <summary>
    /// 请求获得所有可以捐赠的物资
    /// </summary>
    [Serializable]
    public class AvailableDonationMaterialRequest : IReturn<AvailableDonationMaterialResponse>
    {
        // 应该不需要用户id了
    }

    [Serializable]
    public class AvailableDonationMaterialResponse
    {
        [Serializable]
        public class Item
        {
            public int Id { set; get; }             // 物资名称对应一个Id
            public string Name { set; get; }
            public string Description { set; get; }
        }

        public List<Item> Items { set; get; }
    }

    /// <summary>
    /// 用户请求新捐赠
    /// </summary>
    [Serializable]
    public class NewDonationRequest : IReturn<NewDonationResponse>
    {
        public int MaterialId { set; get; }
        public int Quantity { set; get; }
        public string Address { set; get; } // 发请求时刻用户的地址，后续用户地址可能会和这个不同
    }

    [Serializable]
    public class NewDonationResponse
    {
        public GetDonationListResponse.Item Item { set; get; }
    }

    /// <summary>
    /// 用户请求撤销捐赠
    /// </summary>
    [Serializable]
    public class CancelDonationRequest : IReturn<VoidResponse>
    {
        public int DonationId { set; get; }
    }
}
