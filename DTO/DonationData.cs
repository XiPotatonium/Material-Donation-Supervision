using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum DonationState
    {
        Aborted,
        Applying, 
        WaitingDelivery, 
        Done
    }

    /// <summary>
    /// 用户获取自己的所有捐赠
    /// </summary>
    [Serializable]
    public class GetDonationListRequest : IReturn<GetDonationListResponse>
    {
        public int UserId { set; get; }
    }

    [Serializable]
    public class GetDonationListResponse
    {
        public class Item
        {
            public int ID { set; get; }     // 字段参考Application
            public string GUID { set; get; }
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
    public class GetDonationDetailRequest : IReturn<GetApplicationDetailResponse>
    {
        public int UserId { set; get; }
        public int DonationId { set; get; }
    }

    [Serializable]
    public class GetDonationDetailResponse
    {
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
        public class Item
        {

        }

        public List<Item> Items { set; get; }
    }

    /// <summary>
    /// 用户请求新捐赠
    /// </summary>
    [Serializable]
    public class NewDonationRequest : IReturn<NewApplicationResponse>
    {

    }

    [Serializable]
    public class NewDonationResponse
    {

    }

    /// <summary>
    /// 用户请求撤销捐赠
    /// </summary>
    [Serializable]
    public class CancelDonationRequest : IReturn<VoidResponse>
    {

    }
}
