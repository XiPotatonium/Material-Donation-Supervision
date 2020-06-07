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
using MaterialDesignThemes.Wpf;
using MDS.Client.Extension;
using MDS.Client.NavigationPages;

namespace MDS.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 这两个是给全局异常处理用的，Pages不要使用这个
        public static Snackbar Snackbar { private set; get; } = null;
        public static SnackbarMessage SnackbarMessage { private set; get; } = null;

        /// <summary>
        /// 用这个来显示异常
        /// </summary>
        /// <param name="message"></param>
        public static void SetSnackBarContentAndPopup(string message)
        {
            SnackbarMessage.Content = message;
            Snackbar.IsActive = true;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Snackbar = PART_SnackBar;
            SnackbarMessage = SnackBarContent;
            PART_Frame.Content = new MyMainPage(this);

            UserInfoResponse response = await NetworkHelper.GetAsync(new UserInfoRequest()
            {
                UserId = UserInfo.Id
            }).Progress(PART_ProgressBar);

            UserInfo.PhoneNumber = response.PhoneNumber;
            UserInfo.HomeAddress = response.HomeAddress;
            UserInfo.UserType = response.UserType;
            // 要根据用户类型调整tab，目前测试状态，禁用这段代码
            RefreshTabDisplay();
        }

        /// <summary>
        /// 根据用户类型调整tab的显示状态
        /// </summary>
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

        public void NavigateToMainPage()
        {
            MainTabControl.SelectedItem = MyMainPageTab;
            PART_Frame.Content = new MyMainPage(this);
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

        private void SnackBarContent_ActionClick(object sender, RoutedEventArgs e)
        {
            PART_SnackBar.IsActive = false;
        }
    }
}
