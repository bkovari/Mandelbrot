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
            Application.Run(new Form1());
        }


        [STAThread]
        public static void Main()
        {
            Program.ApplicationInitMethods();
        }
    }
}
