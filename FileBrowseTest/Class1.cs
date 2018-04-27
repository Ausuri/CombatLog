using System;
using System.Collections;

namespace FileBrowseTest
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
			Hashtable foo = new Hashtable();

			string test1 = @"C:\Program Files\CCP\EVE\capture\Gamelogs\20040727_005532.txt";
			string test2 = @"C:\Program Files\CCP\EVE\capture\Gamelogs\20040727_005532.txt";

			foo.Add(test1, "The value for test1");

			try
			{
				foo.Add(test2, "The value for test2");
			}
			catch
			{
				Console.WriteLine("{0} already in cache", test2);
			}

			foreach ( object o in foo.Keys )
			{
				Console.WriteLine("Key: {0} = {1}", o.ToString(), foo[o]);
			}

			if ( foo.ContainsKey(test1) )
				Console.WriteLine("test1: {0} is in the cache", test1);

			if ( foo.ContainsKey(test2) )
				Console.WriteLine("test2: {0} is in the cache", test2);
		}
	}
}
