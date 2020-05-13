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
        private ObservableCollection<MaterialListViewModel> MaterialListViewModels { set; get; } = null;

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
            ParentWindow.SetProgressBar(true);

            await Task.Delay(200);
            if (ApplicationViewModel != null)
            {
                // TODO 网络请求ApplicationDetailViewModel

                // TODO 根据ApplicationViewModel和ApplicationDetailViewModel来生成正确的Tab显示
                PART_Stepper.Controller.GotoStep(1);
            }
            else
            {
                // 填写新申请
                // TODO 请求所有可选的物资
                MaterialListViewModels = new ObservableCollection<MaterialListViewModel>()
                {
                    new MaterialListViewModel()
                    {
                        Name = "物资1",
                        Constraint = 10,
                        Description = "物资一的介绍"
                    },
                    new MaterialListViewModel()
                    {
                        Name = "物资2",
                        Constraint = 100,
                        Description = "物资er的介绍"
                    }
                };
                MaterialSelectListBox.ItemsSource = MaterialListViewModels;
                RefreshAddressInfo();
                // TODO 初始化好填写界面
                PART_Stepper.Controller.GotoStep(0);
            }

            RefreshApplicationCardView();

            ParentWindow.SetProgressBar(false);
        }

        private void RefreshApplicationCardView()
        {
            if (PART_Stepper.Controller.PreviousStep != null)
            {
                // 不是第一步
                PART_Card.Visibility = Visibility.Visible;
                CardApplicationId.Text = ApplicationViewModel.GUID;
                CardApplicationName.Text = ApplicationViewModel.Name;
                CardApplicationQuantity.Text = ApplicationViewModel.Quantity.ToString();
                CardApplicationTime.Text = ApplicationViewModel.StartTime.ToString();
            }
            else
            {
                PART_Card.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshAddressInfo()
        {
            UserNameTextBlock.Text = UserInfo.Name;
            UserPhoneTextBlock.Text = UserInfo.PhoneNumber;
            UserAddressTextBlock.Text = UserInfo.HomeAddress;
        }

        private void AddressSettingButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void PART_Stepper_ContinueNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            int idx = 0;
            for (; idx < PART_Stepper.Controller.Steps.Length; ++idx)
            {
                if (args.CurrentStep == PART_Stepper.Controller.Steps[idx])
                {
                    break;
                }
            }
            if (idx == 0)
            {
                // 从申请到等待审核
                // TODO 这里有一个请求要发
                await Task.Delay(100);
                ApplicationViewModel = new ApplicationListViewModel();
                ApplicationDetailViewModel = new ApplicationDetailViewModel();
                RefreshApplicationCardView();
            }
            else if (idx == 3)
            {
                // 确认
            }
        }

        private void PART_Stepper_CancelNavigation(object sender, MaterialDesignExtensions.Controls.StepperNavigationEventArgs args)
        {
            // TODO 这里需要发送撤销请求的包
        }

        private void MaterialSelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaterialListViewModel selected = (MaterialListViewModel)MaterialSelectListBox.SelectedItem;
            if (selected != null)
            {
                MaterialNameTextBlock.Text = selected.Name;
            }
            else
            {
                MaterialNameTextBlock.Text = "请选择想要申请的物资";
            }
        }
    }

    public class ApplicationDetailViewModel
    {

    }

    public class MaterialListViewModel
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int Constraint { set; get; }
    }
}
