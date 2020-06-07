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
            var bytes = Encoding.UTF8.GetBytes(pwd + "3z2w@9!aas");     // Salt
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
            if (string.IsNullOrEmpty(LoginUserPhoneNumberTextBox.Text) || string.IsNullOrEmpty(LoginPasswordBox.Password))
            {
                // 本地用户名密码有效检测
                PART_SnackBar.IsActive = true;
                SnackBarContent.Content = "用户名或密码不能为空";
                return;
            }

            LoginResponse response = await NetworkHelper.GetAsync(new LoginRequest()
            {
                PhoneNumber = LoginUserPhoneNumberTextBox.Text,
                Password = Hash(LoginPasswordBox.Password)
            }).DisableElements(PrimaryButton, SwitchButton, LoginUserPhoneNumberTextBox, LoginPasswordBox)
            .Progress(PART_ProgressBar);

            if (response.UserId < 0)
            {
                // 服务器判断账号密码错误
                PART_SnackBar.IsActive = true;
                SnackBarContent.Content = "用户名或密码错误";
            }
            else
            {
                UserInfo.Id = response.UserId;
                UserInfo.PhoneNumber = LoginUserPhoneNumberTextBox.Text;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private async Task Register()
        {
            if (RegisterPasswordBox.Text.Length <= 6)
            {
                // 本地密码有效检测
                // TODO 具体规则待定，这里暂时设计成长度必须大于6
                // 最终确定后要在登陆面板把规则显示给用户
                PART_SnackBar.IsActive = true;
                SnackBarContent.Content = "密码长度过短";
                return;
            }

            RegisterResponse response = await NetworkHelper.GetAsync(new RegisterRequest()
            {
                PhoneNumber = RegisterUserPhoneNumberTextBox.Text,
                Password = Hash(RegisterPasswordBox.Text)
            }).DisableElements(PrimaryButton, SwitchButton, RegisterUserPhoneNumberTextBox, RegisterPasswordBox)
            .Progress(PART_ProgressBar);

            if (response.UserId < 0)
            {
                // 服务器判断账号密码错误
                PART_SnackBar.IsActive = true;
                SnackBarContent.Content = "不能注册，用户名已存在";
            }
            else
            {
                UserInfo.Id = response.UserId;
                UserInfo.PhoneNumber = RegisterUserPhoneNumberTextBox.Text;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
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
