using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    public class BookmarkResultAdapter
    {
        private static String LogoFolder = "F1 M 21,30.0001L 55.9999,30.0001L 55.9999,50L 21,50L 21,30.0001 Z M 52,28L 37,28C 38,25 39.4999,24.0001 39.4999,24.0001L 50.75,24C 51.3023,24 52,24.6977 52,25.25L 52,28 Z ";
        private static String LogoFile = "F1 M 22,19L 43.25,19L 54,29.75L 54,57L 22,57L 22,19 Z M 26,23L 26,53L 50,53L 50,35L 38,35L 38,23L 26,23 Z M 42,23.25L 42,31L 49.75,31L 42,23.25 Z ";

        
        private static SolidColorBrush _color;
        public static SolidColorBrush color
        {
            get { return _color; }
            set
            {
                _color = value;
            }
        }

        public static void ColorChanged()
        {
            if (AppearanceManager.Current.ThemeSource == AppearanceManager.DarkThemeSource)
            {
                color = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                color = new SolidColorBrush(Colors.Black);
            }
        }

        public BookmarkType type { get; private set; }
        public int index { get; private set; }
        public string imagePath { get; private set; }
        public string text { get; private set; }
        public int id { get; private set; }
        public string uri { get; private set; }
        public string logoData { get; private set; }

        public BookmarkResultAdapter(BookmarkType type, int index, string imagePath, string text, int id, string uri)
        {
            this.type = type;
            this.imagePath = imagePath;
            this.text = text;
            this.id = id;
            this.uri = uri;
            
            if (type == BookmarkType.FOLDER)
            {
                logoData = LogoFolder;
            }
            else
            {
                logoData = LogoFile;
            }
        }
    }

    public enum BookmarkType { FOLDER = 0, FILE } ;
}
