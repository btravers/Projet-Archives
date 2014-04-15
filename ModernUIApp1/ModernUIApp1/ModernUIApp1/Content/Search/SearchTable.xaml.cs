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
using FirstFloor.ModernUI.Presentation;
using Handlers.Handlers;
using ModernUIApp1.Content.Search;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for RechercheTable.xaml
    /// </summary>
    public partial class SearchTable : UserControl
    {
        public SearchTable()
        {
            InitializeComponent();

            // Event manager for edit text year value
            this.yearSlider.ValueChanged += yearSlider_ValueChanged;
        }

        private void yearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.yearValue.Text = e.NewValue.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SearchResult.window != null)
            {
                TableHandler handler = new TableHandler();

                List<PageTable> result = handler.searchTable((int) yearSlider.Value, location.Text);

                SearchResult.window.model.clearResult();
                if (result.Count == 0)
                {
                    statusText.Text = "Pas de résultat !";                        
                }
                else
                {
                    statusText.Text = ""; 
                    foreach (PageTable pageTable in result)
                    {
                        SearchResult.window.model.addResult(new SearchResultAdapter("/Resources/fake_sheet.jpg", "Table " + pageTable.page + " volume " + pageTable.register.volume, "/Pages/ViewTable.xaml"));
                    }
                }
            }
        }
    }
}
