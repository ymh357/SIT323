using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SIT323
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            Validater v = new Validater();
            String IISFilename = @"http://www.it.deakin.edu.au/SIT323/Task2/Test1.czl";
            String azureFilename=@"http://sit323.azurewebsites.net/Task2/Test1.czl";
            try
            {
                 v.GetMaxCrozzle(IISFilename);
            }
            catch
            {
                v.GetMaxCrozzle(azureFilename);
            }

            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

            /*SubSolutions subSolutions = new SubSolutions(v.WordList, 2);
            subSolutions.testSolutions();*/

            
        }
    }
}
