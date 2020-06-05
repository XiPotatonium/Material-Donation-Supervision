using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class AdminData
    {

    }

    public enum AdminState
    {
        FINISH, WAIT
    }

    public enum AdminResult
    {
        PASS, FAIL, NONE
    }

    public class Normal
    {
        public string Number { set; get; }
        public string User { set; get; }
        public DateTime Time { set; get; }
        public AdminState State { set; get; }
        public int Reviewer { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }
    }

    [Serializable]
    public class AutherRequestListRequest : IReturn<AutherRequestListResponse>
    {
        public int AdminID { set; get; }  //管理员ID
        public AdminState state { set; get; }  //当前任务状态
    }

    [Serializable]
    public class AutherRequestListResponse
    {
        public List<Normal> a_normals { set; get; }
    }

    [Serializable]
    public class AutherRequestAgreeRequest : IReturn<AutherRequestAgreeResponse>
    {
        public int AdminID { set; get; } //管理员id
        public string Number { set; get; } //申请编号
        public string Secondary_passward { set; get; }
    }

    [Serializable]
    public class AutherRequestAgreeResponse
    {
        public int flag { set; get; }
    }

    [Serializable]
    public class AutherRequestRefuseRequest : IReturn<AutherRequestRefuseResponse>
    {
        public int AdminID { set; get; } //管理员id
        public string Number { set; get; } //申请编号
        public string Secondary_passward { set; get; }
    }

    [Serializable]
    public class AutherRequestRefuseResponse
    {
        public int flag { set; get; }
    }

    [Serializable]
    public class MaterialAuditListRequest : IReturn<MaterialAuditListResponse>
    {
        public int AdminID { set; get; }
        public AdminState state { set; get; }
    }

    [Serializable]
    public class MaterialAuditListResponse
    {
        public List<Normal> m_normals { set; get; }
    }

    [Serializable]
    public class MaterialAuditAgreeRequest : IReturn<MaterialAuditAgreeResponse>
    {
        public int AdminID { set; get; } //管理员id
        public string Number { set; get; } //申请编号
        public string Secondary_passward { set; get; }
    }

    [Serializable]
    public class MaterialAuditAgreeResponse
    {
        public int flag { set; get; }
    }

    [Serializable]
    public class MaterialAuditRefuseRequest : IReturn<MaterialAuditRefuseResponse>
    {
        public int AdminID { set; get; } //管理员id
        public string Number { set; get; } //申请编号
        public string Secondary_passward { set; get; }
    }

    [Serializable]
    public class MaterialAuditRefuseResponse
    {
        public int flag { set; get; }
    }
}
