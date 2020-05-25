using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    /// <summary>
    /// 用户获取自己的所有请求
    /// </summary>
    [Serializable]
    public class GetApplicationListRequest : IReturn<GetApplicationListResponse>
    {
        public int UserId { set; get; }
    }

    [Serializable]
    public class GetApplicationListResponse
    {
        public class Item
        {

        }

        public List<Item> Items { set; get; }
    }


    /// <summary>
    /// 用户获取某个具体请求的详细信息
    /// </summary>
    [Serializable]
    public class GetApplicationDetailRequest : IReturn<GetApplicationDetailResponse>
    {
        public int UserId { set; get; }
        // TODO
    }

    [Serializable]
    public class GetApplicationDetailResponse
    {
        // TODO
    }




    /// <summary>
    /// 请求获得所有可以申请的物资
    /// </summary>
    [Serializable]
    public class AvailableApplicationMaterialRequest : IReturn<AvailableApplicationMaterialResponse>
    {
        // 应该不需要用户id了
    }

    [Serializable]
    public class AvailableApplicationMaterialResponse
    {
        public class Item
        {

        }

        public List<Item> Items { set; get; }
    }

    /// <summary>
    /// 用户请求新申请
    /// </summary>
    [Serializable]
    public class NewApplicationRequest : IReturn<NewApplicationResponse>
    {

    }

    [Serializable]
    public class NewApplicationResponse
    {

    }

    /// <summary>
    /// 用户请求撤销申请
    /// </summary>
    [Serializable]
    public class CancelApplicationRequest : IReturn<VoidResponse>
    {

    }

    /// <summary>
    /// 确认收到
    /// </summary>
    [Serializable]
    public class ConfirmApplicationDoneRequest : IReturn<VoidResponse>
    {

    }
}
