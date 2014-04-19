using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Users.Bookmark;

namespace Handlers.Handlers
{
    public class BookmarkHandler
    {
        public BookmarkFolder getRootBookmarkFolder()
        {
            // Create the root bookmark folder
            BookmarkFolder.bookmarkFolderRoot = new BookmarkFolder();

            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(0, BookmarkFolder.bookmarkFolderRoot, "t0"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(1, BookmarkFolder.bookmarkFolderRoot, "t1"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(2, BookmarkFolder.bookmarkFolderRoot, "t2"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(3, BookmarkFolder.bookmarkFolderRoot, "t3"));


            return BookmarkFolder.bookmarkFolderRoot;

        }
    }
}
