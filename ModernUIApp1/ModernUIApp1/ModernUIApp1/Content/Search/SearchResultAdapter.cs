﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModernUIApp1.Content.Search
{
    public class SearchResultAdapter
    {
        public int index { get; private set; }
        public string imagePath { get; private set; }
        public string text { get; private set; }
        public int id { get; private set; }
        public string uri { get; private set; }

        public SearchResultAdapter(int index, string imagePath, string text, int id, string uri)
        {
            this.index = index;
            this.imagePath = imagePath;
            this.text = text;
            this.id = id;
            this.uri = uri;
        }
    }
}
