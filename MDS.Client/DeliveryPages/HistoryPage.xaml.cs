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
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> historyList { set; get; }
        public HistoryPage()
        {
            InitializeComponent();
            historyList = new ObservableCollection<DeliveryListViewModel>();
            userHistoryList.DataContext = historyList;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateHistoryList();
        }
        private async Task UpdateHistoryList()
        {
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Finished
            });
            foreach (Item item in deliveryListResponse.Items)
            {
                historyList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID.ToString(),
                    Name = item.Name,
                    Quantity = item.Quantity,
                    StartID = item.StartID,
                    FinishID = item.FinishID,
                    Departure = item.Departure,
                    Destination = item.Destination,
                    StartTime = item.StartTime,
                    FinishTime = item.FinishTime
                });
            }
        }
    }
}
