using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MDS.Client.NavigationPages;

namespace MDS.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyMainPage MyMainPage { set; get; } = new MyMainPage();
        private MyDeliveryPage MyDeliveryPage { set; get; } = new MyDeliveryPage();
        public MainWindow()
        {
            InitializeComponent();

            MyMainPageFrame.Content = MyMainPage;
            MyDeliveryPageFrame.Content = MyDeliveryPage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO 要根据用户类型调整tab，目前测试状态，禁用这段代码
            // RefreshTabDisplay();
        }

        private void RefreshTabDisplay()
        {
            ManagePageTab.Visibility = UserInfo.UserType == UserType.ADMIN ? Visibility.Visible : Visibility.Collapsed;
            MyDeliveryPageTab.Visibility = UserInfo.UserType == UserType.DELIVERER ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
