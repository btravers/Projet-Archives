using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Users.Shortcut
{
    public class AnnotationType
    {
        public static readonly Dictionary<int, AnnotationType> types = new Dictionary<int, AnnotationType>();

        public int id_type { get; private set; }
        public String label { get; private set; }

        public AnnotationType(int id_type, String label)
        {
            this.id_type = id_type;
            this.label = label;

            types.Add(id_type, this);
        }
    }
}
