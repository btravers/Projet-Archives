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
using Data.Data.Registre;
using ModernUIApp1.Handlers.Utils;

namespace ModernUIApp1.Pages
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class ViewTable : UserControl
    {
        public static ViewTable window { get; private set; }
        
        public ViewTable()
        {
            InitializeComponent();

            ViewTable.window = this;

            reload();
        }

        public void reload()
        {
            PageTable pageTable = TableViewManager.instance.pageTable;
            if (pageTable != null)
            {
                Console.WriteLine(pageTable.id_page_table);
            }
        }
    }
}
