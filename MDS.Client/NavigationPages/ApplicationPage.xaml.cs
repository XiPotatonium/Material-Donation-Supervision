using System;
using System.Collections.Generic;
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
using System.Linq;
using System.Collections.ObjectModel;
using DTO;
using MDS.Client.Extension;

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for ApplyPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private ApplicationListViewModel ApplicationViewModel { set; get; } = null;
        private ApplicationDetailViewModel ApplicationDetailViewModel { set; get; } = null;
        private ObservableCollection<ApplicationMaterialListViewModel> MaterialListViewModels { set; get; } = null;

        private ApplicationPage()
        {
            InitializeComponent();
        }

        public ApplicationPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        public ApplicationPage(MainWindow parent, ApplicationListViewModel userApplicationViewModel)
        {
            InitializeComponent();

            ParentWindow = parent;
            ApplicationViewModel = userApplicationViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplicationViewModel != null)
            {
                // 查看已存在的申请
                // 1. 网络请求ApplicationDetailViewModel
                ApplicationDetailViewModel = new ApplicationDetailViewModel(await NetworkHelper.GetAsync(new GetApplicationDetailRequest()
                {
                    ApplicationId = ApplicationViewModel.OriginalItem.ID
                }).Progress(ParentWindow.PART_ProgressBar));

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
                AvailableApplicationMaterialResponse response = await NetworkHelper.GetAsync(
                    new AvailableApplicationMaterialRequest() { }).Progress(ParentWindow.PART_ProgressBar);
                // TODO 删除假数据
                response = new AvailableApplicationMaterialResponse() { Items = new List<AvailableApplicationMaterialResponse.Item>() };
                response.Items.Add(new AvailableApplicationMaterialResponse.Item()
                {
                    Id = 1,
                    Name = "物资1",
                    Constraint = 10,
                    Description = "物资一的介绍"      // TODO UI设计
                });
                response.Items.Add(new AvailableApplicationMaterialResponse.Item()
                {
                    Id = 2,
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
                CardApplicationId.Text = ApplicationViewModel.Id;
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

        private async void PART_Stepper_ContinueNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            int idx = 0;
            for (; idx < PART_Stepper.Controller.Steps.Length; ++idx)
            {
                if (args.CurrentStep == PART_Stepper.Controller.Steps[idx])
                {
                    break;      // 确定现在在哪个Step
                }
            }
            if (idx == 0)
            {
                // 发送申请
                ApplicationMaterialListViewModel selected = (ApplicationMaterialListViewModel)MaterialSelectListBox.SelectedItem;
                if (selected == null)
                {
                    ParentWindow.SetSnackBarContentAndPopup("请选择要申请的物资");
                    args.Cancel = true;
                    return;
                }
                else if (QuantityInputBox.Value == null || QuantityInputBox.Value <= 0)
                {
                    ParentWindow.SetSnackBarContentAndPopup("不合法的数目");
                    args.Cancel = true;
                    return;
                }
                else if (QuantityInputBox.Value > selected.Constraint)
                {
                    ParentWindow.SetSnackBarContentAndPopup("超出数量限制");
                    args.Cancel = true;
                    return;
                }

                NewApplicationResponse response = await NetworkHelper.GetAsync(new NewApplicationRequest()
                {
                    MaterialId = selected.OriginItem.Id,
                    Quantity = (int)QuantityInputBox.Value,
                    Address = UserInfo.HomeAddress
                }).Progress(ParentWindow.PART_ProgressBar);

                ApplicationViewModel = new ApplicationListViewModel(response.Item);
                ApplicationDetailViewModel = new ApplicationDetailViewModel(await NetworkHelper.GetAsync(new GetApplicationDetailRequest()
                {
                    ApplicationId = ApplicationViewModel.OriginalItem.ID
                }).Progress(ParentWindow.PART_ProgressBar));
                RefreshApplicationCardView();
            }
            else if (idx == 3)
            {
                // 确认
                await NetworkHelper.GetAsync(new ConfirmApplicationDoneRequest() { ApplicationId = ApplicationViewModel.OriginalItem.ID });
            }
        }

        private async void PART_Stepper_CancelNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            // 发送撤销请求的包
            await NetworkHelper.GetAsync(new CancelApplicationRequest() { ApplicationId = ApplicationViewModel.OriginalItem.ID })
                .Progress(ParentWindow.PART_ProgressBar);
            ParentWindow.SetSnackBarContentAndPopup("申请已取消");
            ParentWindow.NavigateToMainPage();
        }

        private void MaterialSelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationMaterialListViewModel selected = (ApplicationMaterialListViewModel)MaterialSelectListBox.SelectedItem;
            if (selected != null)
            {
                MaterialNameTextBlock.Text = selected.Name;
                QuantityInputBox.QuantityConstraintHint = $"限量{selected.Constraint}";
            }
            else
            {
                MaterialNameTextBlock.Text = "请选择想要申请的物资";
            }
        }
    }

    /// <summary>
    /// 申请的详细信息，暂时只有申请时刻填写的地址
    /// </summary>
    public class ApplicationDetailViewModel
    {
        public string Address { set; get; }
        public GetApplicationDetailResponse OriginalItem { get; }

        public ApplicationDetailViewModel(GetApplicationDetailResponse response)
        {
            OriginalItem = response;

            Address = response.Address;
        }
    }

    /// <summary>
    /// 可申请物资列表
    /// </summary>
    public class ApplicationMaterialListViewModel
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int Constraint { set; get; }

        public AvailableApplicationMaterialResponse.Item OriginItem { get; }

        public ApplicationMaterialListViewModel(AvailableApplicationMaterialResponse.Item item)
        {
            OriginItem = item;

            Name = item.Name;
            Description = item.Description;
            Constraint = item.Constraint;
        }
    }
}
