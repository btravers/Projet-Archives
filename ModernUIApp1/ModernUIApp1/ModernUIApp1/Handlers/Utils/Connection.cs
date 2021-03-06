﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Handlers.Utils
{
    /* This class contains methods in order to connect the client to the web server */
    public class Connection
    {
        // Website, contains the root
        public static String ROOT_URL = "";

        static Connection() {
            var appSettings = ConfigurationManager.AppSettings;
            ROOT_URL = appSettings["server"] ?? "";
        }

        /* Encryption for URLs */
        private static String URLEncryption(String url)
        {
            // TODO : Encryption here
            url = url.Replace(" ", "+"); // For example

            return url;
        }
        /* Encryption for POST arguments */
        private static String ARGSEncryption(String arg)
        {
            // TODO : Encryption here
            arg = arg.Replace(" ", "+"); // For example

            return arg;
        }

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /* Connection which returns XmlDocument, useful when data are returned */
        public static XDocument getXmlResponse(String requestString)
        {
            return XDocument.Load(ROOT_URL + requestString);
        }

        /* Connection with GET method */
        /* TODO : This method has to return a XML file */
        public static String getRequest(String requestString)
        {
            // return
            String responseFromServer = null;
            // Create a GET request
            WebRequest resultRequest = WebRequest.Create(ROOT_URL + Connection.RemoveDiacritics(requestString));

            Console.WriteLine(ROOT_URL + requestString);

            resultRequest.Method = "GET";

            try
            {
                // Get the response
                WebResponse response = resultRequest.GetResponse();
                // DEBUG MODE Display
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                // Content send by the server
                Stream dataStream = response.GetResponseStream();
                // Read the content
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();

                // DEBUG MODE Display the content.
                Console.WriteLine(responseFromServer);

                // Clean up the streams
                reader.Close();
                dataStream.Close();
                response.Close();

            } catch (WebException e) {
                Console.WriteLine(e);
            }

            return responseFromServer;
        }

        /* Connection with POST method, accept arguments */
        /* TODO : This method has to return a XML file */
        public static String postRequest(String requestString, Dictionary<String, String> args)
        {
            // Create a POST request
            WebRequest resultRequest = WebRequest.Create(ROOT_URL + requestString);
            resultRequest.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "";
            
            // Construct the data with args sent
            foreach (String key in args.Keys.ToArray()) {
                postData += ARGSEncryption(key) + "=" + ARGSEncryption(args[key]);
            }
            // UTF8 encoding
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            resultRequest.ContentType = "application/x-www-form-urlencoded";
            
            // Set the ContentLength property of the WebRequest.
            resultRequest.ContentLength = byteArray.Length;
            
            // Get the request stream.
            Stream dataStream = resultRequest.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response
            WebResponse response = resultRequest.GetResponse();

            // DEBUG MODE Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Content send by the server
            dataStream = response.GetResponseStream();
            // Read the content
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            // DEBUG MODE Display the content.
            Console.WriteLine(responseFromServer);

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
