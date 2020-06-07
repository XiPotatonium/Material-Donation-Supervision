using MDS.Client.DeliveryPages;
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

using MDS.Client.AdminPages;

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        private MainWindow parentWindow { get; } = null;
        //private ManageAllPage myManageAllPage { set; get; } = new ManageAllPage();
        private MaterialAuditPage myMaterialAuditPage { set; get; } = new MaterialAuditPage();
        public ManagePage()
        {
            InitializeComponent();
            //ManageAllPageFrame.Content = myManageAllPage;
            MaterialAuditPageFrame.Content = myMaterialAuditPage;
        }
    }
}

