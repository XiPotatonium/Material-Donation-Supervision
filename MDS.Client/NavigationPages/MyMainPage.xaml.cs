using DTO;
using MDS.Client.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            await UpdateApplicationList();
            await UpdateDonationList();
            RefreshUserInfoDisplay();
        }

        private async Task UpdateApplicationList()
        {
            GetApplicationListResponse response = await NetworkHelper.GetAsync(new GetApplicationListRequest()
            {
                UserId = UserInfo.Id
            }).Progress(ParentWindow.PART_ProgressBar);

            // TODO 删掉假数据
            response = new GetApplicationListResponse() { Items = new List<GetApplicationListResponse.Item>() };
            response.Items.Add(new GetApplicationListResponse.Item()
            {
                ID = 0,
                GUID = "uhsdhasdbqwi2178412d",
                Name = "消毒水(500ml)",
                Quantity = 5,
                State = ApplicationState.Received,
                StartTime = DateTime.Now
            });
            response.Items.Add(new GetApplicationListResponse.Item()
            {
                ID = 1,
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = ApplicationState.Delivering,
                StartTime = DateTime.Now
            });

            UserApplications = new ObservableCollection<ApplicationListViewModel>(
                response.Items.Select(i => new ApplicationListViewModel(i)));
            UserApplicationList.ItemsSource = UserApplications;
        }

        private async Task UpdateDonationList()
        {
            GetDonationListResponse response = await NetworkHelper.GetAsync(new GetDonationListRequest()
            {
                UserId = UserInfo.Id
            }).Progress(ParentWindow.PART_ProgressBar);

            // TODO 删除假数据
            response = new GetDonationListResponse() { Items = new List<GetDonationListResponse.Item>() };
            response.Items.Add(new GetDonationListResponse.Item()
            {
                GUID = "uhsdhasdbqwi2178412d",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                State = DonationState.Done,
                StartTime = DateTime.Now
            });

            UserDonations = new ObservableCollection<DonationListViewModel>(
                response.Items.Select(i => new DonationListViewModel(i)));

            UserDonationList.ItemsSource = UserDonations;
        }

        private void RefreshUserInfoDisplay()
        {
            UserNameTextBlock.Text = UserInfo.PhoneNumber;
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
            HomeAddressTextBlock.Text = UserInfo.HomeAddress;
        }

        private void UserApplicationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ApplicationListViewModel userApplicationViewModel = (ApplicationListViewModel)UserApplicationList.SelectedItem;
            if (userApplicationViewModel != null && userApplicationViewModel.OriginalItem.State != ApplicationState.Aborted)
            {
                // Aborted不允许进入详情页
                ParentWindow.NavigateToApplicationPageAndDisplay(userApplicationViewModel);
            }
        }

        private void UserDonationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DonationListViewModel userDonationViewModel = (DonationListViewModel)UserDonationList.SelectedItem;
            if (userDonationViewModel != null && userDonationViewModel.OriginalItem.State != DonationState.Aborted)
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

            // TODO 输入合法性检查
            await NetworkHelper.GetAsync(new UserInfoModifyRequest()
            {
                PhoneNumber = NewPhoneTextBox.Text,
                HomeAddress = NewAddressTextBox.Text
            }).Progress(ParentWindow.PART_ProgressBar);

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

        public GetApplicationListResponse.Item OriginalItem { get; }

        public ApplicationListViewModel(GetApplicationListResponse.Item item)
        {
            OriginalItem = item;

            GUID = item.GUID;
            Name = item.Name;
            Quantity = item.Quantity;
            StartTime = item.StartTime;
            switch (item.State)
            {
                case ApplicationState.Aborted:
                    State = "已撤销";
                    break;
                case ApplicationState.Applying:
                    State = "待审核";
                    break;
                case ApplicationState.Delivering:
                    State = "配送中";
                    break;
                case ApplicationState.Received:
                    State = "已送达";
                    break;
                case ApplicationState.Done:
                    State = "已完成";
                    break;
                default:
                    State = "UNK";
                    break;
            }
        }
    }

    public class DonationListViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }

        public GetDonationListResponse.Item OriginalItem { get; }

        public DonationListViewModel(GetDonationListResponse.Item item)
        {
            OriginalItem = item;

            GUID = item.GUID;
            Name = item.Name;
            Quantity = item.Quantity;
            StartTime = item.StartTime;
            switch (item.State)
            {
                case DonationState.Aborted:
                    State = "已撤销";
                    break;
                case DonationState.Applying:
                    State = "待审批";
                    break;
                case DonationState.WaitingDelivery:
                    State = "待配送";
                    break;
                case DonationState.Done:
                    State = "已完成";
                    break;
                default:
                    State = "UNK";
                    break;
            }
        }
    }
}
