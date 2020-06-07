using DTO;
using MDS.Client.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private DonationListViewModel DonationViewModel { set; get; } = null;
        private DonationDetailViewModel DonationDetailViewModel { set; get; } = null;
        private ObservableCollection<DonationMaterialListViewModel> MaterialListViewModels { set; get; } = null;

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
            DonationViewModel = userDonationViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DonationViewModel != null)
            {
                // 查看已存在的捐赠
                // 1. 网络请求
                DonationDetailViewModel = new DonationDetailViewModel(await NetworkHelper.GetAsync(new GetDonationDetailRequest()
                {
                    UserId = UserInfo.Id,
                    DonationId = DonationViewModel.OriginalItem.ID
                })).Progress(ParentWindow.PART_ProgressBar);

                // 2. 根据DonationViewModel来生成正确的Tab显示
                switch (DonationViewModel.OriginalItem.State)
                {
                    case DonationState.Applying:
                        PART_Stepper.Controller.GotoStep(1);
                        break;
                    case DonationState.WaitingDelivery:
                        PART_Stepper.Controller.GotoStep(2);
                        break;
                    case DonationState.Done:
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
                AvailableDonationMaterialResponse response = await NetworkHelper.GetAsync(new AvailableDonationMaterialRequest() { })
                    .Progress(ParentWindow.PART_ProgressBar);

                MaterialListViewModels = new ObservableCollection<DonationMaterialListViewModel>(response.Items.Select(i => new DonationMaterialListViewModel(i)));
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
                CardDonationId.Text = DonationViewModel.Id;
                CardDonationName.Text = DonationViewModel.Name;
                CardDonationQuantity.Text = DonationViewModel.Quantity.ToString();
                CardDonationTime.Text = DonationViewModel.StartTime.ToString();
                CardAddress.Text = DonationDetailViewModel.Address;
            }
            else
            {
                // 第一步，不需要显示当前订单卡片
                PART_Card.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshAddressInfo()
        {
            UserPhoneNumberTextBlock.Text = UserInfo.PhoneNumber;
            UserAddressTextBlock.Text = UserInfo.HomeAddress;
        }

        private async void PART_Stepper_ContinueNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            // 发送申请
            DonationMaterialListViewModel selected = (DonationMaterialListViewModel)MaterialSelectListBox.SelectedItem;
            if (selected == null)
            {
                ParentWindow.SetSnackBarContentAndPopup("请选择要捐赠的物资");
                args.Cancel = true;
                return;
            }
            else if (QuantityInputBox.Value == null || QuantityInputBox.Value <= 0)
            {
                ParentWindow.SetSnackBarContentAndPopup("不合法的数目");
                args.Cancel = true;
                return;
            }

            NewDonationResponse response = await NetworkHelper.GetAsync(new NewDonationRequest()
            {
                MaterialId = selected.OriginItem.Id,
                Quantity = (int)QuantityInputBox.Value,
                Address = UserInfo.HomeAddress
            }).Progress(ParentWindow.PART_ProgressBar);

            DonationViewModel = new DonationListViewModel(response.Item);
            DonationDetailViewModel = new DonationDetailViewModel(await NetworkHelper.GetAsync(new GetDonationDetailRequest()
            {
                UserId = UserInfo.Id,
                DonationId = DonationViewModel.OriginalItem.ID
            })).Progress(ParentWindow.PART_ProgressBar);
            RefreshApplicationCardView();
        }

        private async void PART_Stepper_CancelNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            await NetworkHelper.GetAsync(new CancelDonationRequest() { DonationId = DonationViewModel.OriginalItem.ID })
                .Progress(ParentWindow.PART_ProgressBar);
        }

        private void MaterialSelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DonationMaterialListViewModel selected = (DonationMaterialListViewModel)MaterialSelectListBox.SelectedItem;
            if (selected != null)
            {
                MaterialNameTextBlock.Text = selected.Name;
                MaterialDetailTextBlock.Text = selected.Description;
            }
            else
            {
                MaterialNameTextBlock.Text = "请选择想要捐赠的物资";
            }
        }
    }


    public class DonationDetailViewModel
    {
        public string Address { set; get; }
        public GetDonationDetailResponse OriginalItem { get; }

        public DonationDetailViewModel(GetDonationDetailResponse response)
        {
            OriginalItem = response;

            Address = response.Address;
        }

        internal DonationDetailViewModel Progress(ProgressBar pART_ProgressBar)
        {
            throw new NotImplementedException();
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
