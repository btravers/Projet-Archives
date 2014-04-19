using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    public class BookmarkResultAdapter
    {
        public BookmarkType type { get; private set; }
        public int index { get; private set; }
        public string imagePath { get; private set; }
        public string text { get; private set; }
        public int id { get; private set; }
        public string uri { get; private set; }

        public BookmarkResultAdapter(BookmarkType type, int index, string imagePath, string text, int id, string uri)
        {
            this.type = type;
            this.imagePath = imagePath;
            this.text = text;
            this.id = id;
            this.uri = uri;
        }
    }

    public enum BookmarkType { FOLDER = 0, FILE } ;
}
