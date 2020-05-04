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
    /// Interaction logic for ApplyPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private UserApplicationViewModel UserApplicationViewModel { set; get; } = null;

        private ApplicationPage()
        {
            InitializeComponent();
        }

        public ApplicationPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        public ApplicationPage(MainWindow parent, UserApplicationViewModel userApplicationViewModel)
        {
            InitializeComponent();

            ParentWindow = parent;
            UserApplicationViewModel = userApplicationViewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
