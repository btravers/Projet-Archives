﻿using Data.Data.Users.Bookmark;
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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using ModernUIApp1.Pages.Popups;
using Data.Data.Registre;
using System.IO;

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

#region LongClick variables (useless)
        /*
        // TIMER for long click
        private DispatcherTimer timer;
        private Object sender;
        private MouseButtonEventArgs e;
         */
        // End long click
#endregion

        /* Constructor */
        public BookmarkResult()
        {
            InitializeComponent();

//            resultListBox.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(ListBox_MouseClickDown), true);
            // DRAG N DROP
            resultListBox.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(ListBox_ShortClick), true);
            resultListBox.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(ListBox_MouseMove), true);
            // END DRAG N DROP

            currentUnderFiles = new Dictionary<int,BookmarkFile>();
            currentUnderFolders = new Dictionary<int,BookmarkFolder>();

            window = this;
            this.model = new BookmarkResultViewModel();
            this.DataContext = this.model;

            bhandler = new BookmarkHandler();
            rootFolder = bhandler.getRootBookmarkFolder();
            currentFolder = rootFolder;

            SyncColor();
        }

        #region DragNDrop
        /* Handler called when the source is moving */
        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBox listBox = (ListBox)sender;
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListBoxItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                // Allow remove
                remove_on();

                BookmarkResultAdapter item = (BookmarkResultAdapter)listBox.ItemContainerGenerator.ItemFromContainer(dep);

                DragDrop.DoDragDrop(dep,
                                     item.type + "/" + item.id.ToString(),
                                     DragDropEffects.Copy);
            }
            else
            {
                remove_off();
            }
        }

        /* Handler called when an item is dropped on the source */
        private void item_Drop(object sender, DragEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListBoxItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            BookmarkResultAdapter item = (BookmarkResultAdapter)listBox.ItemContainerGenerator.ItemFromContainer(dep);

            if (item.type.Equals(BookmarkType.FOLDER))
            {
                // If the DataObject contains string data, extract it.
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    //MessageBox.Show("Drop : " + dataString + " to : " + item.text);

                    BookmarkFolder destination = currentUnderFolders[item.id];

                    if (dataString.Split('/')[0].Equals(BookmarkType.FOLDER.ToString()))
                    { // It's a Folder, update it
                        // Update File, New ParentFolder, Current Folder
                        BookmarkFolder target = currentUnderFolders[int.Parse(dataString.Split('/')[1])];

                        // Check destination != target
                        if (target != destination)
                        {
                            target.bookmarkFolderParent = destination;
                            destination.addBookmarkFolder(target);
                            currentFolder.bookmarkFolders.Remove(target.id_bookmark_folder);
                            // Update in server
                            bhandler.updateBookmarkFolder(target);
                        }
                        else
                        { // If it is it's a rename
                            this.renameFolder(currentFolder.bookmarkFolders[item.id]);
                        }
                    }
                    else
                    { // It's a File, update it
                        // Update File, New ParentFolder, Current Folder
                        BookmarkFile target = currentUnderFiles[int.Parse(dataString.Split('/')[1])];

                        target.bookmarkFolderParent = destination;
                        destination.addBookmark(target);
                        currentFolder.bookmarkFiles.Remove(target.id_bookmark_file);
                        // Update in server
                        bhandler.updateBookmarkFile(target);
                    }

                    // Reload the current folder
                    loadCurrentFolder();
                }
            }
            else // FILE
            {
                BookmarkFile destination = currentUnderFiles[item.id];
                // If the DataObject contains string data, extract it.
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    if (dataString.Split('/')[0].Equals(BookmarkType.FILE.ToString()))
                    {
                        BookmarkFile target = currentUnderFiles[int.Parse(dataString.Split('/')[1])];

                        if (destination == target)
                        {
                            this.renameFile(currentFolder.bookmarkFiles[item.id]);
                        }
                    }
                }
            }
        }

        /* Called if it's a short click */
        private void ListBox_ShortClick(Object sender, MouseButtonEventArgs e)
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

            BookmarkResultAdapter item = (BookmarkResultAdapter)listBox.ItemContainerGenerator.ItemFromContainer(dep);
            if (item != null)
            {
                if (item.type.Equals(BookmarkType.FILE))
                {
                    if (BookmarkResult.window != null && BookmarkResult.window.currentUnderFiles != null && BookmarkResult.window.currentUnderFiles.ContainsKey(item.id))
                    {                     
                        ViewManager.instance.sheet = BookmarkResult.window.currentUnderFiles[item.id].id_sheet;
                    }

                    if (ViewRegister.window != null)
                    {
                        ViewRegister.window.reload();
                    }

                    MainWindow.window.ContentSource = new Uri(item.uri, UriKind.Relative);
                }
                else
                {
                    window.moveToFolder(item.id);
                }
            }
        }

        /* Allow remove */
        private void remove_on()
        {
            BookmarkToolbar.window.remove.EllipseStrokeThickness = 15;
        }

        /* Disable remove */
        private void remove_off()
        {
            BookmarkToolbar.window.remove.EllipseStrokeThickness = 0;
        }

        #endregion

        #region LongClick operations & handlers (useless)
        // Long click & short click
        /* Handler Cick left down on an item */
        /*
        private void ListBox_MouseClickDown(Object sender, MouseButtonEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(875); // All 875ms,
            timer.Tick += TickHandler; // Handler "TickHandler" is called
            timer.Start();
            this.sender = sender;
            this.e = e;
        }
         */
        // Timer tick
        /*
        private void TickHandler(Object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();   // Stop the timer
                timer = null; // Delete it
               
                if (this.e != null && this.sender != null)
                {
                    // ListBox_LongClick(this.sender, this.e); // Determine that it's a long click
                    // Allow drop because it's a long click
                    // resultListBox.AllowDrop = true;
                }
                 
           }
        }
         */
        /* Handler Cick left up on an item */
        /*
        private void ListBox_MouseClickUp(Object sender, MouseButtonEventArgs e)
        {
            // resultListBox.AllowDrop = false;
            
            if (timer != null) // If a timer is in running
            {
                timer.Stop(); // Stop it to avoid the long click
            }
             

             ListBox_ShortClick(sender, e); // So it's a short click because the timer didn't had the time to run the tick handler
        }
        */

        /* Called if it's a long click */
        /*
        private void ListBox_LongClick(Object sender, MouseButtonEventArgs e)
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
            MessageBox.Show("LONG CLICK on " + item.text);
            //}
            //catch (Exception) { }

        }
         */ 
        // End long click & short click
