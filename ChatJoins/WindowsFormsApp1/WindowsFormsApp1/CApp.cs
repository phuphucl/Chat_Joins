using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class CApp
    {
        public string ProcessName { get; }
        public Process Process { get; set; }
        public CApp(string appName)
        {
            ProcessName = appName;
        }

    }
}
