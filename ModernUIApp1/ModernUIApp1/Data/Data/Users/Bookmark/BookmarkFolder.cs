using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Bookmark
{
    class BookmarkFolder
    {
        public static BookmarkFolder bookmarkFolderRoot;

        public int id_bookmark_folder;
        BookmarkFolder bookmarkFolderParent;

        String label;

        /* Dictionnary contains all BookmarkFolders which refers to the BookmarkFolder */
        Dictionary<int, BookmarkFolder> bookmarkFolders;
        /* Dictionnary contains all BookmarkFiles which refers to the BookmarkFolder */
        Dictionary<int, BookmarkFile> bookmarkFiles;

        /* Constructors */
        public BookmarkFolder()
        {
        }

        public BookmarkFolder(int id_bookmark_folder, BookmarkFolder bookmarkFolderParent, String label)
        {
            this.id_bookmark_folder = id_bookmark_folder;
            this.bookmarkFolderParent = bookmarkFolderParent;
            this.label = label;
        }


        /* Add a bookmarkFolder to the dictionary */
        public void addBookmarkFolder(BookmarkFolder new_bookmarkFolder)
        {
            bookmarkFolders.Add(new_bookmarkFolder.id_bookmark_folder, new_bookmarkFolder);
        }

        /* Add a bookmarkFile to the dictionary */
        public void addBookmark(BookmarkFile new_bookmarkFile)
        {
            bookmarkFiles.Add(new_bookmarkFile.id_bookmark_file, new_bookmarkFile);
        }
    }
}
