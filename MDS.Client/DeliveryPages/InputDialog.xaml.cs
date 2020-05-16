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
using System.Windows.Shapes;

namespace MDS.Client.DeliveryPages
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {
        private int type;
        private MutualString info = new MutualString();
        public InputDialog(int t)
        {
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
        private void ButtonConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            string inputs = userInput.Password;
            bool check = false;

            if (this.type == 0)
            {
                check = CheckInputStartID(inputs);
            }
            else if (this.type == 1)
            {
                check = CheckInputFinishID(inputs);
            }

            if (check == true)
            {
                MessageBox.Show("操作成功");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("密码错误");
                userInput.Password = "";
            }
        }
        private void ButtonCancel_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("操作取消");
            this.DialogResult = false;
        }

        private bool CheckInputStartID(string inputs)
        {
            if (inputs == "123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckInputFinishID(string inputs)
        {
            if (inputs == "456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class MutualString
    {
        public string output { set; get; }
        public string input { set; get; }
    }
}
