using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModernUIApp1.Content.Search
{
    public class SearchResultAdapter
    {
        public string imagePath { get; private set; }
        public string text { get; private set; }

        public SearchResultAdapter(string imagePath, string text)
        {
            this.imagePath = imagePath;
            this.text = text;
        }
    }
}
