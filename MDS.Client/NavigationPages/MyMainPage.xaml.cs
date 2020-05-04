using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        private ObservableCollection<UserApplicationViewModel> UserApplications { set; get; }
        private ObservableCollection<UserDonationViewModel> UserDonations { set; get; }

        private MyMainPage()
        {
            InitializeComponent();
        }

        public MyMainPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO 假数据
            UserApplications = new ObservableCollection<UserApplicationViewModel>();
            UserApplications.Add(new UserApplicationViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "消毒水(500ml)",
                Quantity = 5,
                State = "已送达",
                StartTime = DateTime.Now
            });
            UserApplications.Add(new UserApplicationViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = "已送达",
                StartTime = DateTime.Now
            });

            UserApplicationList.ItemsSource = UserApplications;

            UserDonations = new ObservableCollection<UserDonationViewModel>();
            UserDonations.Add(new UserDonationViewModel()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = "已送达",
                StartTime = DateTime.Now
            });

            UserDonationList.ItemsSource = UserDonations;

            RefreshUserInfoDisplay();
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
            UserApplicationViewModel userApplicationViewModel = (UserApplicationViewModel)UserApplicationList.SelectedItem;
            if (userApplicationViewModel != null)
            {
                ParentWindow.NavigateToApplicationPageAndDisplay(userApplicationViewModel);
            }
        }
    }

    public class UserApplicationViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }
    }

    public class UserDonationViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }
    }
}
