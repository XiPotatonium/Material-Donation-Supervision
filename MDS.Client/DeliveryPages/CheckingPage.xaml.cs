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
namespace MDS.Client.DeliveryPages
{
    /// <summary>
    /// CheckingPage.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> checkingList { set; get; }
        public CheckingPage()
        {
            InitializeComponent();
            checkingList = new ObservableCollection<DeliveryListViewModel>();
            userCheckingList.DataContext = checkingList;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateCheckingList();
        }
        private async Task UpdateCheckingList()
        {
            checkingList = new ObservableCollection<DeliveryListViewModel>();
            userCheckingList.DataContext = checkingList;
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Checking
            });
            foreach (Item item in deliveryListResponse.Items)
            {
                checkingList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID.ToString(),
                    Name = item.Name,
                    Quantity = item.Quantity,
                    StartID = item.StartID,
                    FinishID = item.FinishID,
                    Departure = item.Departure,
                    Destination = item.Destination
                });
            }
        }
    }
}
