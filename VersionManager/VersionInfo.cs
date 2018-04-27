using System;

namespace CombatLog.VersionManager
{
	/// <summary>
	/// Summary description for VersionInfo.
	/// </summary>

	public enum VersionType { Public, Alpha, Beta };

	[Serializable()]
	public class VersionInfo
	{
		private long		_VersionNumber;
		private string		_VersionString;
		private DateTime	_ReleaseDate;
		private VersionType	_ReleaseType;
		private string		_DownloadUrl;
		private string		_ReleaseNotesUrl;
        
		public VersionInfo()
		{
		}

		public string VersionString
		{
			get { return this._VersionString; }
			set { this._VersionString = value; }
		}

		public CombatLog.VersionManager.VersionType ReleaseType
		{
			get { return this._ReleaseType; }
			set { this._ReleaseType = value; }
		}

		public string DownloadUrl
		{
			get { return this._DownloadUrl; }
			set { this._DownloadUrl = value; }
		}

		public long VersionNumber
		{
			get { return this._VersionNumber; }
			set { this._VersionNumber = value; }
		}

		public string ReleaseNotesUrl
		{
			get { return this._ReleaseNotesUrl; }
			set { this._ReleaseNotesUrl = value; }
		}

		public System.DateTime ReleaseDate
		{
			get { return this._ReleaseDate; }
			set { this._ReleaseDate = value; }
		}
	}
}
