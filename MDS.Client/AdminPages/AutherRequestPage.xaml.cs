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

namespace MDS.Client.AdminPages
{
    /// <summary>
    /// AutherRequestPage.xaml 的交互逻辑
    /// </summary>
    public partial class AutherRequestPage : Page
    {
        private ObservableCollection<AutherRequestConstruct> AutherRequestList_left { set; get; }
        private ObservableCollection<AutherRequestConstruct> AutherRequestList_right { set; get; }

        public AutherRequestPage()
        {
            InitializeComponent();
            AutherRequestList_left = new ObservableCollection<AutherRequestConstruct>();
            AutherRequestList_right = new ObservableCollection<AutherRequestConstruct>();
            history.DataContext = AutherRequestList_left;
            waiting.DataContext = AutherRequestList_right;
        }

        private void Goto_Detail_History(object sender, RoutedEventArgs e)
        {
            AutherRequestConstruct flag = (AutherRequestConstruct)history.SelectedItem;
            AutherRequestDetialPage autherRequestDetialPage = new AutherRequestDetialPage(flag);
            autherRequestDetialPage.ShowDialog();
        }

        private void Goto_Detail_Waiting(object sender, RoutedEventArgs e)
        {
            AutherRequestConstruct flag = (AutherRequestConstruct)waiting.SelectedItem;
            AutherRequestDetialPage autherRequestDetialPage = new AutherRequestDetialPage(flag);
            autherRequestDetialPage.ShowDialog();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateHistoryList();
            await UpdateWaitingList();
        }

        private async Task UpdateHistoryList()
        {
            await Task.Delay(100);
            AutherRequestList autherRequestList = new AutherRequestList();
            autherRequestList.a_normals = new List<Normal>();
            autherRequestList.a_normals.Add(new Normal()
            {
                Number = "0011",
                User = "一号",
                Time = DateTime.Now,
                State = AdminState.FINISH,
                Reviewer = "me",
                Result = AdminResult.FAIL,
                Content = "sssss",
                Remarks = "zzzzz"
            });

            foreach (Normal normal in autherRequestList.a_normals)
            {
                AutherRequestList_left.Add(new AutherRequestConstruct()
                {
                    Number = normal.Number,
                    User = normal.User,
                    Time = normal.Time,
                    State = normal.State,
                    Reviewer = normal.Reviewer,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }

        private async Task UpdateWaitingList()
        {
            await Task.Delay(100);
            AutherRequestList autherRequestList = new AutherRequestList();
            autherRequestList.a_normals = new List<Normal>();
            autherRequestList.a_normals.Add(new Normal()
            {
                Number = "0011",
                User = "一号",
                Time = DateTime.Now,
                State = AdminState.FINISH,
                Reviewer = "me",
                Result = AdminResult.FAIL,
                Content = "sssss",
                Remarks = "zzzzz"
            });

            foreach (Normal normal in autherRequestList.a_normals)
            {
                AutherRequestList_right.Add(new AutherRequestConstruct()
                {
                    Number = normal.Number,
                    User = normal.User,
                    Time = normal.Time,
                    State = normal.State,
                    Reviewer = normal.Reviewer,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }
    }
}
