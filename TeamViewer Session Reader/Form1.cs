using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TeamViewer_Session_Reader.Classes;
using TeamViewer_Session_Reader.Helpers;

namespace TeamViewer_Session_Reader {
    public partial class Form1 : Form {
        List<Session> SessionList = new List<Session>();
        string path = string.Empty;
        public Form1() {
            InitializeComponent();

            PerformRead(path);
            SetupData();
        }

        private void Scan() {
            path = FileHelper.SearchFiles("Connections.txt", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TeamViewer").First();
        }

        private void SetupData() {
            dgvSessions.DataSource = SessionList;
            tbxConCount.Text = SessionList.LongCount().ToString();
            tbxConAmount.Text = new TimeSpan(SessionList.Sum(o => o.SessionLength.Ticks)).ToString();
        }

        private void PerformRead(string p) {
            string[] newWork = new string[] { };
            newWork = FileHelper.ReadWhenPossible(p);
            SessionList = Digest(newWork);
        }
        
        private List<Session> Digest(string[] w) {
            Session temp = new Session();
            string hold = "";
            List<Session> res = new List<Session>();
            for (int i = 0; i < w.LongLength; i++) {
                hold = w[i];
                if (!string.IsNullOrEmpty(hold)) {                                  //First Line is usually blank
                    temp = ConversionHelper.StringToSession(hold);
                    temp.CalcSessionLength();
                    if (!(temp == null))
                        res.Add(temp);
                }
            }
            
            return res;
        }
    }
}
