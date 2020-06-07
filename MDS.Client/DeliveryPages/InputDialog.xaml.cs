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
                this.info.output = "请输入ID以确认申请：";
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
                deliveryMoveResponse = await NetworkHelper.GetAsync(new DeliveryMoveRequest()
                {
                    DelivererId = UserInfo.Id,
                    GUID = this.GUID,
                    SecureId = int.Parse(userInput.Password)
                });
            }

            if (deliveryMoveResponse.Check == 0)
            {
                MessageBox.Show("操作成功");
                this.DialogResult = true;
            }
            else if (deliveryMoveResponse.Check == 1)
            {
                MessageBox.Show("验证ID错误");
            }
            else if (deliveryMoveResponse.Check == 2)
            {
                MessageBox.Show("任务状态有误");
            }
            else
            {
                MessageBox.Show("验证ID错误");
            }
            userInput.Password = "";
        }
        private void ButtonCancel_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("操作取消");
            this.DialogResult = false;
        }
    }
    public class MutualString
    {
        public string output { set; get; }
        public int input { set; get; }
    }
}
