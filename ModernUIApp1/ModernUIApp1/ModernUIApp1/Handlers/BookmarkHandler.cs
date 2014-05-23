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

            if (Authenticator.AUTHENTICATOR.connected)
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

            if (Authenticator.AUTHENTICATOR.connected)
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

            if (Authenticator.AUTHENTICATOR.connected)
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

            return BookmarkFolder.bookmarkFolderRoot;
        }

        /* Update parent */
        public void updateBookmarkFile(BookmarkFile file)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkUpdateFileParent.Replace(LinkResources.IdFile, file.id_bookmark_file.ToString()).Replace(LinkResources.IdParentFolder, file.bookmarkFolderParent.id_bookmark_folder.ToString()));
            }
        }

        /* Update parent */
        public void updateBookmarkFolder(BookmarkFolder folder)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkUpdateFolderParent.Replace(LinkResources.IdFolder, folder.id_bookmark_folder.ToString()).Replace(LinkResources.IdParentFolder, folder.bookmarkFolderParent.id_bookmark_folder.ToString()));
            }
        }


        /* Remove Folder */
        public void removeFolder(BookmarkFolder folder)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRemoveFolder.Replace(LinkResources.IdFolder, folder.id_bookmark_folder.ToString()));
            }
        }
        /* Remove File */
        public void removeFile(BookmarkFile file)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRemoveFile.Replace(LinkResources.IdFile, file.id_bookmark_file.ToString()));
            }
        }

        /* Rename Folder (the folder already got the new name (local), update by id) */
        public void renameFolder(BookmarkFolder folder)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRenameFolder.Replace(LinkResources.IdFolder, folder.id_bookmark_folder.ToString()).Replace(LinkResources.NewName, folder.label));
            }
        }
        /* Rename File (the file already got the new name (local), update by id) */
        public void renameFile(BookmarkFile file)
        {
            if (Authenticator.AUTHENTICATOR.connected)
            {
                // Request
                String xmlResponse = Connection.getRequest(LinkResources.LinkBookmarkRenameFile.Replace(LinkResources.IdFile, file.id_bookmark_file.ToString()).Replace(LinkResources.NewName, file.label));
            }
        }
    }
}
