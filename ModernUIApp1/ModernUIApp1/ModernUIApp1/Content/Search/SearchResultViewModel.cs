using FirstFloor.ModernUI.Presentation;
using ModernUIApp1.Content.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ModernUIApp1.Content
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SearchResultViewModel : NotifyPropertyChanged
    {
        private List<SearchResultAdapter> results;
        private SearchResultAdapter selectedResult;

        public SearchResultViewModel()
        {
            this.results = new List<SearchResultAdapter>();

            this.results.Add(new SearchResultAdapter("/Resources/fake_sheet.jpg", "test 1"));
            this.results.Add(new SearchResultAdapter("/Resources/fake_sheet.jpg", "test 2"));
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AccentImage")
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

                    MainWindow.window.ContentSource = new Uri("/Pages/ViewTable.xaml", UriKind.Relative);
                }
            }
        }
    }
}
