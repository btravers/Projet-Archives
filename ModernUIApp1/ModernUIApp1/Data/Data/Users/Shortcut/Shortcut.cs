using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Shortcut
{
    class Shortcut
    {
        int id_shortcut;
        Type type;

        String default_text;
        int id_icon;

        /* Constructors */
        public Shortcut()
        {
        }

        public Shortcut(int id_shortcut, Type type, String default_text, int id_icon)
        {
            this.id_shortcut = id_shortcut;
            this.type = type;
            this.default_text = default_text;
            this.id_icon = id_icon;
        }
    }
}
