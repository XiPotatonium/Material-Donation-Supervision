using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for DonatePage.xaml
    /// </summary>
    public partial class DonationPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private DonationListViewModel UserDonationViewModel { set; get; } = null;

        private DonationPage()
        {
            InitializeComponent();
        }

        public DonationPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        public DonationPage(MainWindow parent, DonationListViewModel userDonationViewModel)
        {
            InitializeComponent();

            ParentWindow = parent;
            UserDonationViewModel = userDonationViewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
