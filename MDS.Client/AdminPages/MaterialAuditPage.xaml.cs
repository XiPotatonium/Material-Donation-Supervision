using DTO;
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
    /// MaterialAuditPage.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialAuditPage : Page
    {
        private ObservableCollection<MaterialAuditConstruct> MaterialAuditList_left { set; get; }
        private ObservableCollection<MaterialAuditConstruct> MaterialAuditList_right { set; get; }
        public MaterialAuditPage()
        {
            InitializeComponent();
            MaterialAuditList_left = new ObservableCollection<MaterialAuditConstruct>();
            MaterialAuditList_right = new ObservableCollection<MaterialAuditConstruct>();
            history.DataContext = MaterialAuditList_left;
            waiting.DataContext = MaterialAuditList_right;
        }

        private void Goto_Detail_History(object sender, RoutedEventArgs e)
        {
            MaterialAuditConstruct flag = (MaterialAuditConstruct)history.SelectedItem;
            MaterialAuditDetialPage materialAuditDetialPage = new MaterialAuditDetialPage(flag, 2);
            materialAuditDetialPage.ShowDialog();
        }

        private void Goto_Detail_Waiting(object sender, RoutedEventArgs e)
        {
            MaterialAuditConstruct flag = (MaterialAuditConstruct)waiting.SelectedItem;
            MaterialAuditDetialPage materialAuditDetialPage = new MaterialAuditDetialPage(flag, 1);
            materialAuditDetialPage.ShowDialog();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateHistoryList();
            await UpdateWaitingList();
        }

        private async Task UpdateHistoryList()
        {
            await Task.Delay(100);
            MaterialAuditListResponse materialAuditList = new MaterialAuditListResponse();
            materialAuditList.m_normals = new List<Normal>();
            materialAuditList.m_normals.Add(new Normal()
            {
                Number = "0011",
                UserID = 5,
                Time = DateTime.Now,
                State = AdminState.FINISH,
                ReviewerID = 2,
                Result = AdminResult.FAIL,
                Content = "sssss",
                Remarks = "zzzzz"
            });

            foreach (Normal normal in materialAuditList.m_normals)
            {
                MaterialAuditList_left.Add(new MaterialAuditConstruct()
                {
                    Number = normal.Number,
                    ApplicantID = normal.UserID,
                    Time = normal.Time,
                    State = normal.State,
                    ReviewerID = normal.ReviewerID,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }

        private async Task UpdateWaitingList()
        {
            await Task.Delay(100);
            MaterialAuditListResponse materialAuditList = new MaterialAuditListResponse();
            materialAuditList.m_normals = new List<Normal>();
            materialAuditList.m_normals.Add(new Normal()
            {
                Number = "0011",
                UserID = 6,
                Time = DateTime.Now,
                State = AdminState.FINISH,
                ReviewerID = 2,
                Result = AdminResult.FAIL,
                Content = "sssss",
                Remarks = "zzzzz"
            });

            foreach (Normal normal in materialAuditList.m_normals)
            {
                MaterialAuditList_right.Add(new MaterialAuditConstruct()
                {
                    Number = normal.Number,
                    ApplicantID = normal.UserID,
                    Time = normal.Time,
                    State = normal.State,
                    ReviewerID = normal.ReviewerID,
                    Result = normal.Result,
                    Content = normal.Content,
                    Remarks = normal.Remarks
                });
            }
        }
    }
}
