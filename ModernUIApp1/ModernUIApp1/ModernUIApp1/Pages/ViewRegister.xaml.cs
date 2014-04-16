﻿using ModernUIApp1.Content.View.Common;
using System;
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

namespace ModernUIApp1.Pages
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class ViewRegister : UserControl
    {
        public static ViewRegister window { get; private set; }
        
        public ViewRegister()
        {
            InitializeComponent();

            ViewRegister.window = this;

            reload();
        }

        public void reload()
        {
            SheetContent sheetContent = SheetContent.window;
            if (sheetContent != null)
            {
                sheetContent.reload();
            }
        }
    }
}
