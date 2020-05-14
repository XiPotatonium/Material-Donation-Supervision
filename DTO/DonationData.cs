using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    [Serializable]
    public class GetApplicationListRequest : IReturn<GetApplicationListResponse>
    {
        public int UserId { set; get; }
    }

    [Serializable]
    public class GetApplicationListResponse
    {
        // TODO
    }


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
}
