using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// AutherRequestDetialPage.xaml 的交互逻辑
    /// </summary>
    public partial class AutherRequestDetialPage : Window
    {
        private AutherRequestConstruct Info_List = new AutherRequestConstruct();

        public AutherRequestDetialPage(AutherRequestConstruct List)
        {
            InitializeComponent();
            this.Info_List = List;
            NumberTextBlock.Text = "编号: " + Info_List.Number;
            UserTextBlock.Text = "认证人: " + Info_List.User;
            TimeTextBlock.Text = "认证时间: " + Info_List.Time;
            StateTextBlock.Text = "认证状态: " + Info_List.State;
            ReviewerTextBlock.Text = "审核人: " + Info_List.Reviewer;
            ResultTextBlock.Text = "认证结果: " + Info_List.Result;
            ContentTextBlock.Text = "认证内容: " + Info_List.Content;
            RemarksTextBlock.Text = "备注: " + Info_List.Remarks;
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    /*public class StringToDisplay : INotifyPropertyChanging
    {
        private string text { set; get; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }*/
}
