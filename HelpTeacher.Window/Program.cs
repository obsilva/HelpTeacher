using HelpTeacher.Forms;
using System;
using System.Windows.Forms;

namespace HelpTeacher
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var loga = new Login();
			if (loga.ShowDialog() == DialogResult.OK)
			{
				Application.Run(new Principal());
			}
			else
			{
				Application.Exit();
			}
		}
	}
}
