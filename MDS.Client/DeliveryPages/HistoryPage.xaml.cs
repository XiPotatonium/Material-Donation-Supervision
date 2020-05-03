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

namespace MDS.Client.DeliveryPages
{
    /// <summary>
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();

            historyList.Children.Add(CreateNewLine());
        }

        private Grid CreateNewLine()
        {
            Grid newLine = new Grid();
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(120) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(120) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(400) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            newLine.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            for (int i = 0; i < 8; i++)
            {
                TextBlock t = new TextBlock();
                t.Text = "axnwkjx";
                Grid.SetColumn(t, i);
                //
            }
            return newLine;
        }

    }
}
