using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;
using System.IO;
using System.Net;

namespace ModernUIApp1.Handlers.Utils
{
    class FileCache
    {
        private static FileCache cacheFile;
        public static FileCache instance
        {
            get
            {
                if (cacheFile == null)
                {
                    cacheFile = new FileCache();
                }

                return cacheFile;
            }
            private set { cacheFile = value; }
        }

        public void downloadFile(string url, string filePath, Action callback)
        {
            if (!File.Exists(filePath))
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri(url);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                webClient.DownloadFileAsync(uri, filePath);
                webClient.DownloadFileCompleted += delegate
                {
                    FileInfo f = new FileInfo(filePath);
                    if (f.Length == 0)
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                    callback();
                };
            }
            else
            {
                callback();
            }
        }
    }
}
