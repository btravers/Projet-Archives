using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Registre.Annotation
{
    public enum EType
    {
        Numéro_matricule = 3,
        Nom,
        Prénom,
        Profession,
        Régiment
    }

    public class AnnotationSheet
    {
        public int id_annotations_sheet;
        Sheet sheet;
        //EType type;
        public int type { get; private set; }
        String user;

        public int x { get; private set; }
        public int y { get; private set; }
        public String text { get; private set; }

        public AnnotationSheet(int id_annotations_sheet, Sheet sheet, int type, String user, String text, int x, int y)
        {
            this.id_annotations_sheet = id_annotations_sheet;
            this.sheet = sheet;
            this.type = type;
            this.user = user;
            this.text = text;
            this.x = x;
            this.y = y;
        }

        public override String ToString()
        {
            return id_annotations_sheet + " : " + text + "\n";
        }

    }
}
