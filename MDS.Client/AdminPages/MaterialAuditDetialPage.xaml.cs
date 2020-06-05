using System;
using System.Collections.Generic;
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
    /// MaterialAuditDetialPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialAuditDetialPage : Window
    {
        private MaterialAuditConstruct Info_List = new MaterialAuditConstruct();
        public MaterialAuditDetialPage(MaterialAuditConstruct List, int flag)
        {
            InitializeComponent();
            this.Info_List = List;
            AdminInfo.Event_Result = false;
            NumberTextBlock.Text = "编号: " + Info_List.Number;
            ApplicantTextBlock.Text = "申请人: " + Info_List.Applicant;
            TimeTextBlock.Text = "申请时间: " + Info_List.Time;
            StateTextBlock.Text = "申请状态: " + Info_List.State;
            ReviewerTextBlock.Text = "审核人: " + Info_List.Reviewer;
            ResultTextBlock.Text = "申请结果: " + Info_List.Result;
            ContentTextBlock.Text = "申请内容: " + Info_List.Content;
            RemarksTextBlock.Text = "备注: " + Info_List.Remarks;
            if(flag == 1)
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
            //MaterialAuditConstruct flag = Info_List;
            Certain_Passward certain_Passward = new Certain_Passward(2, Info_List.Number);
            certain_Passward.ShowDialog();
        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {
            Certain_Passward certain_Passward = new Certain_Passward(1, Info_List.Number);
            certain_Passward.ShowDialog();
        }

        
    }
}
