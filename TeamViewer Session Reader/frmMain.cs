using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TeamViewer_Session_Reader.Classes;
using TeamViewer_Session_Reader.Helpers;

namespace TeamViewer_Session_Reader {
    public partial class frmMain : Form {

        SortableBindingList<Session> SessionList = new SortableBindingList<Session>();
        string path = string.Empty;
        int i = 0;
        public frmMain() {
            InitializeComponent();
            Scan();
            if (string.IsNullOrEmpty(path)) {
                MessageBox.Show("Could not locate File, Please load manually");
            } else {
                Process();
            }
        }

        private void Scan() {
            path = FileHelper.SearchFiles("Connections.txt", 
                                                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"TeamViewer")).
                                                        FirstOrDefault() ?? string.Empty;
        }

        private void SetupData(SortableBindingList<Session> list) {
            dgvSessions.DataSource = list;
            tbxConCount.Text = SessionList.LongCount().ToString();
            tbxConAmount.Text = new TimeSpan(SessionList.Sum(o => o.SessionLength.Ticks)).ToString(@"hh\:mm\:ss");
        }

        private void PerformRead(string p) {
            string[] newWork = new string[] { };
            newWork = FileHelper.ReadWhenPossible(p);
            SessionList = new SortableBindingList<Session>(Digest(newWork));
        }

        private void Process() {
            PerformRead(path);
            SetupData(SessionList);
        }

        private void Filter() {
            SortableBindingList<Session> temp = new SortableBindingList<Session>(SessionList.Where(oo => oo.SessionOpen.Date >= dtpFrom.Value.Date && 
                                                                                                                                                             oo.SessionOpen.Date <= dtpTo.Value.Date).ToList());
            SetupData(temp);
        }
        
        private List<Session> Digest(string[] w) {
            Session temp = new Session();
            string hold = "";
            List<Session> res = new List<Session>();
            for (int i = 0; i < w.LongLength; i++) {
                hold = w[i];
                if (!string.IsNullOrEmpty(hold)) {                                                                      //First Line is usually blank
                    temp = ConversionHelper.StringToSession(hold);
                    temp.CalcSessionLength();
                    if (!(temp == null))
                        res.Add(temp);
                }
            } 
            return res;
        }

        private void dgvSessions_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (i == 0)
                dgvSessions.Sort(dgvSessions.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Ascending);
            else
                dgvSessions.Sort(dgvSessions.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Descending);
            i = i == 0 ? 1 : 0;
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            ofdFile.ShowDialog();
            path = ofdFile.FileName;
            if (File.Exists(path))
                Process();
            else
                MessageBox.Show("File not found");
        }

        private void btnRescan_Click(object sender, EventArgs e) {
             Scan();
            if (!string.IsNullOrEmpty(path)) {
                Process();
            }
        }
    }
}
