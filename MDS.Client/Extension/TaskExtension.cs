using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MDS.Client.Extension
{
    public static class TaskExtension
    {
        public static async Task<T> Progress<T>(this Task<T> task, ProgressBar bar)
        {
            try
            {
                bar.IsIndeterminate = true;
                var result = await task;
                return result;
            }
            finally
            {
                bar.IsIndeterminate = false;
                // TODO 可以做的更柔和一点
                bar.Value = 100;
            }
        }

        private static void SetElementsEnabled(FrameworkElement[] elements, bool enabled)
        {
            foreach (var e in elements)
            {
                e.IsEnabled = enabled;
            }
        }

        public static async Task<T> DisableElements<T>(this Task<T> task, params FrameworkElement[] elements)
        {
            try
            {
                SetElementsEnabled(elements, false);
                var result = await task;
                return result;
            }
            finally
            {
                SetElementsEnabled(elements, true);
            }
        }
    }
}
