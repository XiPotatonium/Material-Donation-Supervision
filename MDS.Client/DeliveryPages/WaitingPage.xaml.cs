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
    /// WaitingPage.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> waitingList { set; get; }
        public WaitingPage()
        {
            InitializeComponent();
            waitingList = new ObservableCollection<DeliveryListViewModel>();
            userWaitingList.DataContext = waitingList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateWaitingList();
        }
        private async Task UpdateWaitingList()
        {/*
            DeliveryListResponse deliveryListResponse = await NetworkHelper.GetAsync(new DeliveryListRequest()
            {
                DelivererId = UserInfo.Id,
                State = DeliveryState.Waiting
            });
            *////todo 假数据
            await Task.Delay(100);
            DeliveryListResponse deliveryListResponse = new DeliveryListResponse();
            deliveryListResponse.Items = new List<Item>();
            deliveryListResponse.Items.Add(new Item()
            {
                GUID = "qh1i2hisqh1is",
                Name = "水",
                Quantity = 100,
                StartID = 12345,
                FinishID = 98765,
                Departure = "a小区",
                Destination = "0仓库",
                StartTime = DateTime.Now,
                FinishTime = DateTime.Now
            });
            /////////////////
            foreach (Item item in deliveryListResponse.Items)
            {
                waitingList.Add(new DeliveryListViewModel()
                {
                    GUID = item.GUID,
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
        public void ButtonMove_Clicked(object sender, RoutedEventArgs e)
        {
            DeliveryListViewModel cur = (DeliveryListViewModel)userWaitingList.SelectedItem;
            InputDialog dialog = new InputDialog(cur.GUID, 0);
            dialog.ShowDialog();
        }
    }


}
