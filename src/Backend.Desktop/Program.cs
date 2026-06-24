using System;
using System.Windows.Forms;

namespace Backend.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Primero lanzamos el Login
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Si el login es exitoso, corremos la ventana principal
                Application.Run(new MainForm());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}