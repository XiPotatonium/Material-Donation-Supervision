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
using DTO;
using MDS.Client.NavigationPages;

namespace MDS.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PART_Frame.Content = new MyMainPage(this);
            // TODO 用户信息假数据
            UserInfoResponse response = new UserInfoResponse()
            {
                Name = "UXX65535",
                PhoneNumber = "152-1111-1111",
                HomeAddress = "XX省-XX市-XX区-XX街道-XX小区-XXXXXXXXXXXXX",
                UserType = UserType.ADMIN
            };
            UserInfo.Name = response.Name;
            UserInfo.PhoneNumber = response.PhoneNumber;
            UserInfo.HomeAddress = response.HomeAddress;
            UserInfo.UserType = response.UserType;
            // TODO 要根据用户类型调整tab，目前测试状态，禁用这段代码
            // RefreshTabDisplay();
        }

        private void RefreshTabDisplay()
        {
            ManagePageTab.Visibility = UserInfo.UserType == UserType.ADMIN ? Visibility.Visible : Visibility.Collapsed;
            MyDeliveryPageTab.Visibility = UserInfo.UserType == UserType.DELIVERER ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] == ApplicationPageTab)
            {
                PART_Frame.Content = new ApplicationPage(this);
            }
            else if (e.AddedItems[0] == DonationPageTab)
            {
                PART_Frame.Content = new DonationPage(this);
            }
            else if (e.AddedItems[0] == ManagePageTab)
            {
                PART_Frame.Content = new ManagePage();
            }
            else if (e.AddedItems[0] == MyDeliveryPageTab)
            {
                PART_Frame.Content = new MyDeliveryPage();
            }
            else
            {
                PART_Frame.Content = new MyMainPage(this);
            }
        }

        public void NavigateToApplicationPageAndDisplay(ApplicationListViewModel userApplicationViewModel)
        {
            MainTabControl.SelectedItem = ApplicationPageTab;
            PART_Frame.Content = new ApplicationPage(this, userApplicationViewModel);
        }

        public void NavigateToDonationPageAndDisplay(DonationListViewModel userDonationViewModel)
        {
            MainTabControl.SelectedItem = DonationPageTab;
            PART_Frame.Content = new DonationPage(this, userDonationViewModel);
        }

        /// <summary>
        /// ProgressBar控制，开启后ProgressBar动画开启，结束后ProgressBar涨到慢
        /// </summary>
        /// <param name="isInDeterminate">true为开启动画，false为结束动画</param>
        public void SetProgressBar(bool isInDeterminate)
        {
            PART_ProgressBar.IsIndeterminate = isInDeterminate;
            if (!isInDeterminate)
            {
                // TODO 可以做的更柔和一点
                PART_ProgressBar.Value = 100;
            }
        }
    }
}
