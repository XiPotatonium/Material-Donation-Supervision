using DTO;
using MDS.Client.Extension;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        private static string Hash(string pwd)
        {
            var bytes = Encoding.UTF8.GetBytes(pwd + "3z2w@9!aas");
            var bytesHashed = SHA256.Create().ComputeHash(bytes);
            string hashed = Convert.ToBase64String(bytesHashed);
            return hashed;
        }

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
            switch (Mode)
            {
                case LoginDialogMode.LOGIN:
                    await Login();
                    break;
                case LoginDialogMode.REGISTER:
                    await Register();
                    break;
                default:
                    break;
            }
        }

        private async Task Login()
        {
            // TODO: 这段代码开发的时候注释掉，上线的时候要用
            //if (string.IsNullOrEmpty(LoginUserNameTextBox.Text) || string.IsNullOrEmpty(LoginPasswordBox.Password))
            //{
            //    // 本地用户名密码有效检测
            //    PART_SnackBar.IsActive = true;
            //    SnackBarContent.Content = "用户名或密码不能为空";
            //    return;
            //}

            LoginResponse loginResponse = await NetworkHelper.GetAsync(new LoginRequest()
            {
                UserName = LoginUserNameTextBox.Text,
                Password = Hash(LoginPasswordBox.Password)
            }).DisableElements(PrimaryButton, SwitchButton);

            loginResponse = new LoginResponse() { UserId = 0 };    // TODO 假数据

            if (loginResponse.UserId < 0)
            {
                // 服务器判断账号密码错误
                PART_SnackBar.IsActive = true;
                SnackBarContent.Content = "用户名或密码错误";
            }
            else
            {
                UserInfo.Id = loginResponse.UserId;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private async Task Register()
        {
            throw new NotImplementedException();
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

        private void SnackBarContent_ActionClick(object sender, RoutedEventArgs e)
        {
            PART_SnackBar.IsActive = false;
        }
    }
}
