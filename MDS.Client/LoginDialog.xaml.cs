using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDS.Client
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        enum LoginDialogMode
        {
            LOGIN, REGISTER
        }

        private LoginDialogMode Mode { set; get; } = LoginDialogMode.LOGIN;

        public LoginDialog()
        {
            InitializeComponent();
        }

        private async void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
            PrimaryButton.IsEnabled = false;    // 防止重复按下
            await Task.Delay(1);                // 模拟网络延迟
            LoginResponse loginResponse = NetworkHelper.Get(new LoginRequest()
            {
                UserName = LoginUserNameTextBox.Text,
                Password = LoginPasswordBox.Password    // TODO 哈希
            });

            // TODO 假数据
            UserInfo.Id = loginResponse.UserId;
            UserInfo.Name = "UXX65535";
            UserInfo.PhoneNumber = "152-1111-1111";
            UserInfo.HomeAddress = "XX省-XX市-XX区-XX街道-XX小区-XXXXXXXXXXXXX";
            UserInfo.UserType = UserType.ADMIN;

            MessageBox.Show($"DEBUG: UserID={loginResponse.UserId}");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (Mode)
            {
                case LoginDialogMode.LOGIN:
                    Mode = LoginDialogMode.REGISTER;
                    PrimaryButton.Content = "注册";
                    SwitchButton.Content = "去登陆";
                    LoginRoot.Visibility = Visibility.Collapsed;
                    RegisterRoot.Visibility = Visibility.Visible;
                    break;
                case LoginDialogMode.REGISTER:
                    Mode = LoginDialogMode.LOGIN;
                    PrimaryButton.Content = "登陆";
                    SwitchButton.Content = "去注册";
                    LoginRoot.Visibility = Visibility.Visible;
                    RegisterRoot.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
    }
}
