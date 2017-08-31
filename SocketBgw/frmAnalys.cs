using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AnalysLogs
{
    public partial class frmAnalys : Form
    {
        StreamReader sr;
        private ManualResetEvent mre = new ManualResetEvent(false);
        private static bool m_continue = true;
        BackgroundWorker bgW;
        List<TraceInfo> lstTraceInfo = new List<TraceInfo>();
        int lineSum = 0;

        public frmAnalys()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            // BackgroundWorker
            bgW = new BackgroundWorker();
            bgW.DoWork += new DoWorkEventHandler(bgW_DoWork);
            bgW.ProgressChanged += new ProgressChangedEventHandler(bgW_ProgressChanged);
            bgW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgW_RunWorkerCompleted);
            bgW.WorkerReportsProgress = true;
            bgW.WorkerSupportsCancellation = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string strResult = string.Empty;

            openFileDialog.Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = openFileDialog.FileName;
                btnOpen.Enabled = false;
                btnAnalys.Enabled = true;
                btnSave.Enabled = false;
                //count line of file
                string openFileName = openFileDialog.FileName;
                lineSum = File.ReadAllLines(openFileName).Length;

                try
                {
                    sr = new StreamReader(openFileDialog.OpenFile());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Qúa trình mở file log bị lỗi: " + ex.Message);
                }
            }
        }

        private void btnAnalys_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnAnalys.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;

            if (!bgW.IsBusy)
            {
                //wait until the thread completes and signals we can continue
                mre.Set();
                bgW.RunWorkerAsync();
                lblStatus.Text = "Pause";
            }
            else
            {
                mre.Reset();
                lblStatus.Text = "Resume";
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (bgW.IsBusy)
            {
                if (btnPause.Text.ToUpper() == "PAUSE")
                {
                    btnPause.Text = "Resume";
                    m_continue = false;
                    mre.Reset();
                }
                else if (btnPause.Text.ToUpper() == "RESUME")
                {

                    btnPause.Text = "Pause";
                    m_continue = true;
                    mre.Set();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bgW.IsBusy)
            {
                bgW.CancelAsync();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Excel 2003 (*.xls)|*.xls|Excel 2010-2013 (*.xlsx)|*.xlsx|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pathOutput = saveFileDialog.FileName;
                
                using (StreamWriter sw = new StreamWriter(pathOutput))
                {
                    foreach(TraceInfo trace in lstTraceInfo)
                    {
                        sw.Write(trace.STT + ": " + trace.MA + "; " + trace.SIP + "; " + trace.OS);
                        sw.Write(Environment.NewLine);
                    }
                }
            }
        }

        #region Worker_Events

        /// <summary>
        /// Analys Log
        /// </summary>
        void bgW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string line = string.Empty;
                TraceInfo traceBefore = new TraceInfo();
                int currLine = 0;

                txtCurrentLine.Invoke(new MethodInvoker(delegate
                {
                    txtCurrentLine.Text = string.Empty;
                }));
                
                while ((line = sr.ReadLine()) != null)
                {
                    mre.WaitOne();
                    Thread.Sleep(100);
                    // Progress lineSum
                    double currPercent = ((double)++currLine / (double)lineSum) * 100;
                    int percent = Int32.Parse(Math.Floor(currPercent).ToString());

                    bgW.ReportProgress(percent);
                    // Display current line process.
                    txtCurrentLine.Invoke(new MethodInvoker(delegate
                    {
                        txtCurrentLine.Text = line;
                    }));

                    
                    if (bgW.CancellationPending)
                    {
                        e.Cancel = true;
                        bgW.ReportProgress(0);
                        return;
                    }

                    //TraceLine(line, traceBefore);

                    int startMA = line.IndexOf(@"&MA=") + 4;
                    int endMA = line.IndexOf(@"&RN=");
                    int startSIP = line.IndexOf(@"&SIP=") + 5;
                    int endSIP = line.IndexOf(@"&OS=");
                    int startOS = line.IndexOf(@"&OS=") + 4;
                    int endOS = line.IndexOf(@"&SIGN=");

                    if ((startMA > 0) && (startSIP > 0) && (startOS > 0))
                    {
                        if ((startMA < endMA) && (startSIP < endSIP) && (startOS < endOS))
                        {
                            TraceInfo traceInfo = new TraceInfo();
                            traceInfo.STT = traceBefore.STT + 1;
                            traceInfo.MA = line.Substring(startMA, endMA - startMA);
                            traceInfo.SIP = line.Substring(startSIP, endSIP - startSIP);
                            traceInfo.OS = line.Substring(startOS, endOS - startOS);

                            if (traceBefore.CompareTo(traceInfo) != 0)
                            {
                                traceBefore = traceInfo;
                                lstTraceInfo.Add(traceInfo);
                            }
                        }
                    }
                }
                if(line == null)
                {
                    MessageBox.Show("Ket thuc doc log .");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Qua trinh doc log co loi: " + ex.Message);
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }

            // Operation completed 100%
            bgW.ReportProgress(100);
        }

        /// <summary>
        /// Notification is performed here to the progress bar
        /// </summary>
        void bgW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Here you play with the main UI thread
            progressBar.Value = e.ProgressPercentage;
            lblStatus.Text = "Processing......" + progressBar.Value.ToString() + "%";
        }

        /// <summary>
        /// On completed do the appropriate task
        /// </summary>
        void bgW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //If it was cancelled midway
            if (e.Cancelled)
            {
                lblStatus.Text = "Task Cancelled.";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "Error while performing background operation.";
            }
            //else if(e.Result == object)
            //{
            
            //}
            else
            {
                lblStatus.Text = "Task Completed...";
            }
            mre.Reset();

            btnOpen.Enabled = true;
            btnAnalys.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            btnSave.Enabled = true;
        }
        #endregion
    }
}
