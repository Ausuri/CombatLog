using System;
using System.IO;

namespace CombatLog
{
	/// <summary>
	/// Summary description for ConfigGameLogDir.
	/// </summary>
	[Serializable()]
	public class ConfigGameLogDir
	{
		private string _PathName;
		private string _Alias;
		private FileSystemWatcher _Watcher = null;

		public ConfigGameLogDir()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[System.Xml.Serialization.XmlIgnoreAttribute]
		public FileSystemWatcher Watcher
		{
			get { return this._Watcher; }
			set { this._Watcher = value; }
		}

		public ConfigGameLogDir(string PathName, string Alias)
		{
			_PathName = PathName;
			_Alias = Alias;
		}

		public string Alias
		{
			get { return this._Alias; }
			set { this._Alias = value; }
		}

		public string PathName
		{
			get { return this._PathName; }
			set { this._PathName = value; }
		}
	}
}
