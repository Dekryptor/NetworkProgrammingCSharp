using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AnalysLogs
{
    public class Utils
    {
        private IEnumerable<string> GetSubStrings(string input, string start, string end)
        {
            Regex r = new Regex(Regex.Escape(start) + "(.*?)"  + Regex.Escape(end));
            MatchCollection matches = r.Matches(input);
            foreach (Match match in matches)
            yield return match.Groups[1].Value;
        }

        #region Excel_Supports
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static void ExportToExcel(ListBox lst, string excel_file)
        {
            //int cols;
            //open file
            StreamWriter wr = new StreamWriter(excel_file);

            //write rows to excel file
            for (int i = 0; i < (lst.Items.Count - 1); i++)
            {
                wr.Write(lst.Items[i].ToString() + "\t");

                wr.WriteLine();
            }

            //close file
            wr.Close();
        }
        #endregion
    }
}
