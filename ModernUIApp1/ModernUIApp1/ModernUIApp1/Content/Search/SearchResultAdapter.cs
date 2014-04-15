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
        public int id { get; private set; }
        public string uri { get; private set; }

        public SearchResultAdapter(string imagePath, string text, int id, string uri)
        {
            this.imagePath = imagePath;
            this.text = text;
            this.id = id;
            this.uri = uri;
        }
    }
}
