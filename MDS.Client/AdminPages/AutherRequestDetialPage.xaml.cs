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

        public AutherRequestDetialPage(AutherRequestConstruct List, int flag)
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
            while (true)
            {
                if (AdminInfo.Event_Result == true)
                {
                    AdminInfo.Event_Result = false;
                    Dispatcher.Invoke(
                        new Action(
                            delegate
                            {
                                Close();
                            }));
                    break;
                }
                Thread.CurrentThread.Join(100);
            }
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
