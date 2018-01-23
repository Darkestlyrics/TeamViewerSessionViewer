using System;
using TeamViewer_Session_Reader.Helpers;

namespace TeamViewer_Session_Reader.Classes {
    class Session {

        private string partnerID;
        /// <summary>
        /// The ID of the Partner Connection
        /// </summary>
        public string PartnerID {
            get { return partnerID; }
            set { partnerID = value; }
        }

 
        private DateTime sessionOpen;
        /// <summary>
        /// The Date and Time the session Opened
        /// </summary>
        public DateTime SessionOpen {
            get { return sessionOpen; }
            set { sessionOpen = value; }
        }
        
        private DateTime sessionClose;
        /// <summary>
        /// The Date and Time the session Closed
        /// </summary>
        public DateTime SessionClose {
            get { return sessionClose; }
            set { sessionClose = value; }
        }

        private string username;
        /// <summary>
        /// The Username used to connecty
        /// </summary>
        public string UserName {
            get { return username; }
            set { username = value; }
        }

        private string connectionID;
        /// <summary>
        /// The Unique ID for the connection
        /// </summary>
        public string ConnectionID {
            get { return connectionID; }
            set { connectionID = value; }
        }

        private TimeSpan sessionLength;
        /// <summary>
        /// The length of time the Connection was alive
        /// </summary>
        public TimeSpan SessionLength {
            get { return sessionLength; }
            set { sessionLength = value; }
        }


        /// <summary>
        /// Creates a new instance of the Session Class
        /// </summary>
        public Session() {
        }

        /// <summary>
        /// Creates a new instance of the Session Class
        /// </summary>
        /// <param name="input">The string to use to populate the new instance of the Session Class</param>
        public Session(string input) {
            ConversionHelper.StringToSession(input);
            calcSessionLength();
        }

        /// <summary>
        /// Calculate the length of the session
        /// </summary>
        public void CalcSessionLength() {
            calcSessionLength();
        }

        private void calcSessionLength() {
            SessionLength = SessionClose - SessionOpen;
        }

    }
}
