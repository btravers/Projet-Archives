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

        String id_session;
        String email;
        String password;
        int status;

        /* Constructors */
        public User()
        {
        }

        public User(int id_user, String id_session)
        {
            this.id_user = id_user;
            this.id_session = id_session;
        }

    }
}
