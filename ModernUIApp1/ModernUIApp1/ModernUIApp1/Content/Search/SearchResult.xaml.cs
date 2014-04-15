using FirstFloor.ModernUI.Windows.Controls;
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

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for RechercheResultat.xaml
    /// </summary>
    public partial class SearchResult : UserControl
    {
        public static SearchResult window { get; private set; }
        public SearchResultViewModel model { get; private set; }
        
        public SearchResult()
        {
            InitializeComponent();

            SearchResult.window = this;

            this.model = new SearchResultViewModel();

            this.DataContext = this.model;
        }
    }
}
