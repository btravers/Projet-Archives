using Handlers.Handlers;
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
using Data.Data.Registre;

namespace ModernUIApp1.Pages
{
    /// <summary>
    /// Interaction logic for SplitPage1.xaml
    /// </summary>
    public partial class Recherche : UserControl
    {
        public Recherche()
        {
            InitializeComponent();

            SheetHandler sh = new SheetHandler();
            List<Sheet> l = sh.search(1878, "saint-malo", "", "bouteau", "", "");
            foreach (Sheet s in l)
            {
                Console.WriteLine(s.ToString());
            }
        }
    }
}
