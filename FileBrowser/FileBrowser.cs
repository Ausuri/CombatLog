using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace CombatLog
{
	public struct UserDirectory
	{
		public string Alias;
		public string DirectoryPath;
	}

	public struct LogFile
	{
		public string Alias;
		public string FileName;
	}
	/// <summary>
	/// Summary description for FileBrowser.
	/// </summary>
	public class FileBrowser
	{
		UserDirectory[] _DirectoryList;

		public FileBrowser()
		{
		}

		private LogFile[] ProcessUserDirs(UserDirectory Directories)
		{
			int FileCount = 0;
			foreach ( UserDirectory s in Directories )
			{
				DirectoryInfo di = new DirectoryInfo(s.DirectoryPath);
				try
				{
					FileCount += di.GetFiles().Length;
				}
				catch (Exception e)
				{
					Debug.WriteLine("Problem in ProcessUserDirs: " + e.ToString());
				}
			}
			Debug.WriteLine("Total filecount = " + FileCount.ToString());

			LogFile[] LogFiles = new LogFile[FileCount];

			int LogFileCount = 0;
			foreach ( UserDirectory s in this._DirectoryList )
			{
				DirectoryInfo di = new DirectoryInfo(s.DirectoryPath);

				Debug.WriteLine("Processing directory: " + s.DirectoryPath);

				try
				{
					FileInfo[] Files = di.GetFiles();

					foreach ( FileInfo fi in Files )
					{
						LogFiles[LogFileCount].Alias	= s.Alias;
						LogFiles[LogFileCount].FileName	= fi.FullName;
						LogFileCount++;
					}
				}
				catch (Exception e)
				{
					Debug.WriteLine("Problem in ProcessUserDirs: " + s.DirectoryPath);
					Debug.WriteLine(e.ToString());
				}
			}

			return LogFiles;
		}

		public LogFile[] GetFileList(CombatLogCacheCollection LogFileCache, UserDirectory Directories)
		{
			Win32.HiPerfTimer HTimer = new Win32.HiPerfTimer();

			Debug.WriteLine("FB: " + LogFileCache.Count + " items in cache");

			HTimer.Start();

			LogFile[] LogFiles = ProcessUserDirs(Directories);

			HTimer.Stop();
			Debug.WriteLine("TIMER: Time to execute ProcessUserDirs: " + HTimer.Duration.ToString());

			ArrayList ReturnLogFiles = new ArrayList();

			HTimer.Start();
			foreach ( LogFile f in LogFiles )
			{
				if ( LogFileCache.Contains(f.FileName) )
				{
					// Debug.WriteLine("IN CACHE: " + f.FileName);
					if ( LogFileCache[f.FileName].IsCombatLog )
					{
						ReturnLogFiles.Add(f);
					}
				}
			}
			HTimer.Stop();
			Debug.WriteLine("TIMER: Time to check file list against cache: " + HTimer.Duration.ToString());

			LogFile[] CombatLogFiles = new LogFile[ReturnLogFiles.Count];

			HTimer.Start();
			for (int i = 0; i < ReturnLogFiles.Count; i ++)
				CombatLogFiles[i] = (LogFile)ReturnLogFiles[i];

			HTimer.Stop();
			Debug.WriteLine("TIMER: Time to build logfile array: " + HTimer.Duration.ToString());

			return CombatLogFiles;
		}


		public UserDirectory[] DirectoryList
		{
			get { return this._DirectoryList; }
			set { this._DirectoryList = value; }
		}
	}
}
