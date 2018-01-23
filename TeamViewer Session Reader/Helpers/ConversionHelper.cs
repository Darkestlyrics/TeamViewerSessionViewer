using System;
using TeamViewer_Session_Reader.Classes;

namespace TeamViewer_Session_Reader.Helpers {

    static class ConversionHelper {

        /// <summary>
        /// Converts a string to a Session
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static Session StringToSession(string s) {
            try {
                return stringToSession(s);
            } catch (Exception ex) {
                throw new Exception("Error encountered during Conversion", ex);
            }
        }

        private static Session stringToSession(string s) {
            string temp = s;
            temp = temp.Replace("             ", ",");
            temp = temp.Replace("  ", "");
            string[] values = temp.Split(',');
            Session res = new Session() {
                PartnerID = values[0],
                SessionOpen = DateTime.ParseExact(values[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                SessionClose = DateTime.ParseExact(values[2], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                UserName = values[3],
                ConnectionID = values[6]
            };
            return res;
        }
    }
}
