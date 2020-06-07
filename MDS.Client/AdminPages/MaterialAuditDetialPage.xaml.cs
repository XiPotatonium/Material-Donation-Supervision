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
using DTO;

namespace MDS.Client.AdminPages
{
    /// <summary>
    /// MaterialAuditDetialPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialAuditDetialPage : Window
    {
        private MaterialAuditConstruct Info_List = new MaterialAuditConstruct();
        private string Add_Result;
        public MaterialAuditDetialPage(MaterialAuditConstruct List, int flag)
        {
            InitializeComponent();
            Info_List = List;
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
            AdminInfo.Event_Result = false;
            NumberTextBlock.Text = "申请编号: " + Info_List.Number;
            ApplicantTextBlock.Text = "申请人: " + Info_List.ApplicantID;
            TimeTextBlock.Text = "申请时间: " + Info_List.Time;
            StateTextBlock.Text = "申请状态: " + Info_List.State;
            ReviewTypeTextBlock.Text = "申请类型: " + Info_List.Type;
            ReviewerTextBlock.Text = "审核人: " + Info_List.ReviewerID;
            ResultTextBlock.Text = "申请结果: " + Add_Result;
            ContentTextBlock.Text = "申请内容: " + Info_List.Content;
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
