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

namespace ModernUIApp1.Content.View.Common.Bookmark
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

        public Dictionary<int, BookmarkFolder> currentUnderFolders { get; private set; }
        public Dictionary<int, BookmarkFile> currentUnderFiles { get; private set; }


        public BookmarkResult()
        {
            InitializeComponent();

            currentUnderFiles = new Dictionary<int,BookmarkFile>();
            currentUnderFolders = new Dictionary<int,BookmarkFolder>();

            window = this;
            this.model = new BookmarkResultViewModel();
            this.DataContext = this.model;

            BookmarkHandler bhandler = new BookmarkHandler();
            rootFolder = bhandler.getRootBookmarkFolder();
            currentFolder = rootFolder;

            loadCurrentFolder();
        }

        public void moveToFolder(int idFolder)
        {
            if (currentUnderFolders != null && currentUnderFolders.ContainsKey(idFolder)) 
            {
                previousFolder = currentFolder;
                currentFolder = currentUnderFolders[idFolder];

                loadCurrentFolder();
            }
        }

        public void moveToPreviousFolder()
        {
            if (previousFolder != null)
            {
                currentFolder = previousFolder;
                if (currentFolder != null && currentFolder.bookmarkFolderParent != null)
                {
                    previousFolder = currentFolder.bookmarkFolderParent;
                }

                loadCurrentFolder();
            }
        }

        public void moveToHomeFolder()
        {
            if (rootFolder != null)
            {
                currentFolder = rootFolder;
                previousFolder = rootFolder;

                loadCurrentFolder();
            }
        }

        public void loadCurrentFolder()
        {
            this.model.clearResult();
            currentUnderFolders.Clear();
            currentUnderFiles.Clear();

            int index = 0;

            if (currentFolder != null)
            {
                foreach (BookmarkFolder folder in currentFolder.bookmarkFolders.Values)
                {
                    currentUnderFolders.Add(folder.id_bookmark_folder, folder);
                    this.model.addResult(new BookmarkResultAdapter(BookmarkType.FOLDER, index++, "/Resources/mini_RMM.jpg", folder.label, folder.id_bookmark_folder, "/Pages/ViewTable.xaml"));
                }
                foreach (BookmarkFile file in currentFolder.bookmarkFiles.Values)
                {
                    currentUnderFiles.Add(file.id_bookmark_file, file);
                    this.model.addResult(new BookmarkResultAdapter(BookmarkType.FILE, index++, "/Resources/mini_RMM.jpg", file.label, file.id_bookmark_file, "/Pages/ViewTable.xaml"));
                }
            }
        }
    }
}
