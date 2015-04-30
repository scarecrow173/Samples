﻿using System;
using System.Collections.Generic;
using System.Linq;
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

using FirstFloor.ModernUI.Presentation;


namespace TheShawshankRedemption.Pages
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : UserControl
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string iHistory in Classes.History.GetInstance.HistoryQueue.ToList())
            {
                Link HistoryLink = new Link();
                HistoryLink.DisplayName = iHistory;
                HistorySpace.Links.Add(HistoryLink);
            }
        }
    }
}
