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
        public OverviewPage()
        {
            InitializeComponent();
        }
        private async Task UpdateProcessingList()
        {
            await Task.Delay(100);
            // TODO
            processingList = new ObservableCollection<DeliveryListViewModel>();
            processingList.Add(new DeliveryListViewModel()
            {
                GUID = "sicnwilx13123x",
                Name = "消毒水(500ml)",
                Quantity = 5,
                Departure = "a仓库",
                Destination = "xx小区",
                StartTime = DateTime.Now
            });
            processingList.Add(new DeliveryListViewModel()
            {
                GUID = "wiucndsin2341s",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                Departure = "yy小区",
                Destination = "b仓库",
                StartTime = DateTime.Now
            });
            //
            userProcessingList.ItemsSource = processingList;
            ListCount c = new ListCount();
            c.ProcessingCount = processingList.Count;
            userProcessingCount.DataContext = c;
        }
        private async Task UpdateWaitingList()
        {
            await Task.Delay(100);
        }
        private async Task UpdateList()
        {
            // TODO 这里会有一个网络请求，假装网络延时
            await UpdateProcessingList();
            await UpdateWaitingList();
        }
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            // TODO 这里会有一个网络请求，假装网络延时
            await UpdateList();
        }
        private void UserProcessingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserWaitingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
    public class ListCount
    {
        public int ProcessingCount { set; get; }
    }
    public class DeliveryListViewModel
    {
        public string GUID { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public string StartID { set; get; }
        public string FinishID { set; get; }
        public string Departure { set; get; }
        public string Destination { set; get; }
        public string State { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime FinishTime { set; get; }
    }
}
