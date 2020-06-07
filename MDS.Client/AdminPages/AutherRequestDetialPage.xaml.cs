using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
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
        private string Add_Result;

        public AutherRequestDetialPage(AutherRequestConstruct List, int flag)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.Info_List = List;
            switch (Info_List.Result)
            {
                case AdminResult.FAIL:
                    Add_Result = "未通过";
                    break;
                case AdminResult.NONE:
                    Add_Result = "未处理";
                    break;
                case AdminResult.PASS:
                    Add_Result = "通过";
                    break;
                default:
                    break;
            }
            NumberTextBlock.Text = "编号: " + Info_List.Number;
            UserTextBlock.Text = "认证人: " + Info_List.UserID;
            TimeTextBlock.Text = "认证时间: " + Info_List.Time;
            StateTextBlock.Text = "认证状态: " + Info_List.State;
            ReviewTypeTextBlock.Text = "认证类型: " + Info_List.Type;
            ReviewerTextBlock.Text = "审核人: " + Info_List.ReviewerID;
            ResultTextBlock.Text = "认证结果: " + Add_Result;
            ContentTextBlock.Text = "认证内容: " + Info_List.Content;
            RemarksTextBlock.Text = "备注: " + Info_List.Remarks;
            
            if (flag == 1)
            {
                Thread thread = new Thread(new ThreadStart(close_window));
                thread.Start();
            }
            else
            {
                agree_button.Visibility = Visibility.Hidden;
                refuse_button.Visibility = Visibility.Hidden;
            }
        }

        private void close_window()
        {
            //while (true)
            //{
            //    if (AdminInfo.Event_Result == true)
            //    {
            //        AdminInfo.Event_Result = false;
            //        Dispatcher.Invoke(
            //            new Action(
            //                delegate
            //                {
            //                    Close();
            //                }));
            //        break;
            //    }
            //    Thread.CurrentThread.Join(100);
            //}
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {
            Certain_Passward certain_Passward = new Certain_Passward(4, Info_List.Number);
            certain_Passward.ShowDialog();
        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {
            Certain_Passward certain_Passward = new Certain_Passward(3, Info_List.Number);
            certain_Passward.ShowDialog();
        }
    }

}
