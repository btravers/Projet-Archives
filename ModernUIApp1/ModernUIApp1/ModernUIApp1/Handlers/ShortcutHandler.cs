using Data.Data.Users.Shortcut;
using ModernUIApp1.Handlers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Handlers.Handlers
{
    public class ShortcutHandler
    {
        public void createShortcut(int field)
        {
            //if(Authenticator.AUTHENTICATOR.user.id_user != null)
            //INSERT INTO Shortcut (id_shortcut, id_user, id_type, default_text, id_Icon) VALUES (0, Authenticator.AUTHENTICATOR.user.id_user, field, "", 0); 
            
            //id_icon par défaut ? id_icon en int ?
            //Si on ajoute qque chs dans le base, l'id est forcément unique ?
            
            throw new NotImplementedException();
        }

        public void deleteShortcut(int id_shortcut)
        {
            	/**
	 * Delete an annotation on a Sheet
	public static function delete_annotation_sheet($idAnnotationSheet)
	{
		if (Database::existAnnotationSheet($idAnnotationSheet) == -1) {
			return array("message" => "annotation_not_found");
		} else {
			Database::exec("DELETE FROM AnnotationSheet WHERE id_annotation_sheet = ?",array($idAnnotationSheet));
			return array("message" => "deleted");
		}
	}
     */       throw new NotImplementedException();
        }

        public void annotateWithShortcut(int id_shortcut, int text, int x, int y, int width, int height)
        {
            throw new NotImplementedException();
        }

        public List<Shortcut> getAllShortcut()
        {
            //Si l'utilisateur n'est pas nul
            //Appele le serv avec l'id_session
            throw new NotImplementedException();
        }
    }
}
