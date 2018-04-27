using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace CombatLog.Utils
{
	/// <summary>
	/// Summary description for LogFile.
	/// </summary>
	public class LogFile
	{
		public LogFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		private static string StripColorTags(string s)
		{
			string r;
			string original = s;

			try
			{
				if ( s.IndexOf("<color=0x") != -1 )
				{
					r = s.Substring(0, s.IndexOf("<color",0));
					r += s.Substring(s.IndexOf("<color") + 18);
					s = r;
				}

				s = s.Replace("&lt;", "<");
				s = s.Replace("&gt;", ">");
			}
			catch (Exception e)
			{
				Debug.WriteLine("StripColorTags Exception: " + original);
				Debug.WriteLine("StripColorTags Exception: " + e.ToString());
			}

			return s;
		}

		public static string GetLineFromPosition(string FileName, long[] Positions)
		{
			if ( !File.Exists(FileName) )
				throw new Exception("File '" + FileName + "' not found!");

			StringBuilder logFileText = new StringBuilder();

			try
			{
				logFileText.Append(LoadTextFile(FileName));
			}
			catch (Exception e)
			{
				throw e;
			}

			StringBuilder lines = new StringBuilder();

			for ( int i = 0; i < Positions.Length; i++ )
				lines.Append(StripColorTags(GetLine(logFileText.ToString(), Positions[i])));

			return lines.ToString();
		}

		public static string GetLineFromPosition(string FileName, long Position)
		{
			if ( !File.Exists(FileName) )
				throw new Exception("File '" + FileName + "' not found!");

			StringBuilder logFileText = new StringBuilder();

			try
			{
				logFileText.Append(LoadTextFile(FileName));
			}
			catch (Exception e)
			{
				throw e;
			}

			return StripColorTags(GetLine(logFileText.ToString(), Position));
		}

		private static string GetLine(string Text, long Position)
		{
			StringBuilder line = new StringBuilder();

			char addChar = 'a';
			for ( long i = Position; i < Text.Length; i++ )
			{
				if ( addChar == '\n' )
					break;

				addChar = Text[(int)i];

				line.Append(addChar);
			}

			return line.ToString();

		}
		

		private static string LoadTextFile(string FileName)
		{
			StringBuilder logFileText = new StringBuilder();

			try
			{
				StreamReader sr = File.OpenText(FileName);

				logFileText.Append(sr.ReadToEnd());

				sr.Close();
			}
			catch (Exception e)
			{
				throw e;
			}

			return logFileText.ToString();
		}
	}
}
