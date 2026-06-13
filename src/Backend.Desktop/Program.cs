using System;
using System.Windows.Forms;
using Backend.Desktop;

namespace Backend.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Ejecuta tu ventana creada por código
            Application.Run(new MainForm());
        }
    }
}