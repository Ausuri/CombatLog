using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Diagnostics;
using NSpring.Logging;
    
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
		public long FileSize;
		public DateTime CreatedDTM;
		public string LeafName;
	}

    /// <summary>
    /// Strongly typed collection of CombatLog.GameLog.
    /// </summary>
    public class GameLogCollection : System.Collections.CollectionBase
    {

		#region Private member variables
		private Logger logger = null;
        
		// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\EVE - C:\Program Files\CCP\EVE\uninst.exe
		//
		// Chat logs in {PathEve}\Capture\ChatLogs

		private string uninstallRegKeyDir = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\EVE";
		private string uninstallRegKeyVal = "UninstallString";
		private string chatLogDir = @"\capture\GameLogs\";
		private string installDirectory = null;
		#endregion
		
		#region Event interfaces
		//
		// Event Handler
		//
		public delegate void LogFileProcessedEvent(object Sender, CombatLogCacheCollection LogFileLoadedEventArgs, int Current, int Total);
		public event LogFileProcessedEvent LogFileProcessed_Event = null;

		public delegate void LogFileProcessingComplete(object Sender, System.EventArgs e);
		public event LogFileProcessingComplete ProcessingComplete_Event = null;

		public delegate void LogFileProcessingStarting(object Sender, int FileCount);
		public event LogFileProcessingStarting ProcessingStarted_Event = null;

		#endregion

		public Cache LogCache = null;
		Hashtable GameLogPK = new Hashtable();
		Hashtable UniqueListeners = new Hashtable();
        
		private string ReadRegKey(string KeyName, string SubKeyName)
		{
			string s = "";

			try 
			{
				RegistryKey d = Registry.LocalMachine.OpenSubKey(KeyName);

				s = (string)d.GetValue(SubKeyName);
			}
			catch
			{
				throw new Exception("Could not get installation directory information from the registry. Please use the Options->Gamelogs dialog to manually add gamelog directories");
			}

			return s;
		}

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameLogCollection() : 
                base()
        {
		}

		public string GetDefaultInstallPath()
		{
			try
			{
				string s = ReadRegKey(uninstallRegKeyDir, uninstallRegKeyVal);

				//
				// The registry key returns a path to the uninstall executable so we need to strip the uninstall.exe from the string
				//

				installDirectory = s.Substring(0, s.IndexOf("Uninstall.exe") - 1) + chatLogDir;
			}
			catch (Exception e)
			{
				throw e;
			}

			return installDirectory;
		}

		public string[] GetUniqueLogDirs()
		{
			Hashtable LogDirs = new Hashtable();

			foreach ( GameLog g in this.List )
			{
				try
				{
					LogDirs.Add(g.PathAlias, "foo");
				}
				catch
				{
				}
			}

			string[] l = new string[LogDirs.Count];
			int i = 0;

			foreach ( object p in LogDirs.Keys )
				l[i++] = p.ToString();

			return l;
		}

		public string[] GetUniqueListeners()
		{
			string[] Listeners = new string[UniqueListeners.Count];

			int i = 0;
			foreach ( object o in UniqueListeners.Keys )
				Listeners[i++] = o.ToString();

			return Listeners;
		}

		public GameLogCollection FilterBy(string Listener)
		{
			GameLogCollection gc = new GameLogCollection();

			foreach ( GameLog l in this.List )
			{
				if ( l.Listener == Listener )
					gc.Add(l);
			}

			return gc;
		}

		public GameLogCollection FilterByLogDir(string logdir)
		{
			GameLogCollection gc = new GameLogCollection();

			foreach (GameLog l in this.List )
			{
				if ( l.PathAlias == logdir )
					gc.Add(l);
			}

			return gc;
		}

		public GameLogCollection FilterBy(int AgeInDays)
		{
			GameLogCollection gc = new GameLogCollection();

			foreach ( GameLog l in this.List )
			{
				TimeSpan t = new TimeSpan(AgeInDays,0,0,0);

				if ( l.SessionStartedDTM >= DateTime.Now.Subtract(t) )
					gc.Add(l);
			}

			return gc;
		}

		public GameLogCollection FilterBy(string Listener, int AgeInDays)
		{
			GameLogCollection ByListener = this.FilterBy(Listener);

			return ByListener.FilterBy(AgeInDays);
		}


		private LocationInfo.Location GetClosestPreviousLocation(LocationInfo.LocationCollection locs, DateTime CombatEntriesStartDTM)
		{
			LocationInfo.Location recentLocation = null;

			foreach ( LocationInfo.Location l in locs )
			{
				if ( l.LogDTM < CombatEntriesStartDTM )
					recentLocation = l;
			}

			return recentLocation;
		}

		private void SortBySessionDTM(System.Windows.Forms.SortOrder Direction)
		{
			this.InnerList.Sort(new Sort.GameLogCollectionSorter(Direction));
		}

		//
		// Return the chronologically previous game log file from the one specified by the caller
		// This method relies on the CacheData collection being sorted decending by SessionStartDTM
		//
		private CombatLogCache GetPreviousLogFile(string FromThisFile)
		{
			for ( int i = 0; i < this.LogCache.CacheData.Count; i++ )
			{
				if ( this.LogCache.CacheData[i].FileName == FromThisFile )
				{
					if ( (i + 1) < this.LogCache.CacheData.Count )
						return this.LogCache.CacheData[i+1];
					else
						return null;
				}
			}

			return null;
		}

		public CombatLog.LocationInfo.LocationCollection GetLocations(GameLog g, DateTime StartDTM)
		{
			bool HaveLocation = false;

			LocationInfo.LocationCollection locs = g.GetWarpMessages();

			if ( locs == null )
				return null;

			LocationInfo.Location recentLocation = GetClosestPreviousLocation(locs, StartDTM);

			//
			// We have a recent location for the current gamelog file which
			// preceeds the start of the combat entries so return this
			//

			if ( recentLocation != null )
				return locs;

			string LastLogFileName = g.FileName;
			int MaxLogFileRegressions = 4;
			int RegressCount = 0;

			this.LogCache.CacheData.SortBySessionDTM(System.Windows.Forms.SortOrder.Descending);

			while ( !HaveLocation && RegressCount < MaxLogFileRegressions )
			{
				CombatLogCache cl = GetPreviousLogFile(LastLogFileName);

				GameLog gl = new GameLog(cl.FileName);

				//
				// There are no previous gamelogs so no chance of getting location data
				//
				if ( gl == null )
					return null;

				//
				// Extract any "Warping to" messages from this log file
				//

				LocationInfo.LocationCollection NewLocations = gl.GetWarpMessages();

				//
				// Keep a copy of recent location information. This can be used
				// by the client to plot recent travel information.
				//
				if ( NewLocations != null )
					foreach ( LocationInfo.Location l in NewLocations )
						locs.Add(l);

				//
				// If there are warp messages here, get the most recent one that preceeds the 
				// combat entries starting and return to caller.
				//
				if ( locs != null )
				{
					recentLocation = GetClosestPreviousLocation(locs, StartDTM);

					if ( recentLocation != null )
					{
						// Debug.WriteLine("Found a suitably recent warping to message: " + recentLocation.LocationStr + " in " + recentLocation.SourceFileName);
						return locs;
					}
				}

				LastLogFileName = gl.FileName;

				RegressCount++;
			}

			return null;
		}
	
		public bool UserPathSameAsRegistry(string UserPath)
		{
			if ( installDirectory == null )
				return false;

			string tmpReg;
			string tmpUser;

			if ( installDirectory.EndsWith(@"\") )
				tmpReg = installDirectory.Substring(0, installDirectory.Length -1);
			else
				tmpReg = installDirectory;

			if ( UserPath.EndsWith(@"\") )
				tmpUser = UserPath.Substring(0, UserPath.Length - 1);
			else
				tmpUser = UserPath;

			if ( tmpUser.ToLower() == tmpReg.ToLower() )
				return true;
			else
				return false;
		}


		private LogFile[] ProcessUserDirs(UserDirectory[] Directories)
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
			foreach ( UserDirectory s in Directories )
			{
				DirectoryInfo di = new DirectoryInfo(s.DirectoryPath);

				Debug.WriteLine("Processing directory: " + s.DirectoryPath);

				try
				{
					FileInfo[] Files = di.GetFiles();

					foreach ( FileInfo fi in Files )
					{
						LogFiles[LogFileCount].Alias		= s.Alias;
						LogFiles[LogFileCount].FileName		= fi.FullName;
						LogFiles[LogFileCount].FileSize		= fi.Length;
						LogFiles[LogFileCount].CreatedDTM	= fi.CreationTime;
						LogFiles[LogFileCount].LeafName		= fi.Name;
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

		private LogFile[] GetFileList(CombatLogCacheCollection LogFileCache, UserDirectory[] Directories)
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


		public void CacheThis(GameLog cl)
		{
			if ( cl.IsCombatLog() )
			{
				logger.Log(Level.Info, cl.FileName + " looks like a combat log");

				try
				{
					// cl.GetHeaders();
					CombatLogEntryCollection cLog = cl.GetCombatEntries();
					AttackEntryCollection aLog = cl.GetAttackEntries();

					CombatLogCache clc = new CombatLogCache(cl.FileName, cl.FileSize, cl.SessionStartedDTM, true);
					clc.Character = cl.Listener;
				
					if ( cLog != null )
					{
						clc.WeaponsUsed		= cLog.GetUniqueWeaponsList();
						clc.TargetsAttacked = cLog.GetUniqueTargets();
						clc.HitTypes		= cLog.GetUniqueHitTypes();
						clc.Attackers		= aLog.GetUniqueAttackers();

						WeaponSummaryData.WeaponStatsCollection WepSummary = cLog.GetWeaponStatsSummary();

						clc.WeaponStatsSummary = new WeaponSummaryData.WeaponStatsCollection();

						foreach ( WeaponSummaryData.WeaponStats ws in WepSummary )
						{
							clc.WeaponStatsSummary.Add(ws);
						}

					}

					Debug.WriteLine("Adding " + cl.FileName + " to master cache");

					clc.DirAlias = cl.PathAlias;
					this.Add(cl);
					LogCache.CacheData.Add(clc);
				}
				catch (Exception e)
				{
					Debug.WriteLine("Error getting headers for non-cached file: " + e.ToString());
					logger.Log(Level.Exception, "Error retrieving headers for non-cached file ["+cl.FileName+"]");
					logger.Log(Level.Exception, "Error message: " + e.ToString());
				}
			}
			else
			{
				Debug.WriteLine("Adding " + cl.FileName + " as a non-combat log file");
				LogCache.CacheData.Add(new CombatLogCache(cl.FileName, cl.FileSize, cl.SessionStartedDTM, false));
			}

		}
		
		private void ExpungeMissingFilesFromCache(Logger logger)
		{
			ArrayList KillFiles = new ArrayList();

			foreach ( CombatLogCache c in LogCache.CacheData )
				if ( !c.FileExists )
				{
					KillFiles.Add(c.FileName);
					Debug.WriteLine("Killfile: " + c.FileName);
				}

			string filename;
			foreach ( object o in KillFiles )
			{
				filename = o.ToString();
				GameLog rg = this[filename];
				CombatLogCache rc = this.LogCache.CacheData[filename];

				try
				{
					if ( rg != null )
					{
						Debug.WriteLine("Removing from gamelog: " + filename);
						this.Remove(rg);
					}


					if ( rc != null )
					{
						Debug.WriteLine("Removing from cache: " + filename);
						this.LogCache.CacheData.Remove(rc);
					}

				}
				catch ( Exception e )
				{
					Debug.WriteLine("Expunge Error: " + e.ToString());
				}
			}
		}

		public void EnumerateGameLogDir(ConfigGameLogDirCollection ScanPaths, Logger ParentLogger)
		{
			logger = ParentLogger;
			logger.Log(Level.Info, "In EnumerateGameLogDir");

			if ( LogCache == null )
			{
				logger.Log(Level.Exception, "Fatal error: Log Cache Object is null");
				throw new Exception("Fatal error: Log Cache Object is null");
			}

			Win32.HiPerfTimer HTimer = new Win32.HiPerfTimer();

			UserDirectory[] UserDirs = new UserDirectory[ScanPaths.Count];

			for ( int i = 0; i < ScanPaths.Count; i++ )
			{
				UserDirs[i].Alias = ScanPaths[i].Alias;
				UserDirs[i].DirectoryPath = ScanPaths[i].PathName;
			}

			logger.Log(Level.Info, "Processing log directories");
			LogFile[] LogFiles = ProcessUserDirs(UserDirs);
			logger.Log(Level.Info, "Log directories processed. Found " + LogFiles.Length + " files");

			//GameLogCollection EventBufferGameLogs = new GameLogCollection();
			CombatLogCacheCollection EventBufferGameLogs = new CombatLogCacheCollection();
			int CurrentFileNo = 0;
			int TotalFileCount = LogFiles.Length;
			double EventTimer = 0.0;
			bool FireUpdateEvent = false; // Need more updates when caching for the first time. This flag is set to true if a combat entry is added to the cache

			CombatLogCache fi = null;

			foreach ( LogFile f in LogFiles )
			{
				try
				{
					if ( LogCache.CacheData.FileCached(f.FileName) ) // File is in the cache
						fi = LogCache.CacheData[f.FileName];
					else // File is not in the cache
					{
						logger.Log(Level.Info, "CACHING FILE: " + f.FileName);

						GameLog g = new GameLog(f.FileName);
						g.FileSize = f.FileSize;
						g.PathAlias = f.Alias;

						g.GetHeaders();

						CacheThis(g);

						fi = LogCache.CacheData[f.FileName];


						// We give more frequent UI updates when building the cache as this takes significantly longer than
						// processing previously cached items.

						FireUpdateEvent = true;
					}
				}
				catch (Exception e)
				{
					logger.Log(Level.Exception, e.ToString());
				}

				fi.FileExists = true;
				EventBufferGameLogs.Add(fi);

				HTimer.Start();
				try
				{
					if ( (double)CurrentFileNo % 1000.0 == 0 || FireUpdateEvent)
					{
						logger.Log(Level.Info, "Firing enum update event for file threshold: " + CurrentFileNo);
						if ( this.LogFileProcessed_Event != null )
						{
							this.LogFileProcessed_Event(this, EventBufferGameLogs, CurrentFileNo, TotalFileCount - 1);
							EventBufferGameLogs.Clear();
						}

						FireUpdateEvent = false;
					}
				}
				catch (Exception e)
				{
					logger.Log(Level.Exception, e.ToString());
				}
				HTimer.Stop();
				EventTimer += HTimer.Duration;
				CurrentFileNo++;
			}

			logger.Log(Level.Info, "Gamelog enumeration complete");

			ExpungeMissingFilesFromCache(ParentLogger);

			try
			{
				//
				// Flush out any remaining gamelogs
				//
				if ( EventBufferGameLogs.Count != 0 )
				{
					if ( this.LogFileProcessed_Event != null )
						this.LogFileProcessed_Event(this, EventBufferGameLogs, --CurrentFileNo, --TotalFileCount);
				}

				//
				// Fire the processing complete event
				//
				if ( ProcessingComplete_Event != null )
					this.ProcessingComplete_Event(this, new System.EventArgs());

			}
			catch (Exception e)
			{
				logger.Log(Level.Exception, "Error firing enumeration complete event");
				logger.Log(Level.Exception, e.ToString());
			}
		}

		private void IndexThis(GameLog value)
		{
			try
			{
				Debug.WriteLine("INDEXTHIS: Adding " + value.FileName.ToLower() + " to GameLogPK");
				GameLogPK.Add(value.FileName.ToLower(), value);
			}
			catch {}

			try
			{
				UniqueListeners.Add(value.Listener, "bar");
			}
			catch
			{
			}
		}

		private void RemoveIndexEntries(GameLog value)
		{
			Debug.WriteLine("Removing " + value.FileName.ToLower() + " from GameLogPK");
			GameLogPK.Remove(value.FileName.ToLower());
			UniqueListeners.Remove(value.Listener);
		}

		public CombatLog.GameLog this[string FileName]
		{
			get
			{
				if ( GameLogPK.Contains(FileName.ToLower()) )
					return (GameLog)this.GameLogPK[FileName.ToLower()];
				else
					return null;
			}
		}
		
		/// <summary>
        /// Gets or sets the value of the CombatLog.GameLog at a specific position in the GameLogCollection.
        /// </summary>
        public CombatLog.GameLog this[int index]
        {
            get
            {
                return ((CombatLog.GameLog)(this.List[index]));
            }
            set
            {
                this.List[index] = value;
            }
        }
        
        /// <summary>
        /// Append a CombatLog.GameLog entry to this collection.
        /// </summary>
        /// <param name="value">CombatLog.GameLog instance.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(CombatLog.GameLog value)
        {
			IndexThis(value);
            return this.List.Add(value);
        }
        
        /// <summary>
        /// Determines whether a specified CombatLog.GameLog instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.GameLog instance to search for.</param>
        /// <returns>True if the CombatLog.GameLog instance is in the collection; otherwise false.</returns>
        public bool Contains(CombatLog.GameLog value)
        {
            return this.List.Contains(value);
        }

		public bool Contains(string FileName)
		{
			return GameLogPK.Contains(FileName.ToLower());
		}
        
        /// <summary>
        /// Retrieve the index a specified CombatLog.GameLog instance is in this collection.
        /// </summary>
        /// <param name="value">CombatLog.GameLog instance to find.</param>
        /// <returns>The zero-based index of the specified CombatLog.GameLog instance. If the object is not found, the return value is -1.</returns>
        public int IndexOf(CombatLog.GameLog value)
        {
            return this.List.IndexOf(value);
        }
        
        /// <summary>
        /// Removes a specified CombatLog.GameLog instance from this collection.
        /// </summary>
        /// <param name="value">The CombatLog.GameLog instance to remove.</param>
        public void Remove(CombatLog.GameLog value)
        {
			RemoveIndexEntries(value);
            this.List.Remove(value);
        }
        
        /// <summary>
        /// Returns an enumerator that can iterate through the CombatLog.GameLog instance.
        /// </summary>
        /// <returns>An CombatLog.GameLog's enumerator.</returns>
        public new GameLogCollectionEnumerator GetEnumerator()
        {
            return new GameLogCollectionEnumerator(this);
        }
        
        /// <summary>
        /// Insert a CombatLog.GameLog instance into this collection at a specified index.
        /// </summary>
        /// <param name="index">Zero-based index.</param>
        /// <param name="value">The CombatLog.GameLog instance to insert.</param>
        public void Insert(int index, CombatLog.GameLog value)
        {
            this.List.Insert(index, value);
        }
        
        /// <summary>
        /// Strongly typed enumerator of CombatLog.GameLog.
        /// </summary>
        public class GameLogCollectionEnumerator : object, System.Collections.IEnumerator
        {
            
            /// <summary>
            /// Current index
            /// </summary>
            private int _index;
            
            /// <summary>
            /// Current element pointed to.
            /// </summary>
            private CombatLog.GameLog _currentElement;
            
            /// <summary>
            /// Collection to enumerate.
            /// </summary>
            private GameLogCollection _collection;
            
            /// <summary>
            /// Default constructor for enumerator.
            /// </summary>
            /// <param name="collection">Instance of the collection to enumerate.</param>
            internal GameLogCollectionEnumerator(GameLogCollection collection)
            {
                _index = -1;
                _collection = collection;
            }
            
            /// <summary>
            /// Gets the CombatLog.GameLog object in the enumerated GameLogCollection currently indexed by this instance.
            /// </summary>
            public CombatLog.GameLog Current
            {
                get
                {
                    if (((_index == -1) 
                                || (_index >= _collection.Count)))
                    {
                        throw new System.IndexOutOfRangeException("Enumerator not started.");
                    }
                    else
                    {
                        return _currentElement;
                    }
                }
            }
            
            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (((_index == -1) 
                                || (_index >= _collection.Count)))
                    {
                        throw new System.IndexOutOfRangeException("Enumerator not started.");
                    }
                    else
                    {
                        return _currentElement;
                    }
                }
            }
            
            /// <summary>
            /// Reset the cursor, so it points to the beginning of the enumerator.
            /// </summary>
            public void Reset()
            {
                _index = -1;
                _currentElement = null;
            }
            
            /// <summary>
            /// Advances the enumerator to the next queue of the enumeration, if one is currently available.
            /// </summary>
            /// <returns>true, if the enumerator was succesfully advanced to the next queue; false, if the enumerator has reached the end of the enumeration.</returns>
            public bool MoveNext()
            {
                if ((_index 
                            < (_collection.Count - 1)))
                {
                    _index = (_index + 1);
                    _currentElement = this._collection[_index];
                    return true;
                }
                _index = _collection.Count;
                return false;
            }

        }
    }
}
