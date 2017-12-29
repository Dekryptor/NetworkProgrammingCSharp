using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AsyncClientServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new OptionForm();
            config.ShowDialog();

            var s1 = new MainForm(false, config.startServer(), config.serverRandomData(), false, config.serverAppend());
            s1.Show();
            for (int i = 0; i < config.numberofclient(); ++i)
            {
                new MainForm(true, config.startClient(), config.clientRandomData(), config.clientDisconnect(),config.clientAppend()).Show();
            }
            s1.BringToFront();

            config.Dispose();
            config = null;

            Application.Run();
        }
    }
}
