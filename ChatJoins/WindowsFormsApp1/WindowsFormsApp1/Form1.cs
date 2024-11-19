using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        // hWndInsertAfter
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        // uFlags
        public static class SWP
        {
            public static readonly uint
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }


        private List<CApp> _LstApp = new List<CApp>();
        public Form1()
        {
            InitializeComponent();
            Restore();
            foreach(CApp app in _LstApp)
            {
                app.Process = GetProcessByName(app.ProcessName);
                if (app.Process != null)
                {
                    tabControl1.TabPages.Add(app.ProcessName);
                    TabPage newPage = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                    SetParent(app.Process.MainWindowHandle, newPage.Handle);
                    SetWindowPos(Handle, HWND_NOTOPMOST, 0, 0, 100, 100, SWP.NOSIZE);
                }
            }
            tabControl1_Resize(tabControl1, EventArgs.Empty);
        }
        private void Restore()
        {
            _LstApp.Add(new CApp("Discord"));
        }
        private List<Process> List1;
        private List<Process> List2;
        private void button1_Click(object sender, EventArgs e)
        {
        //    if (List1 == null) List1 = new List<Process>(Process.GetProcesses());
        //    else
        //    {
        //        List2 = new List<Process>(Process.GetProcesses());
        //        foreach (Process p in List1)
        //        {
        //            int index = List2.FindIndex(item => item.MainWindowHandle == p.MainWindowHandle);
        //            if (index >= 0) List2.RemoveAt(index);
        //        }
        //        Process pDiscord = List2.Find(item => item.MainWindowTitle.Contains("Discord"));
        //        Process[] p2 = Process.GetProcessesByName("Discord");
        //        var ppp = p2.Where(it => it.MainWindowHandle != IntPtr.Zero);



        //        Debug.Print(List2.Count.ToString());
        //    }
        }

        private Process GetProcessByName(string processName)
        {
            Process[] ps = Process.GetProcessesByName(processName);
            var p = ps.Where(it => it.MainWindowHandle != IntPtr.Zero);
            if (p != null && p.Count() > 0)
            {
                return p.First();
            }
            return null;
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            foreach (CApp app in _LstApp) 
            {
                
            }
        }
    }
}
