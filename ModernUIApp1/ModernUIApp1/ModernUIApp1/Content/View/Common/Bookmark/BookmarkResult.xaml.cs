using Data.Data.Users.Bookmark;
using Handlers.Handlers;
using ModernUIApp1.Content.View.Common.Bookmark;
using ModernUIApp1.Handlers.Utils;
using ModernUIApp1.Pages;
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
using System.Windows.Threading;

namespace ModernUIApp1.Content.View.Common.Bookmark
{
    /// <summary>
    /// Interaction logic for BookmarkResult.xaml
    /// </summary>
    public partial class BookmarkResult : UserControl
    {
        public static BookmarkResult window { get; private set; } // SINGLETON

        public BookmarkHandler bhandler;
        public BookmarkResultViewModel model;

        public BookmarkFolder rootFolder { get; private set; } // Root folder

        public BookmarkFolder previousFolder { get; private set; } // Previous folder view
        public BookmarkFolder currentFolder { get; private set; } // Current folder view

        public Dictionary<int, BookmarkFolder> currentUnderFolders { get; private set; }
        public Dictionary<int, BookmarkFile> currentUnderFiles { get; private set; }

        /* Constructor */
        public BookmarkResult()
        {
            InitializeComponent();

            // TEST LONG CLICK
            resultListBox.AddHandler(UIElement.MouseDownEvent,
//        new MouseButtonEventHandler(ListBox_MouseLongClickDown), true);
            new MouseButtonEventHandler(ListBox_LongClick), true);
            // END TEST LONG CLICK

            currentUnderFiles = new Dictionary<int,BookmarkFile>();
            currentUnderFolders = new Dictionary<int,BookmarkFolder>();

            window = this;
            this.model = new BookmarkResultViewModel();
            this.DataContext = this.model;

            bhandler = new BookmarkHandler();
            rootFolder = bhandler.getRootBookmarkFolder();
            currentFolder = rootFolder;

            loadCurrentFolder();
        }

        // TEST OTHER SOLUTION
        private void ListBox_LongClick(Object sender, MouseButtonEventArgs e)
        {
            WaitFor(TimeSpan.FromMilliseconds(750), DispatcherPriority.SystemIdle);
            ListBox_MouseLongClickDown(sender, e);
        }
        private void WaitFor(TimeSpan time, DispatcherPriority priority)
        {
            DispatcherTimer timer = new DispatcherTimer(priority);
            timer.Tick += new EventHandler(OnDispatched);
            timer.Interval = time;
            DispatcherFrame dispatcherFrame = new DispatcherFrame(false);
            timer.Tag = dispatcherFrame;
            timer.Start();
            Dispatcher.PushFrame(dispatcherFrame);
        }
        private void OnDispatched(object sender, EventArgs args)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Tick -= new EventHandler(OnDispatched);
            timer.Stop();
            DispatcherFrame frame = (DispatcherFrame)timer.Tag;
            frame.Continue = false;
        }
        private void ListBox_MouseLongClickDown(Object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            //MessageBox.Show("MouseDown event on " + listBox.Name);

            DependencyObject dep = (DependencyObject)e.OriginalSource;
            
            while ((dep != null) && !(dep is ListBoxItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            //try
            //{
                BookmarkResultAdapter item = (BookmarkResultAdapter)listBox.ItemContainerGenerator.ItemFromContainer(dep);
                MessageBox.Show("MouseDown event on " + item.text);
            //}
            //catch (Exception) { }
            /*
                if (item != null)
                {
                    if (item.type.Equals(BookmarkType.FILE))
                    {
                        if (BookmarkResult.window != null && BookmarkResult.window.currentUnderFiles != null && BookmarkResult.window.currentUnderFiles.ContainsKey(item.id))
                        {
                            ViewManager.instance.sheet = BookmarkResult.window.currentUnderFiles[item.id].id_sheet;
                        }

                        if (ViewTable.window != null)
                        {
                            ViewTable.window.reload();
                        }

                        MainWindow.window.ContentSource = new Uri(item.uri, UriKind.Relative);
                    }
                    else
                    {
                        window.moveToFolder(item.id);
                    }
                }
             */
        }
        // END TEST

        /* Move the view into the folder given in parameter */
        public void moveToFolder(int idFolder)
        {
            if (currentUnderFolders != null && currentUnderFolders.ContainsKey(idFolder)) 
            {
                previousFolder = currentFolder;
                currentFolder = currentUnderFolders[idFolder];

                loadCurrentFolder();
            }
        }

        /* Move the view into the folder given in parameter (loop on root folder) */
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

        /* Move the view into the root folder */
        public void moveToHomeFolder()
        {
            if (rootFolder != null)
            {
                currentFolder = rootFolder;
                previousFolder = rootFolder;

                loadCurrentFolder();
            }
        }

        /* Add a new folder to the current folder (REQUEST) */
        public void addNewFolder(String label)
        {
            if (currentFolder == null)
            {
                currentFolder = rootFolder;
            }

            BookmarkFolder tmpFolder = bhandler.newBookmarkFolder(label, currentFolder);

            if (tmpFolder != null)
            {
                currentFolder.addBookmarkFolder(tmpFolder);
                loadCurrentFolder();
            }
        }

        /* Update the view related to the current folder */
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
