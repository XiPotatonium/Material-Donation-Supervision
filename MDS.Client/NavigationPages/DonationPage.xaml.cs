using DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for DonatePage.xaml
    /// </summary>
    public partial class DonationPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private DonationListViewModel UserDonationViewModel { set; get; } = null;

        private DonationPage()
        {
            InitializeComponent();
        }

        public DonationPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        public DonationPage(MainWindow parent, DonationListViewModel userDonationViewModel)
        {
            InitializeComponent();

            ParentWindow = parent;
            UserDonationViewModel = userDonationViewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserDonationViewModel != null)
            {
                // 查看已存在的捐赠
                // 1. 网络请求
                ApplicationDetailViewModel = new ApplicationDetailViewModel(await NetworkHelper.GetAsync(new GetApplicationDetailRequest()
                {
                    UserId = UserInfo.Id,
                    ApplicationId = ApplicationViewModel.OriginalItem.ID
                }));

                // 2. 根据ApplicationViewModel和ApplicationDetailViewModel来生成正确的Tab显示
                switch (ApplicationViewModel.OriginalItem.State)
                {
                    case ApplicationState.Applying:
                        PART_Stepper.Controller.GotoStep(1);
                        break;
                    case ApplicationState.Delivering:
                        PART_Stepper.Controller.GotoStep(2);
                        break;
                    case ApplicationState.Received:
                        ArrivedPanel.Visibility = Visibility.Visible;
                        AllDonePanel.Visibility = Visibility.Collapsed;
                        ConfirmStepBar.Visibility = Visibility.Visible;
                        PART_Stepper.Controller.GotoStep(3);
                        break;
                    case ApplicationState.Done:
                        ArrivedPanel.Visibility = Visibility.Collapsed;
                        AllDonePanel.Visibility = Visibility.Visible;
                        ConfirmStepBar.Visibility = Visibility.Collapsed;
                        PART_Stepper.Controller.GotoStep(3);
                        break;
                    default:
                        PART_Stepper.Controller.GotoStep(1);
                        break;
                }
            }
            else
            {
                // 填写新申请
                // 1. 请求所有可选的物资
                AvailableApplicationMaterialResponse response = await NetworkHelper.GetAsync(new AvailableApplicationMaterialRequest() { });
                // TODO 删除假数据
                response = new AvailableApplicationMaterialResponse() { Items = new List<AvailableApplicationMaterialResponse.Item>() };
                response.Items.Add(new AvailableApplicationMaterialResponse.Item()
                {
                    Name = "物资1",
                    Constraint = 10,
                    Description = "物资一的介绍"
                });
                response.Items.Add(new AvailableApplicationMaterialResponse.Item()
                {
                    Name = "物资2",
                    Constraint = 100,
                    Description = "物资er的介绍"
                });

                MaterialListViewModels = new ObservableCollection<ApplicationMaterialListViewModel>(response.Items.Select(i => new ApplicationMaterialListViewModel(i)));
                MaterialSelectListBox.ItemsSource = MaterialListViewModels;

                // 2. 初始化好填写界面
                RefreshAddressInfo();
                PART_Stepper.Controller.GotoStep(0);
            }

            RefreshApplicationCardView();
        }

        private void RefreshApplicationCardView()
        {
            if (PART_Stepper.Controller.PreviousStep != null)
            {
                // 不是第一步，需要显示订单的详细内容
                PART_Card.Visibility = Visibility.Visible;
                CardApplicationGUID.Text = ApplicationViewModel.GUID;
                CardApplicationName.Text = ApplicationViewModel.Name;
                CardApplicationQuantity.Text = ApplicationViewModel.Quantity.ToString();
                CardApplicationTime.Text = ApplicationViewModel.StartTime.ToString();
                CardAddress.Text = ApplicationDetailViewModel.Address;
            }
            else
            {
                // 第一步，不需要显示当前订单卡片
                PART_Card.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshAddressInfo()
        {
            UserNameTextBlock.Text = UserInfo.PhoneNumber;
            UserAddressTextBlock.Text = UserInfo.HomeAddress;
        }

        private void TabControlStepper_ContinueNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {

        }

        private async void TabControlStepper_CancelNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            await NetworkHelper.GetAsync(new CancelDonationRequest() { DonationId = UserDonationViewModel.OriginalItem.ID });
        }
    }


    public class DonationDetailViewModel
    {
        public string Address { set; get; }
        public GetApplicationDetailResponse OriginalItem { get; }

        public DonationDetailViewModel(GetDonationDetailResponse response)
        {
            OriginalItem = response;

            Address = response.Address;
        }
    }

    /// <summary>
    /// 可捐赠物资列表
    /// </summary>
    public class DonationMaterialListViewModel
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public AvailableDonationMaterialResponse.Item OriginItem { get; }

        public DonationMaterialListViewModel(AvailableDonationMaterialResponse.Item item)
        {
            OriginItem = item;

            Name = item.Name;
            Description = item.Description;
        }
    }
}
