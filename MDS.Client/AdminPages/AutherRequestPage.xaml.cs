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
            AutherRequestDetialPage autherRequestDetialPage = new AutherRequestDetialPage(flag, 2);
            autherRequestDetialPage.ShowDialog();
        }

        private void Goto_Detail_Waiting(object sender, RoutedEventArgs e)
        {
            AutherRequestConstruct flag = (AutherRequestConstruct)waiting.SelectedItem;
            AutherRequestDetialPage autherRequestDetialPage = new AutherRequestDetialPage(flag, 1);
            autherRequestDetialPage.ShowDialog();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateHistoryList();
            await UpdateWaitingList();
        }

        private async Task UpdateHistoryList()
        {
            AutherRequestListResponse autherRequestListResponse = await NetworkHelper.GetAsync(new AutherRequestListRequest()
            {
                AdminID = UserInfo.Id,
                state = AdminState.FINISH
            });

            foreach (Normal normal in autherRequestListResponse.a_normals)
            {
                AutherRequestList_left.Add(new AutherRequestConstruct()
                {
                    Number = normal.Number,
                    UserID = normal.UserID,
                    Time = normal.Time,
                    State = normal.State == AdminState.FINISH ? "已处理" : "未处理",
                    Type = normal.Type == ReviewType.ADMINAUTHENTICANTION ? "管理员认证" : "配送员认证",
                    ReviewerID = normal.ReviewerID,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }

        private async Task UpdateWaitingList()
        {
            AutherRequestListResponse autherRequestListResponse = await NetworkHelper.GetAsync(new AutherRequestListRequest()
            {
                AdminID = UserInfo.Id,
                state = AdminState.WAIT
            });

            foreach (Normal normal in autherRequestListResponse.a_normals)
            {
                AutherRequestList_right.Add(new AutherRequestConstruct()
                {
                    Number = normal.Number,
                    UserID = normal.UserID,
                    Time = normal.Time,
                    State = normal.State == AdminState.FINISH ? "已处理" : "未处理",
                    Type = normal.Type == ReviewType.ADMINAUTHENTICANTION ? "管理员认证" : "配送员认证",
                    ReviewerID = normal.ReviewerID,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }

        private void Goto_Change(object sender, RoutedEventArgs e)
        {
            Change_Passward change_Passward = new Change_Passward();
            change_Passward.ShowDialog();
        }
    }
}
