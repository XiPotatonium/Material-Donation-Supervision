using System;
using System.Collections.Generic;
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

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for ApplyPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        private MainWindow ParentWindow { get; } = null;
        private ApplicationListViewModel ApplicationViewModel { set; get; } = null;
        private ApplicationDetailViewModel ApplicationDetailViewModel { set; get; } = null;

        private ApplicationPage()
        {
            InitializeComponent();
        }

        public ApplicationPage(MainWindow parent)
        {
            InitializeComponent();

            ParentWindow = parent;
        }

        public ApplicationPage(MainWindow parent, ApplicationListViewModel userApplicationViewModel)
        {
            InitializeComponent();

            ParentWindow = parent;
            ApplicationViewModel = userApplicationViewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ParentWindow.SetProgressBar(true);
            if (ApplicationViewModel != null)
            {
                // TODO 网络请求
                await Task.Delay(200);
            }
            ParentWindow.SetProgressBar(false);
        }
    }

    public class ApplicationDetailViewModel
    {

    }
}
