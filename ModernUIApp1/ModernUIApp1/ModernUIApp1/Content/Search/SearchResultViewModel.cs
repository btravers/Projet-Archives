using FirstFloor.ModernUI.Presentation;
using ModernUIApp1.Content.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;
using ModernUIApp1.Pages;
using ModernUIApp1.Handlers.Utils;
using Data.Data.Registre;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SearchResultViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<SearchResultAdapter> results;
        private SearchResultAdapter selectedResult;

        public SearchResultViewModel()
        {
            this.results = new ObservableCollection<SearchResultAdapter>();

            if (SearchResult.window != null)
            {
                SearchResult.window.resultListBox.ItemsSource = this.results;
            }
        }

        public void addResult(SearchResultAdapter adapter)
        {
            this.results.Add(adapter);
        }

        public void sort()
        {
            ObservableCollection<SearchResultAdapter> order = new ObservableCollection<SearchResultAdapter>(this.results.OrderBy(o => o.index));
            this.results.Clear();
            foreach(SearchResultAdapter o in order)
            {
                this.results.Add(o);
            }
        }

        public void clearResult()
        {
            this.results.Clear();
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ResultsList")
            {
                //SyncThemeAndColor();
            }
        }

        public SearchResultAdapter[] ResultsList
        {
            get { return this.results.ToArray(); }
        }

        public SearchResultAdapter SelectedResult
        {
            get { return this.selectedResult; }
            set
            {
                if (this.selectedResult != value)
                {
                    this.selectedResult = value;
                    OnPropertyChanged("SelectedResult");

                    if (this.selectedResult != null)
                    {
                        if (SearchTable.pagesTable != null && SearchTable.pagesTable.ContainsKey(this.selectedResult.id))
                        {
                            ViewManager.instance.pageTable = SearchTable.pagesTable[this.selectedResult.id];

                            List<PageTable> list = new List<PageTable>();
                            int index = 0;
                            foreach (KeyValuePair<int, PageTable> pair in SearchTable.pagesTable.ToList().OrderBy(o => o.Key))
                            {
                                list.Add(pair.Value);
                                if (this.selectedResult.id == pair.Key)
                                {
                                    ViewManager.instance.indexPageTables = index;
                                }

                                index++;
                            }
                            ViewManager.instance.pageTables = list;
                        }

                        if (ViewTable.window != null)
                        {
                            ViewTable.window.reload();
                        }

                        if (SearchRegistre.sheets != null && SearchRegistre.sheets.ContainsKey(this.selectedResult.id))
                        {
                            ViewManager.instance.sheet = SearchRegistre.sheets[this.selectedResult.id];
                        }

                        if (ViewRegister.window != null)
                        {
                            ViewRegister.window.reload();
                        }

                        MainWindow.window.ContentSource = new Uri(this.selectedResult.uri, UriKind.Relative);
                    }
                }
            }
        }
    }
}
