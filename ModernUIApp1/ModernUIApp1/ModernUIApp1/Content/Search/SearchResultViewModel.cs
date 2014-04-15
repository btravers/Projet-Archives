﻿using FirstFloor.ModernUI.Presentation;
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

                    MainWindow.window.ContentSource = new Uri(this.selectedResult.uri, UriKind.Relative);
                }
            }
        }
    }
}
