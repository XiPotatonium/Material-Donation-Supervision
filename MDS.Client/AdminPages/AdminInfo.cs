using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace MDS.Client.AdminPages
{
    public class AutherRequestConstruct
    {
        public string Number { set; get; }
        public int UserID { set; get; }
        public DateTime Time { set; get; }
        //public Button Detail { set; get; }
        public string State { set; get; }
        public string Type { set; get; }
        public int ReviewerID { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }

    }

    public class MaterialAuditConstruct
    {
        public string Number { set; get; }
        public int ApplicantID { set; get; }
        public DateTime Time { set; get; }
        public string State { set; get; }
        public string Type { set; get; }
        public int ReviewerID { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }
    }
}
