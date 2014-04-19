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

            /* TEST PART */
            BookmarkFolder b1 = new BookmarkFolder(0, BookmarkFolder.bookmarkFolderRoot, "t0");
            BookmarkFolder b2 = new BookmarkFolder(1, BookmarkFolder.bookmarkFolderRoot, "t1");
            BookmarkFolder b3 = new BookmarkFolder(2, BookmarkFolder.bookmarkFolderRoot, "t2");
            BookmarkFolder.bookmarkFolderRoot.addBookmark(new BookmarkFile(0, new Data.Data.Registre.Sheet(), BookmarkFolder.bookmarkFolderRoot, "f0"));
            BookmarkFolder.bookmarkFolderRoot.addBookmark(new BookmarkFile(1, new Data.Data.Registre.Sheet(), BookmarkFolder.bookmarkFolderRoot, "f1"));

            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b1);
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b2);
            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b3);

            b1.addBookmarkFolder(new BookmarkFolder(3, b1, "1.t3"));
            b1.addBookmarkFolder(new BookmarkFolder(4, b1, "1.t4"));
            b1.addBookmarkFolder(new BookmarkFolder(5, b1, "1.t5"));
            b1.addBookmark(new BookmarkFile(2, new Data.Data.Registre.Sheet(), b1, "1.f0"));
            b1.addBookmark(new BookmarkFile(3, new Data.Data.Registre.Sheet(), b1, "1.f1"));

            b2.addBookmarkFolder(new BookmarkFolder(6, b2, "2.t6"));
            b2.addBookmarkFolder(new BookmarkFolder(7, b2, "2.t7"));
            b2.addBookmarkFolder(new BookmarkFolder(8, b2, "2.t8"));
            b2.addBookmark(new BookmarkFile(4, new Data.Data.Registre.Sheet(), b2, "2.f0"));
            b2.addBookmark(new BookmarkFile(5, new Data.Data.Registre.Sheet(), b2, "2.f1"));

            b3.addBookmarkFolder(new BookmarkFolder(9, b3, "3.t9"));
            b3.addBookmarkFolder(new BookmarkFolder(10, b3, "3.t10"));
            b3.addBookmarkFolder(new BookmarkFolder(11, b3, "3.t11"));
            b3.addBookmarkFolder(new BookmarkFolder(12, b3, "3.t12"));
            b3.addBookmark(new BookmarkFile(6, new Data.Data.Registre.Sheet(), b3, "3.f0"));
            b3.addBookmark(new BookmarkFile(7, new Data.Data.Registre.Sheet(), b3, "3.f1"));
            /* END TEST PART */

            return BookmarkFolder.bookmarkFolderRoot;

        }
    }
}
