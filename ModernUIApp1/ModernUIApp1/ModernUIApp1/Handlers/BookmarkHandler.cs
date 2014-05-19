using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Users.Bookmark;
using Data.Data.Registre;
using Handlers.Utils;
using ModernUIApp1.Resources;
using ModernUIApp1.Handlers.Utils;
using ModernUIApp1.Handlers.Utils.Parsers;

namespace Handlers.Handlers
{
    public class BookmarkHandler
    {
        static int dbgTmpInt = -1;

        /* Return the new folder returned by the server */
        public BookmarkFolder newBookmarkFolder(String label, BookmarkFolder parent)
        {
            int tmpInt = dbgTmpInt--;

            if (Authenticator.AUTHENTICATOR.user != null)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkNewFolder.Replace(LinkResources.SessionId, Authenticator.AUTHENTICATOR.user.id_session).Replace(LinkResources.Name, label).Replace(LinkResources.IdParentFolder, parent.id_bookmark_folder.ToString()));

                if (xmlResponse != null)
                {
                    Parser parser = new Parser(xmlResponse);
                    tmpInt = parser.parseResultId();
                }
            }

            return new BookmarkFolder(tmpInt, parent, label);
        }

        /* Return the new folder returned by the server */
        public static BookmarkFile newBookmarkFile(Sheet sheet, String label)
        {
            int tmpInt = dbgTmpInt--;

            if (Authenticator.AUTHENTICATOR.user != null)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkNewFile.Replace(LinkResources.SessionId, Authenticator.AUTHENTICATOR.user.id_session).Replace(LinkResources.Name, label).Replace(LinkResources.IdParentFolder, "-1").Replace(LinkResources.IdSheet, sheet.id_sheet.ToString()));

                if (xmlResponse != null)
                {
                    Parser parser = new Parser(xmlResponse);
                    tmpInt = parser.parseResultId();
                }
            }

            return new BookmarkFile(tmpInt, sheet, null, label);
        }

        /* Return the root folder by the server with all data loaded (subfolders and subfiles, recursively) */
        public BookmarkFolder getRootBookmarkFolder()
        {
            // Create the root bookmark folder
            BookmarkFolder.bookmarkFolderRoot = new BookmarkFolder();

            if (Authenticator.AUTHENTICATOR.user != null)
            {
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkGetRoot.Replace(LinkResources.SessionId, Authenticator.AUTHENTICATOR.user.id_session));

                if (xmlResponse != null)
                {
                    Parser parser = new Parser(xmlResponse);

                    Dictionary<int, BookmarkFolder> foldersTmp = parser.parseBookmarkFolders();

                    foreach (BookmarkFolder fold in foldersTmp.Values)
                    {
                        if (fold.bookmarkFolderParent != null && foldersTmp.ContainsKey(fold.bookmarkFolderParent.id_bookmark_folder))
                        {
                            foldersTmp[fold.bookmarkFolderParent.id_bookmark_folder].addBookmarkFolder(fold);
                        }
                        else
                        {
                            BookmarkFolder.bookmarkFolderRoot.addBookmarkFolder(fold);
                            fold.bookmarkFolderParent = BookmarkFolder.bookmarkFolderRoot;
                        }

                        Console.WriteLine("Folder : Parent : " + fold.bookmarkFolderParent.label + " Fils : " + fold.label);
                    }

                    Dictionary<int, BookmarkFile> fileTmp = parser.parseBookmarkFiles(foldersTmp);

                    foreach (BookmarkFile file in fileTmp.Values)
                    {
                        if (file.bookmarkFolderParent != null && foldersTmp.ContainsKey(file.bookmarkFolderParent.id_bookmark_folder))
                        {
                            foldersTmp[file.bookmarkFolderParent.id_bookmark_folder].addBookmark(file);
                        }
                        else
                        {
                            BookmarkFolder.bookmarkFolderRoot.addBookmark(file);
                            file.bookmarkFolderParent = BookmarkFolder.bookmarkFolderRoot;
                        }

                        Console.WriteLine("File : Parent : " + file.bookmarkFolderParent.label + " Fils : " + file.label);
                    }
                }
            }

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
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkUpdateFileParent);
            xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkUpdateFolderParent);


            //throw new NotImplementedException();
        }

        /* Remove Folder */
        public void removeFolder(BookmarkFolder folder)
        {
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRemoveFolder);

        }
        /* Remove File */
        public void removeFile(BookmarkFile file)
        {
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRemoveFile);

        }

        /* Rename Folder (the folder already got the new name (local), update by id) */
        public void renameFolder(BookmarkFolder folder)
        {
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRenameFolder);

        }
        /* Rename File (the file already got the new name (local), update by id) */
        public void renameFile(BookmarkFile file)
        {
            // Request
            String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRenameFile);

        }
    }
}
