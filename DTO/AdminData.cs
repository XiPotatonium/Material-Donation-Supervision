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
        public string Number { set; get; } //申请编号（用户认证、物资申请）
        public string User { set; get; } //申请人
        public DateTime Time { set; get; } //申请时间
        public AdminState State { set; get; }  //申请状态（已完成、待审核）
        public int Reviewer { set; get; } //审核人id
        public AdminResult Result { set; get; } //审核结果 PASS：通过 FAIL：未通过 NONE：未处理
        public string Content { set; get; } //申请的具体内容
        public string Remarks { set; get; }  //备注
    }

    /*
     用户申请认证
     */

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

    //同意申请认证，结果为PASS
    [Serializable]
    public class AutherRequestAgreeRequest : IReturn<AutherRequestAgreeResponse>
    {
        public int AdminID { set; get; } //管理员id
        public string Number { set; get; } //申请编号
        public string Secondary_passward { set; get; } //二级密码
    }

    [Serializable]
    public class AutherRequestAgreeResponse
    {
        public int flag { set; get; }  //0为密码正确，其他为错误
    }

    //拒绝申请认证，结果为FAIL
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

    /*
     物资申请
    */

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
        public string Secondary_passward { set; get; } //二级密码
    }

    [Serializable]
    public class MaterialAuditRefuseResponse
    {
        public int flag { set; get; }
    }

    /*
    管理员二级密码修改
    */

    [Serializable]
    public class SecondaryPasswordChangeRequest : IReturn<SecondaryPasswordChangeResponse>
    {
        public int AdminID { set; get; }
        public string Old_password { set; get; }  //原密码
        public string New_password { set; get; }  //新密码
    }

    public class SecondaryPasswordChangeResponse
    {
        public int flag { set; get; }  //0:成功 1:密码相同 2:密码为空
    }
}
