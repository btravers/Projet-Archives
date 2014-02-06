using FirstFloor.ModernUI.Presentation;
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
        // 20 accent colors from Windows Phone 8
        private Color[] accentImages = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        private Color selectedAccentImage;

        public SearchResultViewModel()
        {
 //           SyncThemeAndColor();

 //           AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void SyncThemeAndColor()
        {
            // and make sure accent color is up-to-date
            //this.SelectedAccentImage = AppearanceManager.Current.AccentColor;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AccentImage")
            {
                //SyncThemeAndColor();
            }
        }

        public Color[] AccentImages
        {
            get { return this.accentImages; }
        }


        public Color SelectedAccentImage
        {
            get { return this.selectedAccentImage; }
            set
            {
                if (this.selectedAccentImage != value)
                {
                    this.selectedAccentImage = value;
                    OnPropertyChanged("SelectedAccentImage");

                    //AppearanceManager.Current.AccentColor = value;
                }
            }
        }
    }
}
