using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for MyMainPage.xaml
    /// </summary>
    public partial class MyMainPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private ObservableCollection<ApplicationListViewModel> UserApplications { set; get; }
        private ObservableCollection<DonationListViewModel> UserDonations { set; get; }

        private MyMainPage()
        {
            InitializeComponent();
        }

        public MyMainPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO 这里会有一个网络请求，假装网络延时
            await UpdateApplicationList();
            await UpdateDonationList();
            RefreshUserInfoDisplay();
        }

        private async Task UpdateApplicationList()
        {
            await Task.Delay(100);

            // TODO 假数据
            UserApplications = new ObservableCollection<ApplicationListViewModel>();
            UserApplications.Add(new ApplicationListViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "消毒水(500ml)",
                Quantity = 5,
                State = "已送达",
                StartTime = DateTime.Now
            });
            UserApplications.Add(new ApplicationListViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = "已送达",
                StartTime = DateTime.Now
            });

            UserApplicationList.ItemsSource = UserApplications;
        }

        private async Task UpdateDonationList()
        {
            await Task.Delay(100);

            UserDonations = new ObservableCollection<DonationListViewModel>();
            UserDonations.Add(new DonationListViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = "已送达",
                StartTime = DateTime.Now
            });

            UserDonationList.ItemsSource = UserDonations;
        }

        private void RefreshUserInfoDisplay()
        {
            UserNameTextBlock.Text = UserInfo.Name;
            switch (UserInfo.UserType)
            {
                case UserType.NORMAL:
                    UserTypeTextBlock.Text = "普通用户";
                    break;
                case UserType.ADMIN:
                    UserTypeTextBlock.Text = "管理员";
                    break;
                case UserType.DELIVERER:
                    UserTypeTextBlock.Text = "配送员";
                    break;
                default:
                    UserTypeTextBlock.Text = "NT用户";
                    break;
            }
            PhoneNumberTextBlock.Text = UserInfo.PhoneNumber;
            HomeAddressTextBlock.Text = UserInfo.HomeAddress;
        }

        private void UserApplicationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplicationListViewModel userApplicationViewModel = (ApplicationListViewModel)UserApplicationList.SelectedItem;
            if (userApplicationViewModel != null)
            {
                ParentWindow.NavigateToApplicationPageAndDisplay(userApplicationViewModel);
            }
        }

        private void UserDonationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DonationListViewModel userDonationViewModel = (DonationListViewModel)UserDonationList.SelectedItem;
            if (userDonationViewModel != null)
            {
                ParentWindow.NavigateToDonationPageAndDisplay(userDonationViewModel);
            }
        }

        private void ModifyInfoButton_Click(object sender, RoutedEventArgs e)
        {
            NewPhoneTextBox.Text = UserInfo.PhoneNumber;
            NewAddressTextBox.Text = UserInfo.HomeAddress;
            ModifyInfoDialog.IsOpen = true;
        }

        private async void DialogConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyInfoDialog.IsOpen = false;
            // TODO 发送请求
            await Task.Delay(100);
            RefreshUserInfoDisplay();
        }

        private void DialogCancelButton_Click(object sender, RoutedEventArgs e)
        {
            ModifyInfoDialog.IsOpen = false;
        }
    }

    public class ApplicationListViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }
    }

    public class DonationListViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }
    }
}
