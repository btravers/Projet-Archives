using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Users.Bookmark;
using Data.Data.Registre;

namespace Handlers.Handlers
{
    public class BookmarkHandler
    {
        static int dbgTmpInt = -1;

        /* Return the new folder returned by the server */
        public BookmarkFolder newBookmarkFolder(String label, BookmarkFolder parent)
        {
            return new BookmarkFolder(dbgTmpInt--, parent, label);
        }

        /* Return the new folder returned by the server */
        public static BookmarkFile newBookmarkFile(Sheet sheet, String label)
        {
            return new BookmarkFile(dbgTmpInt--, sheet, null, label);
        }

        /* Return the root folder by the server with all data loaded (subfolders and subfiles, recursively) */
        public BookmarkFolder getRootBookmarkFolder()
        {
            // Create the root bookmark folder
            BookmarkFolder.bookmarkFolderRoot = new BookmarkFolder();

            /* TEST PART */
            /*
            BookmarkFolder b1 = new BookmarkFolder(0, BookmarkFolder.bookmarkFolderRoot, "t0");
 //           BookmarkFolder b2 = new BookmarkFolder(1, BookmarkFolder.bookmarkFolderRoot, "t1");
 //           BookmarkFolder b3 = new BookmarkFolder(2, BookmarkFolder.bookmarkFolderRoot, "t2");
            BookmarkFolder.bookmarkFolderRoot.addBookmark(new BookmarkFile(0, new Data.Data.Registre.Sheet(), BookmarkFolder.bookmarkFolderRoot, "f0"));
            BookmarkFolder.bookmarkFolderRoot.addBookmark(new BookmarkFile(1, new Data.Data.Registre.Sheet(), BookmarkFolder.bookmarkFolderRoot, "f1"));

            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b1);
//            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b2);
//            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(b3);
           
            
            BookmarkFolder b2 = new BookmarkFolder(1, b1, "t1");
            BookmarkFolder b3 = new BookmarkFolder(2, b1, "t2");
 
            b1.addBookmarkFolder(b2);
            b1.addBookmarkFolder(b3);

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
             */
            /* END TEST PART */

            return BookmarkFolder.bookmarkFolderRoot;
        }

        /* Update parent */
        public void updateBookmarks()
        {
            //throw new NotImplementedException();
        }

        /* Remove Folder */
        public void removeFolder(BookmarkFolder folder)
        {

        }
        /* Remove File */
        public void removeFile(BookmarkFile file)
        {

        }

        /* Rename Folder (the folder already got the new name (local), update by id) */
        public void renameFolder(BookmarkFolder folder)
        {
        }
        /* Rename File (the file already got the new name (local), update by id) */
        public void renameFile(BookmarkFile file)
        {
        }
    }
}
