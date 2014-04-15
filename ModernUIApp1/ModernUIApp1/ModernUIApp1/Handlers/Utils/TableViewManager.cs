using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;

namespace ModernUIApp1.Handlers.Utils
{
    class TableViewManager
    {
        private static TableViewManager tableViewManager;
        public static TableViewManager instance
        {
            get
            {
                if (tableViewManager == null)
                {
                    tableViewManager = new TableViewManager();
                }

                return tableViewManager;
            }
            private set { tableViewManager = value; }
        }

        public PageTable pageTable = null;
    }
}
