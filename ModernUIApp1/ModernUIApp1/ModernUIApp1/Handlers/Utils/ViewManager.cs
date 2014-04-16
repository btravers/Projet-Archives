using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;

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
        public Sheet sheet = null;
    }
}
