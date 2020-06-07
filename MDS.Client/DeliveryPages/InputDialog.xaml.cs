using DTO;
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
using System.Windows.Shapes;

namespace MDS.Client.DeliveryPages
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {
        private int GUID;
        private DeliveryState type;
        private MutualString info = new MutualString();
        public InputDialog(string guid, DeliveryState t)
        {
            this.GUID = int.Parse(guid);
            this.type = t;
            if (this.type == DeliveryState.Waiting)
            {
                this.info.output = "请输入发货用户ID以接受订单：";
            }
            else if (this.type == DeliveryState.Processing)
            {
                this.info.output = "请输入收货用户ID以完成订单：";
            }
            else if (this.type == DeliveryState.Alone)
            {
                this.info.output = "确认申请：";
            }
            else
            {

            }
            InitializeComponent();
            userOutput.DataContext = info;
        }
        private async void ButtonConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            DeliveryMoveResponse deliveryMoveResponse;
            if (this.type == DeliveryState.Alone)
            {
                deliveryMoveResponse = await NetworkHelper.GetAsync(new DeliveryApplyRequest()
                {
                    TransactionId = this.GUID,
                    DelivermanId = UserInfo.Id,
                });
            }
            else
            {
                int sid = 0;
                if (!int.TryParse(userInput.Password, out sid))
                {
                    MainWindow.SetSnackBarContentAndPopup("输入格式有误");
                }
                deliveryMoveResponse = await NetworkHelper.GetAsync(new DeliveryMoveRequest()
                {
                    DelivererId = UserInfo.Id,
                    GUID = this.GUID,

                    SecureId = sid
                });
            }

            if (deliveryMoveResponse.Check == 0)
            {
                MainWindow.SetSnackBarContentAndPopup("操作成功");
                this.DialogResult = true;
            }
            else if (deliveryMoveResponse.Check == 1)
            {
                MainWindow.SetSnackBarContentAndPopup("验证ID错误");
            }
            else if (deliveryMoveResponse.Check == 2)
            {
                MainWindow.SetSnackBarContentAndPopup("任务状态有误");
            }
            else if (deliveryMoveResponse.Check == 3)
            {
                MainWindow.SetSnackBarContentAndPopup("未知的错误");
            }
            userInput.Password = "";
        }
        private void ButtonCancel_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow.SetSnackBarContentAndPopup("操作取消");
            this.DialogResult = false;
        }
    }
    public class MutualString
    {
        public string output { set; get; }
        public int input { set; get; }
    }
}
