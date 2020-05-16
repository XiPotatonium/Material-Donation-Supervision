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
    /// WaitingPage.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> waitingList { set; get; }
        public WaitingPage()
        {
            InitializeComponent();
        }
        private void ButtonAccept_Clicked(object sender, RoutedEventArgs e)
        {
            InputDialog dialog = new InputDialog(0);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {

            }
        }
        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            // TODO 这里会有一个网络请求，假装网络延时
            await UpdateWaitingList();
        }
        private async Task UpdateWaitingList()
        {
            await Task.Delay(100);
            // TODO
            waitingList = new ObservableCollection<DeliveryListViewModel>();
            waitingList.Add(new DeliveryListViewModel()
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
            waitingList.Add(new DeliveryListViewModel()
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
            userWaitingList.DataContext = waitingList;
        }
    }


}
