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
    /// OverviewPage.xaml 的交互逻辑
    /// </summary>
    public partial class OverviewPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> processingList { set; get; }
        private ObservableCollection<DeliveryListViewModel> waitingList { set; get; }
        private ListCount listCount { set; get; }
        public OverviewPage()
        {
            InitializeComponent();
            processingList = new ObservableCollection<DeliveryListViewModel>();
            waitingList = new ObservableCollection<DeliveryListViewModel>();
            listCount = new ListCount();

            userProcessingList.ItemsSource = processingList;
            userWaitingList.ItemsSource = waitingList;
            userProcessingCount.DataContext = listCount;
            userWaitingCount.DataContext = listCount;
        }
        private async Task UpdateProcessingList()
        {
            processingList = new ObservableCollection<DeliveryListViewModel>();
            userProcessingList.ItemsSource = processingList;
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Processing
            });
            foreach (Item item in deliveryListResponse.Items)
            {
                processingList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID.ToString(),
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Departure = item.Departure,
                    Destination = item.Destination,
                    StartTime = item.StartTime
                });
            }
            listCount.ProcessingCount = processingList.Count;
        }
        private async Task UpdateWaitingList()
        {
            waitingList = new ObservableCollection<DeliveryListViewModel>();
            userWaitingList.ItemsSource = waitingList;
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Waiting
            });
            foreach (Item item in deliveryListResponse.Items)
            {
                waitingList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID.ToString(),
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Departure = item.Departure,
                    Destination = item.Destination,
                    StartTime = item.StartTime
                });
            }
            listCount.WaitingCount = waitingList.Count;
        }
        private async Task UpdateList()
        {
            await UpdateProcessingList();
            await UpdateWaitingList();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateList();
        }
    }
    public class ListCount
    {
        public int ProcessingCount { set; get; }
        public int WaitingCount { set; get; }
    }
    public class DeliveryListViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public int StartID { set; get; }
        public int FinishID { set; get; }
        public string Departure { set; get; }
        public string Destination { set; get; }
        //public string State { set; get; }
        public string StartTime { set; get; }
        public string FinishTime { set; get; }

    }
}
