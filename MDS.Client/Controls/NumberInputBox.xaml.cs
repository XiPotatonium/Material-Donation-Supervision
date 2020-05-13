using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDS.Client.Controls
{
    public class NumberInputBoxValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret;
            if (int.TryParse((string)value, out ret))
            {
                if (ret >= 0)
                {
                }
                else
                {
                }
            }
            return ret;
        }
    }

    /// <summary>
    /// Interaction logic for NumberInputBox.xaml
    /// </summary>
    public partial class NumberInputBox : UserControl
    {
        public string QuantityConstraintHint
        {
            set { SetValue(QuantityConstraintHintProperty, value); }
            get { return (string)GetValue(QuantityConstraintHintProperty); }
        }

        public static readonly DependencyProperty QuantityConstraintHintProperty = DependencyProperty.Register(
            nameof(QuantityConstraintHint), typeof(string), typeof(NumberInputBox), new FrameworkPropertyMetadata("这里最好填上数量限制"));

        public int Value { set; get; } = 0;

        public NumberInputBox()
        {
            InitializeComponent();
        }

        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(QuantityTextBox.Text, out int value))
            {
                if (value >= 0)
                {
                    Value = value;
                }
            }
        }

        private void NumberMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > 0 && !ValidationAssist.GetHasError(QuantityTextBox))
            {
                Value--;
                QuantityTextBox.Text = Value.ToString();
            }
        }

        private void QuantityPlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidationAssist.GetHasError(QuantityTextBox))
            {
                Value++;
                QuantityTextBox.Text = Value.ToString();
            }
        }
    }
}
