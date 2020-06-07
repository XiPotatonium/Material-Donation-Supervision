﻿using System;
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

using MDS.Client.DeliveryPages;

namespace MDS.Client.NavigationPages
{
    /// <summary>
    /// Interaction logic for MyDeliveryPage.xaml
    /// </summary>
    public partial class MyDeliveryPage : Page
    {
        private MainWindow parentWindow { get; } = null;
        private OverviewPage myOverviewPage { set; get; } = new OverviewPage();
        private ApplyPage myApplyPage { set; get; } = new ApplyPage();
        //private CheckingPage myCheckingPage { set; get; } = new CheckingPage();
        private ProcessingPage myProcessingPage { set; get; } = new ProcessingPage();
        private WaitingPage myWaitingPage { set; get; } = new WaitingPage();
        private HistoryPage myHistoryPage { set; get; } = new HistoryPage();
        public MyDeliveryPage()
        {
            InitializeComponent();
            OverviewPageFrame.Content = myOverviewPage;
            ApplyPageFrame.Content = myApplyPage;
            //CheckingPageFrame.Content = myCheckingPage;
            ProcessingPageFrame.Content = myProcessingPage;
            WaitingPageFrame.Content = myWaitingPage;
            HistoryPageFrame.Content = myHistoryPage;
        }
        public MyDeliveryPage(MainWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
            OverviewPageFrame.Content = myOverviewPage;
            ApplyPageFrame.Content = myApplyPage;
            //CheckingPageFrame.Content = myCheckingPage;
            ProcessingPageFrame.Content = myProcessingPage;
            WaitingPageFrame.Content = myWaitingPage;
            HistoryPageFrame.Content = myHistoryPage;
        }
    }
}
