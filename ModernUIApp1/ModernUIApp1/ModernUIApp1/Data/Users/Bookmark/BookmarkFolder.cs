﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Bookmark
{
    public class BookmarkFolder
    {
        public static BookmarkFolder bookmarkFolderRoot;

        public int id_bookmark_folder { get; protected set; }
        public BookmarkFolder bookmarkFolderParent { get; set; }

        public String label { get; set; }

        /* Dictionnary contains all BookmarkFolders which refers to the BookmarkFolder */
        public Dictionary<int, BookmarkFolder> bookmarkFolders { get; protected set; }
        /* Dictionnary contains all BookmarkFiles which refers to the BookmarkFolder */
        public Dictionary<int, BookmarkFile> bookmarkFiles { get; protected set; }

        /* Constructors */
        public BookmarkFolder()
        {
            bookmarkFolders = new Dictionary<int,BookmarkFolder>();
            bookmarkFiles = new Dictionary<int, BookmarkFile>();
        }

        public BookmarkFolder(int id_bookmark_folder, BookmarkFolder bookmarkFolderParent, String label)
        {
            this.id_bookmark_folder = id_bookmark_folder;
            this.bookmarkFolderParent = bookmarkFolderParent;
            this.label = label;

            bookmarkFolders = new Dictionary<int, BookmarkFolder>();
            bookmarkFiles = new Dictionary<int, BookmarkFile>();
        }


        /* Add a bookmarkFolder to the dictionary */
        public void addBookmarkFolder(BookmarkFolder new_bookmarkFolder)
        {
            if (!bookmarkFolders.ContainsKey(new_bookmarkFolder.id_bookmark_folder))
                bookmarkFolders.Add(new_bookmarkFolder.id_bookmark_folder, new_bookmarkFolder);
        }

        /* Add a bookmarkFile to the dictionary */
        public void addBookmark(BookmarkFile new_bookmarkFile)
        {
            if (!bookmarkFiles.ContainsKey(new_bookmarkFile.id_bookmark_file))
                bookmarkFiles.Add(new_bookmarkFile.id_bookmark_file, new_bookmarkFile);
        }

        /* Remove a bookmark Folder to the dictionary */
        public void rmBookmarkFolder(BookmarkFolder rm_bookmarkFolder)
        {
            bookmarkFolders.Remove(rm_bookmarkFolder.id_bookmark_folder);
        }

        /* Remove a bookmark File to the dictionary */
        public void rmBookmarkFile(BookmarkFile rm_bookmarkFile)
        {
            bookmarkFiles.Remove(rm_bookmarkFile.id_bookmark_file);
        }
    }
}
