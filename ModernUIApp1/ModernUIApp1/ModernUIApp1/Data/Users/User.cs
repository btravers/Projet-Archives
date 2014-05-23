using Data.Data.Users.Shortcut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class User
    {
        int id_user;

        public String id_session { get; private set; }
        public String email { get; private set; }
        public String password { get; private set; }
        int status;

        List<Shortcut> shortcutList = new List<Shortcut>();

        /* Constructors */
        public User()
        {
        }

        public User(int id_user, String id_session)
        {
            this.id_user = id_user;
            this.id_session = id_session;
        }

        public User(String email, String id_session)
        {
            this.email = email;
            this.id_session = id_session;
        }

        public User(int id_user, String id_session, String email, String password, int status)
        {
            this.id_user = id_user;
            this.id_session = id_session;
            this.email = email;
            this.password = password;
            this.status = status;
        }

        /*public void addShortcut(Shortcut shortcut)
        {
            shortcutList.Add(shortcut);
            Console.WriteLine("add : "+shortcut.default_text);
        }
        
        public void deleteShortcut(int shortcutId)
        {
            foreach (Shortcut shortcut in shortcutList)
            {
                Console.WriteLine("suppr : "+shortcut.default_text);
                if (shortcut.id_shortcut == shortcutId)
                    shortcutList.Remove(shortcut);
            }
        }*/
    }
}