#endregion useless

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

        /* Update the view related to the current folder */
        public void loadCurrentFolder()
        {
            remove_off();

            this.model.clearResult();
            currentUnderFolders.Clear();
            currentUnderFiles.Clear();

            int index = 0;

            if (currentFolder != null)
            {
                foreach (BookmarkFolder folder in currentFolder.bookmarkFolders.Values)
                {
                    currentUnderFolders.Add(folder.id_bookmark_folder, folder);
                    // TODO update URI and ImagePath
                    this.model.addResult(new BookmarkResultAdapter(BookmarkType.FOLDER, index++, null, folder.label, folder.id_bookmark_folder, "/Pages/ViewTable.xaml"));
                }
                foreach (BookmarkFile file in currentFolder.bookmarkFiles.Values)
                {
                    currentUnderFiles.Add(file.id_bookmark_file, file);
                    // TODO update URI and ImagePath
                    
                    if (file.id_sheet == null)
                    {
                        this.model.addResult(new BookmarkResultAdapter(BookmarkType.FILE, index++, "/Resources/mini_RMM.jpg", file.label, file.id_bookmark_file, "/Pages/ViewRegister.xaml"));
                    }
                    else
                    {
                        this.model.addResult(new BookmarkResultAdapter(BookmarkType.FILE, index++, Directory.GetCurrentDirectory() + "/" + file.id_sheet.url, file.label, file.id_bookmark_file, "/Pages/ViewRegister.xaml"));
                    }
                }
            }
        }

        #region Operations which call the bhhandler

        /* Add a new file with a sheet */
        public static void addNewFile(Sheet sheet)
        {
            if (sheet != null)
            {
                BookmarkFile tmp = BookmarkHandler.newBookmarkFile(sheet, sheet.id_sheet.ToString());

                if (window != null)
                {
                    window.rootFolder.addBookmark(tmp);
                    tmp.bookmarkFolderParent = window.rootFolder;

                    window.loadCurrentFolder();
                }
            }
        }
        /* Add a new file with a page table pageTable (not supported yet) */
        public static void addNewFile(PageTable pageTable)
        {
            throw new NotImplementedException();
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

        /* Remove operations */
        /* Remove folder */
        public void removeFolder(BookmarkFolder target)
        {
            Popup pop = new Popup("Supprimer un dossier", "Etes vous sur de vouloir supprimer ce dossier ?", false);

            if (pop.show())
            {
                bhandler.removeFolder(target);
                currentFolder.rmBookmarkFolder(target);
                loadCurrentFolder();
            }
        }
        /* Remove file */
        public void removeFile(BookmarkFile target)
        {
            Popup pop = new Popup("Supprimer un fichier", "Etes vous sur de vouloir supprimer ce fichier ?", false);

            if (pop.show())
            {
                bhandler.removeFile(target);
                currentFolder.rmBookmarkFile(target);
                loadCurrentFolder();
            }
        }

        /* Rename operations */
        /* Rename folder */
        public void renameFolder(BookmarkFolder target)
        {
            Popup pop = new Popup("Renommer un fichier", "Renommer en :", true);

            if (pop.show() && pop.result != null)
            {
                target.label = pop.result;
                bhandler.renameFolder(target);
                loadCurrentFolder();
            }
        }
        /* Rename folder */
        public void renameFile(BookmarkFile target)
        {
            Popup pop = new Popup("Renommer un fichier", "Renommer en :", true);

            if (pop.show() && pop.result != null)
            {
                target.label = pop.result;
                bhandler.renameFile(target);
                loadCurrentFolder();
            }
        }
        #endregion

        /* Sync color if changed */
        public static void SyncColor()
        {
            BookmarkResultAdapter.ColorChanged();
            if (window != null)
                window.loadCurrentFolder();
        }

        /* Function called when disconnected */
        public void disconnect()
        {
            rootFolder = new BookmarkFolder();
            previousFolder = null;
            currentFolder = rootFolder;
            currentUnderFiles.Clear();
            currentUnderFolders.Clear();
            model.clearResult();
        }

        /* Function called when connected */
        public void connect()
        {
            if (bhandler == null)
                bhandler = new BookmarkHandler();

            rootFolder = bhandler.getRootBookmarkFolder();
            currentFolder = rootFolder;

            this.loadCurrentFolder();
        }
    }

}
