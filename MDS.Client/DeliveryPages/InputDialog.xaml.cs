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
        private string GUID;
        private int type;
        private MutualString info = new MutualString();
        public InputDialog(string guid, int t)
        {
            this.GUID = guid;
            this.type = t;
            if (this.type == 0)
            {
                this.info.output = "请输入发货用户ID以接受订单：";
            }
            else if (this.type == 1)
            {
                this.info.output = "请输入收货用户ID以完成订单：";
            }
            InitializeComponent();
            userOutput.DataContext = info;
        }
        private async void ButtonConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            DeliveryMoveResponse deliveryMoveResponse = await NetworkHelper.GetAsync(new DeliveryMoveRequest()
            {
                DelivererId = UserInfo.Id,
                GUID = this.GUID,
                SecureId = userInput.Password   // todo string这里可能有问题
            });

            deliveryMoveResponse = new DeliveryMoveResponse() { Check = 0 };    // todo 假数据

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
