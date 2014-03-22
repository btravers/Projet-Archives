﻿using Data.Data;
using Handlers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        private User user;

        private Authenticator()
        {
        }

        /* Functions */

        /* Encrypt a password */
        private String passwordEncryption(String password)
        {
            // Debug
            Console.WriteLine("encryption");

            // Salt it
            String saltedPassword = password + SALT;

            // Hash it
            SHA1 sha1 = SHA1.Create();
            sha1.ComputeHash(new ASCIIEncoding().GetBytes(saltedPassword));

            // Return the encrypted password
            return Convert.ToBase64String(sha1.Hash);
        }

        /* Check if it's a valid email */
        private bool isAnEmail(String email)
        {
            return new Regex(EMAIL_PATTERN, RegexOptions.IgnoreCase).IsMatch(email);
        }

        /* Check if it's a valid password */
        private bool isAValidPassword(String password)
        {
            return password.Length >= 7 ? true : false;
        }

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

            String response = Connection.getRequest(request);
            Console.WriteLine(response);

            return true;
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

            String response = Connection.getRequest(request);
            Console.WriteLine(response);

            // TODO : if connection succeed
            if (isAValidPassword(password)) // TOSWITCH
            {
                user = new User(email, "id_sessionReturned");
                return true;
            }
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

            return true;
        }
    }
}
