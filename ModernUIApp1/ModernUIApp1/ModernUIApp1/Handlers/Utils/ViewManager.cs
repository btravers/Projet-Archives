using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;
using System.Windows.Media.Imaging;

namespace ModernUIApp1.Handlers.Utils
{
    class ViewManager
    {
        private static ViewManager tableViewManager;
        public static ViewManager instance
        {
            get
            {
                if (tableViewManager == null)
                {
                    tableViewManager = new ViewManager();
                }

                return tableViewManager;
            }
            private set { tableViewManager = value; }
        }

        public PageTable pageTable = null;
        public List<PageTable> pageTables = null;
        public int indexPageTables;

        public Sheet sheet = null;

        public Sheet previousSheet = null;

        public Sheet nextSheet = null;
    }
}
