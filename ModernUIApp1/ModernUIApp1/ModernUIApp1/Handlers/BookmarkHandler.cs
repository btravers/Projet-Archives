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
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(4, BookmarkFolder.bookmarkFolderRoot, "t4"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(5, BookmarkFolder.bookmarkFolderRoot, "t5"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(6, BookmarkFolder.bookmarkFolderRoot, "t6"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(7, BookmarkFolder.bookmarkFolderRoot, "t7"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(8, BookmarkFolder.bookmarkFolderRoot, "t8"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(9, BookmarkFolder.bookmarkFolderRoot, "t9"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(10, BookmarkFolder.bookmarkFolderRoot, "t10"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(11, BookmarkFolder.bookmarkFolderRoot, "t11"));
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(new BookmarkFolder(12, BookmarkFolder.bookmarkFolderRoot, "t12"));



            return BookmarkFolder.bookmarkFolderRoot;

        }
    }
}
