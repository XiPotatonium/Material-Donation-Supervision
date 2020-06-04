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
    /// MaterialAuditDetialPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialAuditDetialPage : Window
    {
        private MaterialAuditConstruct Info_List = new MaterialAuditConstruct();
        public MaterialAuditDetialPage(MaterialAuditConstruct List)
        {
            InitializeComponent();
            this.Info_List = List;
            NumberTextBlock.Text = "编号: " + Info_List.Number;
            ApplicantTextBlock.Text = "申请人: " + Info_List.Applicant;
            TimeTextBlock.Text = "申请时间: " + Info_List.Time;
            StateTextBlock.Text = "申请状态: " + Info_List.State;
            ReviewerTextBlock.Text = "审核人: " + Info_List.Reviewer;
            ResultTextBlock.Text = "申请结果: " + Info_List.Result;
            ContentTextBlock.Text = "申请内容: " + Info_List.Content;
            RemarksTextBlock.Text = "备注: " + Info_List.Remarks;
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
