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

namespace MDS.Client.AdminPages
{
    /// <summary>
    /// Change_Passward.xaml 的交互逻辑
    /// </summary>
    public partial class Change_Passward : Window
    {
        public Change_Passward()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private async void change_click(object sender, RoutedEventArgs e)
        {
            SecondaryPasswordChangeResponse secondaryPasswordChangeResponse = await NetworkHelper.GetAsync(new SecondaryPasswordChangeRequest()
            {
                AdminID = UserInfo.Id,
                New_password = new_passward.Password,
                Old_password = old_passward.Password
            });
            secondaryPasswordChangeResponse = new SecondaryPasswordChangeResponse() { flag = 0 };  //测试
            if (secondaryPasswordChangeResponse.flag == 0)
            {
                MessageBox.Show("修改成功");
                this.Close();
            }
            else if(secondaryPasswordChangeResponse.flag == 1)
            {
                MessageBox.Show("密码相同");
            }
            else if (secondaryPasswordChangeResponse.flag == 2)
            {
                MessageBox.Show("密码为空");
            }
            else if(secondaryPasswordChangeResponse.flag == 3)
            {
                MessageBox.Show("密码错误");
            }
            
        }
    }
}
