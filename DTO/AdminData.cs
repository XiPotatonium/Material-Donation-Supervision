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
        public string Reviewer { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }
    }

    public class AutherRequestList
    {
        public List<Normal> a_normals { set; get; }
    }

    public class MaterialAuditList
    {
        public List<Normal> m_normals { set; get; }
    }
}
