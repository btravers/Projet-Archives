using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUIApp1.Data.Registre.Annotation
{
    class AnnotationShortcut
    {
        public int type { get; set; }
        public String text { get; set; }

        public AnnotationShortcut()
        {
            this.type = 3;
            this.text = "";
        }
    }
}
