using System;
using System.IO;

namespace FSWatcherTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{

			string[] args = System.Environment.GetCommandLineArgs();
 
			// If a directory is not specified, exit program.
			if(args.Length != 2)
			{
				// Display the proper way to call the program.
				Console.WriteLine("Usage: Watcher.exe (directory)");
				return;
			}

			// Create a new FileSystemWatcher and set its properties.
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = args[1];
			/* Watch for changes in LastAccess and LastWrite times, and 
			   the renaming of files or directories. */
			watcher.NotifyFilter = NotifyFilters.Attributes;
			// Only watch text files.
			watcher.Filter = "*.txt";

			// Add event handlers.
			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);

			// Begin watching.
			watcher.EnableRaisingEvents = true;

			// Wait for the user to quit the program.
			Console.WriteLine("Press \'q\' to quit the sample.");
			while(Console.Read()!='q');
		}

		// Define the event handlers.
		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			// Specify what is done when a file is changed, created, or deleted.
			Console.WriteLine("File: " +  e.FullPath + " " + e.ChangeType);

			FileInfo fi = new FileInfo(e.FullPath);

			try
			{
				FileStream f = new FileStream(e.FullPath, FileMode.Open);
				Console.WriteLine("File is readable");
				f.Close();
			}
			catch (Exception err)
			{
				Console.WriteLine("Cannot open file for reading: " + err.ToString());
			}
			
		}

		private static void OnRenamed(object source, RenamedEventArgs e)
		{
			// Specify what is done when a file is renamed.
			Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
		}
	}
}

