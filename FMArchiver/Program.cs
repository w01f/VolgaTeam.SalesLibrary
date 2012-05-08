﻿using System;
using System.Windows.Forms;

namespace FMArchiver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppManager.Instance.Run();
        }
    }
}
