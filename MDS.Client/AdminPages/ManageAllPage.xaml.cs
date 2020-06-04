using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDS.Client.AdminPages
{
    /// <summary>
    /// ManageAllPage.xaml 的交互逻辑
    /// </summary>
    public partial class ManageAllPage : Page
    {
        public ManageAllPage()
        {
            InitializeComponent();
        }
    }

    public class AutherRequestConstruct
    {
        public string Number { set; get; }
        public string User { set; get; }
        public DateTime Time { set; get; }
        //public Button Detail { set; get; }
        public AdminState State { set; get; }
        public string Reviewer { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }

    }

    public class MaterialAuditConstruct
    {
        public string Number { set; get; }
        public string Applicant { set; get; }
        public DateTime Time { set; get; }
        //public Button Detail { set; get; }
        public AdminState State { set; get; }
        public string Reviewer { set; get; }
        public AdminResult Result { set; get; }
        public string Content { set; get; }
        public string Remarks { set; get; }
    }
}
