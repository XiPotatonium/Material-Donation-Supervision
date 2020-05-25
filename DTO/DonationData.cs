using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    /// <summary>
    /// 用户获取自己的所有捐赠
    /// </summary>
    [Serializable]
    public class GetDonationListRequest : IReturn<GetApplicationListResponse>
    {
        public int UserId { set; get; }
    }

    [Serializable]
    public class GetDonationListResponse
    {
        public class Item
        {

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
        // TODO
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
