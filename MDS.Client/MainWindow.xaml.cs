using System;
using System.Collections.Generic;
using System.Linq;
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

using MDS.Client.NavigationPages;

namespace MDS.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO 要根据用户类型调整tab，目前测试状态，禁用这段代码
            // RefreshTabDisplay();
            PART_Frame.Content = new MyMainPage(this);
        }

        private void RefreshTabDisplay()
        {
            ManagePageTab.Visibility = UserInfo.UserType == UserType.ADMIN ? Visibility.Visible : Visibility.Collapsed;
            MyDeliveryPageTab.Visibility = UserInfo.UserType == UserType.DELIVERER ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] == ApplicationPageTab)
            {
                PART_Frame.Content = new ApplicationPage(this);
            }
            else if (e.AddedItems[0] == DonationPageTab)
            {
                PART_Frame.Content = new DonationPage();
            }
            else if (e.AddedItems[0] == ManagePageTab)
            {
                PART_Frame.Content = new ManagePage();
            }
            else if (e.AddedItems[0] == MyDeliveryPageTab)
            {
                PART_Frame.Content = new MyDeliveryPage();
            }
            else
            {
                PART_Frame.Content = new MyMainPage(this);
            }
        }

        public void NavigateToApplicationPageAndDisplay(UserApplicationViewModel userApplicationViewModel)
        {
            MainTabControl.SelectedItem = ApplicationPageTab;
            PART_Frame.Content = new ApplicationPage(this, userApplicationViewModel);
        }
    }
}
