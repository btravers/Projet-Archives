using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Shortcut
{
    class Type
    {
        int id_type;
        String label;

        /* Constructors */
        public Type()
        {
        }

        public Type(int id_type, String label)
        {
            this.id_type = id_type;
            this.label = label;
        }
    }
}
