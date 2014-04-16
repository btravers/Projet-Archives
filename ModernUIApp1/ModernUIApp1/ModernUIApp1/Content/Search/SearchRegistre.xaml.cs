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
using Handlers.Utils;
using Handlers.Handlers;
using Data.Data.Registre;
using ModernUIApp1.Handlers.Utils;
using ModernUIApp1.Content.Search;
using System.IO;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class SearchRegistre : UserControl
    {
        public static Dictionary<int, Sheet> sheets = new Dictionary<int, Sheet>();
        
        public SearchRegistre()
        {
            InitializeComponent();

            // Event manager for edit text year value
            this.yearSlider.ValueChanged += yearSlider_ValueChanged;
        }

        private void yearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.yearValue.Text = e.NewValue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SearchResult.window != null)
            {
                SheetHandler sh = new SheetHandler();

                List<Sheet> result = sh.search((int)yearSlider.Value, location.Text, firstname.Text, lastname.Text, job.Text, regiment.Text);

                SearchResult.window.model.clearResult();
                SearchRegistre.sheets.Clear();

                if (result.Count == 0)
                {
                    //statusText.Text = "Pas de résultat !";
                }
                else
                {
                    //statusText.Text = "";
                    int index = 0;
                    int downloadCompleted = 0;
                    foreach (Sheet sheet in result)
                    {
                        int currentIndex = index;
                        index++;

                        SearchRegistre.sheets.Add(sheet.id_sheet, sheet);

                        FileCache.instance.downloadFile(Connection.ROOT_URL + "/" + ModernUIApp1.Resources.LinkResources.LinkPrintFile.Replace(ModernUIApp1.Resources.LinkResources.Path, sheet.url.Replace("/", "-")), sheet.url,
                        () =>
                        {
                            downloadCompleted++;
                            SearchResult.window.model.addResult(new SearchResultAdapter(currentIndex, Directory.GetCurrentDirectory() + "/" + sheet.url, "", sheet.id_sheet, "/Pages/ViewRegister.xaml"));

                            if (downloadCompleted == result.Count)
                            {
                                SearchResult.window.model.sort();
                            }
                        }
                        );
                    }
                }
            }
        }
    }
}
