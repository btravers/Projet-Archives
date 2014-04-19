using FirstFloor.ModernUI.Presentation;
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
using ModernUIApp1.Content.Bookmark;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class BookmarkResultViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<BookmarkResultAdapter> results;
        private BookmarkResultAdapter selectedResult;

        public BookmarkResultViewModel()
        {
            this.results = new ObservableCollection<BookmarkResultAdapter>();

            if (BookmarkResult.window != null)
            {
                BookmarkResult.window.resultListBox.ItemsSource = this.results;
            }
        }

        public void addResult(BookmarkResultAdapter adapter)
        {
            this.results.Add(adapter);
        }

        public void sort()
        {
            ObservableCollection<BookmarkResultAdapter> order = new ObservableCollection<BookmarkResultAdapter>(this.results.OrderBy(o => o.index));
            this.results.Clear();
            foreach(BookmarkResultAdapter o in order)
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

        public BookmarkResultAdapter[] ResultsList
        {
            get { return this.results.ToArray(); }
        }

        public BookmarkResultAdapter SelectedResult
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
                        if (this.selectedResult.type.Equals(BookmarkType.FILE))
                        {

                            /*
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
                        
                             */
                        }
                        else
                        {
                            BookmarkResult.window.moveToFolder(this.selectedResult.id);
                        }
                    }
                }
            }
        }
    }
}
