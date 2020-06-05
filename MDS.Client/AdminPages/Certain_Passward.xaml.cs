using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using DTO;
using System.Security.Cryptography;

namespace MDS.Client.AdminPages
{
    /// <summary>
    /// Certain_Passward.xaml 的交互逻辑
    /// </summary>
    public partial class Certain_Passward : Window
    {
        private string secondary_passward;
        private int mode; //模式：同意为1；拒绝为2
        private string Number; //申请编号
        public Certain_Passward(int mode, string Number)
        {
            InitializeComponent();
            this.mode = mode;
            this.Number = Number;
        }

        private async void certain_click(object sender, RoutedEventArgs e)
        {
            switch (this.mode)
            {
                case 1:
                    MaterialAuditAgreeResponse materialAuditMoveResponse_agree = await NetworkHelper.GetAsync(new MaterialAuditAgreeRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = this.Number,
                        Secondary_passward = certain_password.Password
                    });
                    materialAuditMoveResponse_agree = new MaterialAuditAgreeResponse() { flag = 0 };
                    if(materialAuditMoveResponse_agree.flag == 0)
                    {
                        MessageBox.Show("已同意");
                        AdminInfo.Event_Result = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                        certain_password.Password = "";
                    }
                    break;
                case 2:
                    MaterialAuditRefuseResponse materialAuditMoveResponse_refuse = await NetworkHelper.GetAsync(new MaterialAuditRefuseRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = this.Number,
                        Secondary_passward = certain_password.Password
                    });
                    materialAuditMoveResponse_refuse = new MaterialAuditRefuseResponse() { flag = 0 };
                    if (materialAuditMoveResponse_refuse.flag == 0)
                    {
                        MessageBox.Show("已拒绝");
                        AdminInfo.Event_Result = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                        certain_password.Password = "";
                    }
                    break;
                case 3:
                    AutherRequestAgreeResponse autherRequestMoveResponse_agree = await NetworkHelper.GetAsync(new AutherRequestAgreeRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = this.Number,
                        Secondary_passward = certain_password.Password
                    });
                    autherRequestMoveResponse_agree = new AutherRequestAgreeResponse() { flag = 0 };
                    if (autherRequestMoveResponse_agree.flag == 0)
                    {
                        MessageBox.Show("已同意");
                        AdminInfo.Event_Result = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                        certain_password.Password = "";
                    }
                    break;
                case 4:
                    AutherRequestRefuseResponse autherRequestMoveResponse_refuse = await NetworkHelper.GetAsync(new AutherRequestRefuseRequest()
                    {
                        AdminID = UserInfo.Id,
                        Number = this.Number,
                        Secondary_passward = certain_password.Password
                    });
                    autherRequestMoveResponse_refuse = new AutherRequestRefuseResponse() { flag = 0 };
                    if (autherRequestMoveResponse_refuse.flag == 0)
                    {
                        MessageBox.Show("已拒绝");
                        AdminInfo.Event_Result = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                        certain_password.Password = "";
                    }
                    break;
                default:
                    break;
            }
         
        }

        private static string Hash(string pwd)
        {
            var bytes = Encoding.UTF8.GetBytes(pwd + "3z2w@9!aas");     // Salt
            var bytesHashed = SHA256.Create().ComputeHash(bytes);
            string hashed = Convert.ToBase64String(bytesHashed);
            return hashed;
        }
    }
}
