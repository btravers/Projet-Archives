using Data.Data.Users.Bookmark;
using Handlers.Handlers;
using ModernUIApp1.Content.View.Common.Bookmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernUIApp1.Content.Bookmark
{
    /// <summary>
    /// Interaction logic for BookmarkResult.xaml
    /// </summary>
    public partial class BookmarkResult : UserControl
    {
        public static BookmarkResult window { get; private set; } // SINGLETON

        public BookmarkResultViewModel model;

        public BookmarkFolder rootFolder { get; private set; } // Root folder
        public BookmarkFolder previousFolder { get; private set; } // Previous folder view
        public BookmarkFolder currentFolder { get; private set; } // Current folder view

        public BookmarkResult()
        {
            InitializeComponent();

            window = this;
            this.model = new BookmarkResultViewModel();
            this.DataContext = this.model;

            BookmarkHandler bhandler = new BookmarkHandler();
            rootFolder = bhandler.getRootBookmarkFolder();
            currentFolder = rootFolder;

            loadCurrentFolder();
        }

        public void loadCurrentFolder()
        {
            int index = 0;

            foreach (BookmarkFolder folder in currentFolder.bookmarkFolders.Values)
            {
                this.model.addResult(new BookmarkResultAdapter(index++, "/Resources/mini_RMM.jpg", folder.label, folder.id_bookmark_folder, "/Pages/ViewTable.xaml"));
            }
            /*
            foreach (BookmarkFile file in currentFolder.bookmarkFiles.Values)
            {
            }
             */
        }
    }
}
