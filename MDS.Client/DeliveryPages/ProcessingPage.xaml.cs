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
    /// ProcessingPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessingPage : Page
    {
        private ObservableCollection<DeliveryListViewModel> processingList { set; get; }
        public ProcessingPage()
        {
            InitializeComponent();
            processingList = new ObservableCollection<DeliveryListViewModel>();
            userProcessingList.DataContext = processingList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateProcessingList();
        }
        private async Task UpdateProcessingList()
        {
            processingList = new ObservableCollection<DeliveryListViewModel>();
            userProcessingList.DataContext = processingList;
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
                    StartID = item.StartID,
                    Departure = item.Departure,
                    Destination = item.Destination,
                    StartTime = item.StartTime
                });
            }
        }
        public void ButtonMove_Clicked(object sender, RoutedEventArgs e)
        {
            DeliveryListViewModel cur = (DeliveryListViewModel)userProcessingList.SelectedItem;
            InputDialog dialog = new InputDialog(cur.GUID, DeliveryState.Processing);
            dialog.ShowDialog();
        }
    }
}
