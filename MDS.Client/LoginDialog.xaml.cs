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

        private void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
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
                    SwitchButton.Content = "登陆";
                    LoginRoot.Visibility = Visibility.Collapsed;
                    RegisterRoot.Visibility = Visibility.Visible;
                    break;
                case LoginDialogMode.REGISTER:
                    Mode = LoginDialogMode.LOGIN;
                    PrimaryButton.Content = "登陆";
                    SwitchButton.Content = "注册";
                    LoginRoot.Visibility = Visibility.Visible;
                    RegisterRoot.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }
    }
}
