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
using System.Net;
using Handlers.Utils;
using System.IO;
using ModernUIApp1.Handlers.Utils;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for RechercheTable.xaml
    /// </summary>
    public partial class SearchTable : UserControl
    {
        public static Dictionary<int, PageTable> pagesTable = new Dictionary<int, PageTable>();
        
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

                List<PageTable> result = handler.search((int) yearSlider.Value, location.Text);

                SearchResult.window.model.clearResult();
                SearchTable.pagesTable.Clear();

                if (result.Count == 0)
                {
                    statusText.Text = "Pas de résultat !";                        
                }
                else
                {
                    statusText.Text = "";
                    int index = 0;
                    int downloadCompleted = 0;
                    foreach (PageTable pageTable in result)
                    {
                        int currentIndex = index;
                        index++;

                        SearchTable.pagesTable.Add(pageTable.id_page_table, pageTable);

                        FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, pageTable.url.Replace("/", "-")), pageTable.url,
                        () => {
                                downloadCompleted++;
                                SearchResult.window.model.addResult(new SearchResultAdapter(currentIndex, Directory.GetCurrentDirectory() + "/" + pageTable.url, "Volume " + pageTable.register.volume + " page " + pageTable.page, pageTable.id_page_table, "/Pages/ViewTable.xaml"));

                                if (downloadCompleted == result.Count)
                                {
                                    SearchResult.window.model.sort();
                                }
                            },
                        () =>
                        {

                        }
                        );
                    }
                }
            }
        }
    }
}
