using DTO;
using MaterialDesignThemes.Wpf;
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

namespace MDS.Client.DeliveryPages
{
    /// <summary>
    /// ApplyPage.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> applyList { set; get; }
        public ApplyPage()
        {
            InitializeComponent();
            applyList = new ObservableCollection<DeliveryListViewModel>();
            userApplyList.DataContext = applyList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateApplyList();
        }
        private async Task UpdateApplyList()
        {
            applyList = new ObservableCollection<DeliveryListViewModel>();
            userApplyList.DataContext = applyList;
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Alone
            });
            foreach (Item item in deliveryListResponse.Items)
            {
                applyList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID.ToString(),
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Departure = item.Departure,
                    Destination = item.Destination
                });
            }
        }
        public async void ButtonMove_Clicked(object sender, RoutedEventArgs e)
        {
            DeliveryListViewModel cur = (DeliveryListViewModel)userApplyList.SelectedItem;
            InputDialog dialog = new InputDialog(cur.GUID, DeliveryState.Alone);
            dialog.ShowDialog();
            await UpdateApplyList();
        }
    }
}
