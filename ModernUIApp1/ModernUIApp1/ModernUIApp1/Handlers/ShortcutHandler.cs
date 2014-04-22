using Data.Data.Users.Shortcut;
using Handlers.Utils;
using ModernUIApp1.Handlers.Utils;
using ModernUIApp1.Handlers.Utils.Parsers;
using ModernUIApp1.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Handlers.Handlers
{
    public class ShortcutHandler
    {
        public void createShortcut(int field, string text, int idIcon)
        {
            if(Authenticator.AUTHENTICATOR.user != null)
            {
                String xmlResponse = Connection.getRequest(LinkResources.LinkAddShortcut.Replace(LinkResources.SessionId, Authenticator.AUTHENTICATOR.user.id_session.ToString()).Replace(LinkResources.IdType, field.ToString()).Replace(LinkResources.Text, text).Replace(LinkResources.IdIcon, idIcon.ToString()));
                if (xmlResponse != null)
                {
                    //TODO : Retourner l'id du shortcut crée
                    //Du coup il faut parser et récuper cette valeur
                    //Parser parser = new Parser(xmlResponse);
                    //int idShotcut = parser.parseCreateShortcut()
                    //Shortcut shortcut = new Shortcut(idShortcut, field, text, idIcon);
                    Shortcut shortcut = new Shortcut();
                    Authenticator.AUTHENTICATOR.user.addShortcut(shortcut);
                }
                else 
                    throw new Exception("xmlResponse in createShortcut is null \n");
            }
            else
                throw new Exception("A visitor can't use the createShortcut fonctionality \n");
        }

        public void deleteShortcut(int id_shortcut)
        {
            if (Authenticator.AUTHENTICATOR.user != null)
            {
                String xmlResponse = Connection.getRequest(LinkResources.LinkDeleteShortcut.Replace(LinkResources.IdShortcut, id_shortcut.ToString()));
                if (xmlResponse != null)
                {
                    //TODO : Parser la réponse du serv, ou tester avec le if ci-dessous
                    Parser parser = new Parser(xmlResponse);
                    if (xmlResponse == "...")
                        Authenticator.AUTHENTICATOR.user.deleteShortcut(id_shortcut);
                    else
                        throw new Exception("wrong aswer from the server \n");
                }
                else
                    throw new Exception("xmlResponse in deleteShortcut is null \n");
            }
            else
                throw new Exception("A visitor can't use the deleteShortcut fonctionality \n");
        }

        public List<Shortcut> getAllShortcut()
        {
            if (Authenticator.AUTHENTICATOR.user != null)
            {
                String xmlResponse = Connection.getRequest(LinkResources.LinkGetAllShortcut.Replace(LinkResources.IdSession, Authenticator.AUTHENTICATOR.user.id_session)); 
                if (xmlResponse != null)
                {
                    Parser parser = new Parser(xmlResponse);
                    
                    //get all shortcut : return parser.parserGetAllShortcut()
                    List<Shortcut> sc = new List<Shortcut>();
                    return sc;
                }
                else
                    throw new Exception("xmlResponse in getAllShortcut is null \n");
            }
            else
                throw new Exception("A visitor can't use the getAllShortcut fonctionality \n");
        }
    }
}
