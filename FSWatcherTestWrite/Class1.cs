using System;
using System.IO;

namespace FSWatcherTestWrite
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
		static void Main(string[] args)
		{
			string foo = "hello there";

			if ( File.Exists("foo.txt") )
				File.Delete("foo.txt");

			FileStream f = new FileStream("foo.txt",FileMode.CreateNew);
			f.Write(System.Text.Encoding.ASCII.GetBytes(foo),0,foo.Length);

			while ( Console.Read() != 'q' );

			f.Close();
		}
	}
}
