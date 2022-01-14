using System;
using System.Globalization;
using System.IO;

namespace IntuneSimple
{
    public class Credential
    {
        private static string intuneFile = @"c:\intune\localadm.txt";
        private static string userLabel = "User:";
        private static string passwordLabel = "Password:";

        private Credential()
        {

        }

        public static Credential Read()
        {
            if (!File.Exists(intuneFile))
            {
                throw new FileNotFoundException("Credenziali amministratore non impostate.", intuneFile);
            }

            var credential = new Credential();
            foreach (string line in System.IO.File.ReadLines(intuneFile))
            {
                if (line.StartsWith("Your admin account will be"))
                {
                    var datestring = line.Substring(38, 20);
                    credential.ExpiredDate = DateTime.Parse(datestring, CultureInfo.InvariantCulture);
                }

                if (line.StartsWith(userLabel))
                {
                    credential.UserName = line.Replace(userLabel, string.Empty).Trim();
                }

                if (line.StartsWith(passwordLabel))
                {
                    credential.Password = line.Replace(passwordLabel, string.Empty).Trim();
                }
            }

            return credential;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
