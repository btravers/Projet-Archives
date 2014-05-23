using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Shortcut
{
    public class Shortcut
    {
        public int id_shortcut { get; private set; }
        AnnotationType type;

        public String default_text {get; private set; }
        int id_icon;

        /* Constructors */
        public Shortcut()
        {
        }

        public Shortcut(int id_shortcut, AnnotationType type, String default_text, int id_icon)
        {
            this.id_shortcut = id_shortcut;
            this.type = type;
            this.default_text = default_text;
            this.id_icon = id_icon;
        }
    }
}
