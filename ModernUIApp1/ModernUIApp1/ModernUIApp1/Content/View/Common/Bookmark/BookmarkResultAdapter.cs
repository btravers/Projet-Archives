using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    public class BookmarkResultAdapter
    {
        public int index { get; private set; }
        public string imagePath { get; private set; }
        public string text { get; private set; }
        public int id { get; private set; }
        public string uri { get; private set; }

        public BookmarkResultAdapter(int index, string imagePath, string text, int id, string uri)
        {
            this.imagePath = imagePath;
            this.text = text;
            this.id = id;
            this.uri = uri;
        }
    }
}
