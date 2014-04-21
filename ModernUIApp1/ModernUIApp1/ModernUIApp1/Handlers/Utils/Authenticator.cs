using Data.Data;
using Handlers.Handlers;
using Handlers.Utils;
using ModernUIApp1.Handlers.Utils.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ModernUIApp1.Handlers.Utils
{
    /* This class contains user authentication in order to authenticate the user when he is asking private request to the server */
    class Authenticator
    {
        // regex taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx/
        private static string EMAIL_PATTERN = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        // SALT for encryption
        private static string SALT = "@RCH1 P01LU$";

        /* SINGLETON */
        private static Authenticator authenticator;
        public static Authenticator AUTHENTICATOR {
            get {
                if (authenticator == null)
                {
                    authenticator = new Authenticator(); 
                }

                return authenticator;
            }
            private set { authenticator = value; }
        }

        /* User */
        public User user { get; private set; }
        private Boolean connected;

        private Authenticator()
        {
            connected = false;
        }

        /* Functions */
        /* Returns if a user is identified */
        public Boolean isConnected()
        {
            return connected;
        }

        /* Static functions */
        /* Encrypt a password */
        private static String passwordEncryption(String password)
        {
            // Salt it
            String saltedPassword = password + SALT;

            // Hash it
            SHA1 sha1 = SHA1.Create();
            sha1.ComputeHash(new ASCIIEncoding().GetBytes(saltedPassword));

            // Return the encrypted password
            return Convert.ToBase64String(sha1.Hash).Replace("/", "").Replace("=", "").Replace("+", ""); // TODO : caracters escape
        }

        /* Check if it's a valid email */
        private static bool isAnEmail(String email)
        {
            //return new Regex(EMAIL_PATTERN, RegexOptions.IgnoreCase).IsMatch(email);
            return email.Length >= 4;
        }

        /* Check if it's a valid password */
        private static bool isAValidPassword(String password)
        {
            return password.Length >= 4;
        }
        /* End static functions */

        /* Requests */

        // TOASK : BOOLEAN OR EXCEPTION?

        /* Send a request register() to the server */
        public bool registerNewUser(String email, String password)
        {
            
            if (!isAnEmail(email))
                return false;

            if (!isAValidPassword(password))
                return false;
            
             
            // Send a request register() to the server
            // Debug mode (with Console.write())
            String request = Resources.LinkResources.LinkRegister.Replace(Resources.LinkResources.Email, email).Replace(Resources.LinkResources.Password, passwordEncryption(password));
            Console.WriteLine(request);
/*
            String response = Connection.getRequest(request);
            Console.WriteLine(response);
*/
            XDocument xmlResponse = Connection.getXmlResponse(request);
            Parser parser = new Parser(xmlResponse);

            // If register succeed
            if (parser.getFirstNode(Resources.LinkResources.Message) == Resources.LinkResources.MsgRegistered)
            {
                // Auto connect
                login(email, password);
                return true;
            } // Else
            else
            {
                return false;
            }
        }

        /* Send a request login() to the server */
        public bool login(String email, String password)
        {
            /* Email isn't supported yet (for the server)
             * if (!isAnEmail(email))
                return false;
             */
            if (!isAValidPassword(password))
                return false;
            
            // Send a request login() to the server
            // Debug mode (with Console.write())
            String request = Resources.LinkResources.LinkLogin.Replace(Resources.LinkResources.Email, email).Replace(Resources.LinkResources.Password, passwordEncryption(password));
            Console.WriteLine("pass:"+passwordEncryption(password));
            Console.WriteLine(request);
/*
            String response = Connection.getRequest(request);
            Console.WriteLine(response);
*/
            XDocument xmlResponse = Connection.getXmlResponse(request);

            // Debug
            IEnumerable<XElement> childList =
            from el in xmlResponse.Elements()
            select el;
            
            foreach (XElement e in childList)
                Console.WriteLine(e);

            // New parser for the xmlResponse
            Parser parser = new Parser(xmlResponse);

            // If connection succeed
            if (parser.getFirstNode(Resources.LinkResources.Message) == Resources.LinkResources.MsgConnected)
            {
                user = new User(email, parser.getFirstNode(Resources.LinkResources.SessionId));
                connected = true;
                
                // Debug
                Console.WriteLine("Session id returned : " + user.id_session);

                return true;
            } // Else
            else 
            {
                user = null;
                return false;
            }
        }

        /* Send a request logout() to the server */
        public bool logout()
        {

            if (user != null)
            {
                // Send a request logout() to the server
                // Debug mode (with Console.write())
                String request = Resources.LinkResources.LinkLogout.Replace(Resources.LinkResources.Email, user.email).Replace(Resources.LinkResources.SessionId, user.id_session);
                Console.WriteLine(request);

                String response = Connection.getRequest(request);
                Console.WriteLine(response);
            }

            user = null;
            connected = false;

            return true;
        }
    }
}
