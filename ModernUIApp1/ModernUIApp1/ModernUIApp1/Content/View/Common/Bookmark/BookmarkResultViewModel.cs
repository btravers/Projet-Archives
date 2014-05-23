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
                }
            }
        }
    }
}
