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

        }
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            // TODO 这里会有一个网络请求，假装网络延时
            await UpdateHistoryList();
        }
        private async Task UpdateHistoryList()
        {
            await Task.Delay(100);
            // TODO
            historyList = new ObservableCollection<DeliveryListViewModel>();
            historyList.Add(new DeliveryListViewModel()
            {
                GUID = "sicnwilx13123x",
                Name = "消毒水(500ml)",
                Quantity = 5,
                Departure = "a仓库",
                Destination = "xx小区",
                StartID = "1234",
                FinishID = "asdf",
                StartTime = DateTime.Now,
                FinishTime = DateTime.Now
            });
            historyList.Add(new DeliveryListViewModel()
            {
                GUID = "wiucndsin2341s",
                Name = "医用酒精(500ml)",
                Quantity = 100,
                Departure = "yy小区",
                Destination = "b仓库",
                StartID = "1234",
                FinishID = "asdf",
                StartTime = DateTime.Now,
                FinishTime = DateTime.Now
            });
            //
            userHistoryList.DataContext = historyList;
        }
    }
}
