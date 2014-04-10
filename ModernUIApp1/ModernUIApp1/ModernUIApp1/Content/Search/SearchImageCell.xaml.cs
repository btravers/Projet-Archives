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

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class SearchImageCell : UserControl
    {
        public SearchImageCell()
        {
            InitializeComponent();

            ItemPanel.MouseLeftButtonUp += ItemPanel_MouseLeftButtonUp;
        }

        void ItemPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {            
            MainWindow.window.ContentSource = new Uri("/Pages/ViewTable.xaml", UriKind.Relative);
        }
    }
}