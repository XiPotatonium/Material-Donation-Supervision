using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum ApplicationState
    {
        Refused,
        Aborted,        // 撤销的申请
        Applying, 
        Delivering, 
        Received, 
        Done
    }


    /// <summary>
    /// 用户获取自己的所有请求
    /// </summary>
    [Serializable]
    public class GetApplicationListRequest : IReturn<GetApplicationListResponse>
    {
    }

    [Serializable]
    public class GetApplicationListResponse
    {
        [Serializable]
        public class Item
        {
            public int ID { set; get; }             // 申请的ID，可以直接对应数据库表中的id，后续关于这个item的查询都会发送这个id
            public string Name { set; get; }        // 申请的东西的名字
            public int Quantity { set; get; }       // 数量，整数
            public ApplicationState State { set; get; } // 申请状态
            public DateTime StartTime { set; get; } // 开始时间
        }

        public List<Item> Items { set; get; }
    }


    /// <summary>
    /// 用户获取某个具体请求的详细信息
    /// </summary>
    [Serializable]
    public class GetApplicationDetailRequest : IReturn<GetApplicationDetailResponse>
    {
        public int ApplicationId { set; get; }
    }

    [Serializable]
    public class GetApplicationDetailResponse
    {
        public string Address { set; get; }     // 发请求时刻的地址
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
        [Serializable]
        public class Item
        {
            public int Id { set; get; }             // 物资名称对应一个Id，申请的时候会使用这个Id
            public string Name { set; get; }        // 物资名称
            public string Description { set; get; } // 物资描述
            public int Constraint { set; get; }     // 物资限制
        }

        public List<Item> Items { set; get; }
    }

    /// <summary>
    /// 用户请求新申请
    /// </summary>
    [Serializable]
    public class NewApplicationRequest : IReturn<NewApplicationResponse>
    {
        public int MaterialId { set; get; }
        public int Quantity { set; get; }
        public string Address { set; get; } // 发请求时刻用户的地址，后续用户地址可能会和这个不同
    }

    [Serializable]
    public class NewApplicationResponse
    {
        public GetApplicationListResponse.Item Item { set; get; }   // 顺带返回新建立的申请的Item
    }

    /// <summary>
    /// 用户请求撤销申请, ApplicationState变成Aborted
    /// </summary>
    [Serializable]
    public class CancelApplicationRequest : IReturn<VoidResponse>
    {
        public int ApplicationId { set; get; }
    }

    /// <summary>
    /// 确认收到, ApplicationState变成Done
    /// </summary>
    [Serializable]
    public class ConfirmApplicationDoneRequest : IReturn<VoidResponse>
    {
        public int ApplicationId { set; get; }
    }
}
