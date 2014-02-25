using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data.Registre;

namespace Handlers.Cache
{
    /* It contains registers already loaded */
    class Cache
    {
        static Cache CACHE_SINGLETON;

        Dictionary<int, Register> cacheDictionary;

        private Cache()
        {
            cacheDictionary = new Dictionary<int, Register>();
        }

        public Cache getCache()
        {
            if (CACHE_SINGLETON == null)
                CACHE_SINGLETON = new Cache();

            return CACHE_SINGLETON;
        }


    }
}
