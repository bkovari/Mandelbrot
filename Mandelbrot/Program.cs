using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot
{

    static class Program
    {  
        private static void ApplicationInitMethods() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        [STAThread]
        public static void Main()
        {
            Program.ApplicationInitMethods();
        }
    }
}
