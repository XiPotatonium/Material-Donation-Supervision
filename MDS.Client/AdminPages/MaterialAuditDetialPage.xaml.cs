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
        enum ConfirmSourceType
        {
            Agree, Refuse
        }

        private ConfirmSourceType ConfirmSource { set; get; }
        private MaterialAuditConstruct InfoList { set; get; }
        public MaterialAuditDetialPage(MaterialAuditConstruct list, int flag)
        {
            InitializeComponent();
            InfoList = list;
            string result = InfoList.Result switch
            {
                AdminResult.FAIL => "未通过",
                AdminResult.NONE => "未处理",
                AdminResult.PASS => "通过",
                _ => "UNK",
            };
            NumberTextBlock.Text = "申请编号: " + InfoList.Number;
            ApplicantTextBlock.Text = "申请人: " + InfoList.ApplicantID;
            TimeTextBlock.Text = "申请时间: " + InfoList.Time;
            StateTextBlock.Text = "申请状态: " + InfoList.State;
            ReviewTypeTextBlock.Text = "申请类型: " + InfoList.Type;
            ReviewerTextBlock.Text = "审核人: " + InfoList.ReviewerID;
            ResultTextBlock.Text = "申请结果: " + result;
            ContentTextBlock.Text = "申请内容: " + InfoList.Content;
            if(flag != 1)
            {
                agree_button.Visibility = Visibility.Hidden;
                refuse_button.Visibility = Visibility.Hidden;
            }
            
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {
            ConfirmSource = ConfirmSourceType.Refuse;
            SecondaryPasswordPopup.IsOpen = true;
        }

        private void Agree_Click(object sender, RoutedEventArgs e)
        {
            ConfirmSource = ConfirmSourceType.Agree;
            SecondaryPasswordPopup.IsOpen = true;
        }

        private void ShowWarning(string msg)
        {
            SnackBarContent.Content = msg;
            PART_SnackBar.IsActive = true;
        }

        private async void PasswordConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ConfirmSource)
            {
                case ConfirmSourceType.Agree:
                    MaterialAuditAgreeResponse materialAuditMoveResponse_agree = await NetworkHelper.GetAsync(new MaterialAuditAgreeRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = InfoList.Number,
                        Secondary_passward = SecondaryPasswordBox.Password
                    });

                    if (materialAuditMoveResponse_agree.flag == 0)
                    {
                        Close();
                    }
                    else
                    {
                        ShowWarning("密码错误");
                        SecondaryPasswordBox.Password = "";
                    }

                    break;
                case ConfirmSourceType.Refuse:
                    MaterialAuditRefuseResponse materialAuditMoveResponse_refuse = await NetworkHelper.GetAsync(new MaterialAuditRefuseRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = InfoList.Number,
                        Secondary_passward = SecondaryPasswordBox.Password
                    });

                    if (materialAuditMoveResponse_refuse.flag == 0)
                    {
                        Close();
                    }
                    else
                    {
                        ShowWarning("密码错误");
                        SecondaryPasswordBox.Password = "";
                    }

                    break;
                default:
                    break;
            }
        }

        private void PasswordCancelButton_Click(object sender, RoutedEventArgs e)
        {
            SecondaryPasswordPopup.IsOpen = false;
        }

        private void SnackBarContent_ActionClick(object sender, RoutedEventArgs e)
        {
            PART_SnackBar.IsActive = false;
        }
    }
}
