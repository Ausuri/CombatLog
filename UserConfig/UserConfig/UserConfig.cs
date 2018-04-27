using System;
using System.IO;
using System.Diagnostics;
using SaveListView;

namespace CombatLog.Config
{
	/// <summary>
	/// Summary description for UserConfig.
	/// </summary>
	[Serializable()]
	public class UserConfig
	{
		private bool _DebugEnabled;
		private bool _MinimiseToSystemTray;
		private bool _DetectNewLogFiles;
		private bool _NotifyNewLogFiles;
		private bool _CheckForNewVersion;
		private bool _RememberWindowLayout;
		private bool _RememberColumnWidths;

		private bool	_UseProxy;
		private bool	_UseBrowserSettings;
		private string	_ProxyHost;
		private int		_ProxyPort;
		private bool	_ProxyRequiresAuthentication;
		private string _ProxyUserName;
		private string _ProxyPassword;

		private WBColumnsCollection _WBColumnDisplayPreferences;

		private string _MainWindowLayout;
		private string _AnalysisWindowLayout;

		private System.Windows.Forms.FormWindowState _AnalysisWindowState;

		private SaveListView.ListViewSettings _FileBrowserListViewSettings;
		private SaveListView.ListViewSettings _CombatDataMainLVS;
		private SaveListView.ListViewSettings _CombatDataSummaryLVS;
		private SaveListView.ListViewSettings _AttackerDataLVS;
		private SaveListView.ListViewSettings _AttackerDataSummaryLVS;

		private ConfigGameLogDirCollection _GameLogDirs;

		public string AliasFromFileName(string FileName)
		{
			string DirName = Path.GetDirectoryName(FileName);

			foreach ( ConfigGameLogDir d in this.GameLogDirs )
				if ( d.PathName == DirName )
					return d.Alias;

			return null;
		}


		public UserConfig()
		{
		}

		public WBColumnsCollection WeaponBrowserColumnDisplayPreferences
		{
			get { return this._WBColumnDisplayPreferences; }
			set { this._WBColumnDisplayPreferences = value; }
		}

		public bool UseProxy
		{
			get { return this._UseProxy; }
			set { this._UseProxy = value; }
		}

		public bool ProxyRequiresAuthentication
		{
			get { return this._ProxyRequiresAuthentication; }
			set { this._ProxyRequiresAuthentication = value; }
		}

		public string ProxyPassword
		{
			get { return this._ProxyPassword; }
			set { this._ProxyPassword = value; }
		}

		public string ProxyUserName
		{
			get { return this._ProxyUserName; }
			set { this._ProxyUserName = value; }
		}

		public int ProxyPort
		{
			get { return this._ProxyPort; }
			set { this._ProxyPort = value; }
		}

		public string ProxyHost
		{
			get { return this._ProxyHost; }
			set { this._ProxyHost = value; }
		}
		public bool UseBrowserSettings
		{
			get { return this._UseBrowserSettings; }
			set { this._UseBrowserSettings = value; }
		}

		public bool RememberWindowLayout
		{
			get { return this._RememberWindowLayout; }
			set { this._RememberWindowLayout = value; }
		}

		public bool RememberColumnWidths
		{
			get { return this._RememberColumnWidths; }
			set { this._RememberColumnWidths = value; }
		}

		public System.Windows.Forms.FormWindowState AnalysisWindowState
		{
			get { return this._AnalysisWindowState; }
			set { this._AnalysisWindowState = value; }
		}

		public string MainWindowLayout
		{
			get { return this._MainWindowLayout; }
			set { this._MainWindowLayout = value; }
		}

		public string AnalysisWindowLayout
		{
			get { return this._AnalysisWindowLayout; }
			set { this._AnalysisWindowLayout = value; }
		}

		public ListViewSettings CombatDataMainLVS
		{
			get { return this._CombatDataMainLVS; }
			set { this._CombatDataMainLVS = value; }
		}

		public ListViewSettings CombatDataSummaryLVS
		{
			get { return this._CombatDataSummaryLVS; }
			set { this._CombatDataSummaryLVS = value; }
		}

		public ListViewSettings AttackerDataLVS
		{
			get { return this._AttackerDataLVS; }
			set { this._AttackerDataLVS = value; }
		}

		public ListViewSettings AttackerDataSummaryLVS
		{
			get { return this._AttackerDataSummaryLVS; }
			set { this._AttackerDataSummaryLVS = value; }
		}

		public SaveListView.ListViewSettings FileBrowserListViewSettings
		{
			get { return this._FileBrowserListViewSettings; }
			set { this._FileBrowserListViewSettings = value; }
		}

		public bool DebugEnabled
		{
			get { return _DebugEnabled; }
			set { _DebugEnabled = value; }
		}

		public CombatLog.ConfigGameLogDirCollection GameLogDirs
		{
			get { return this._GameLogDirs; }
			set { this._GameLogDirs = value; }
		}

		public bool MinimiseToSystemTray
		{
			get { return _MinimiseToSystemTray; }
			set { _MinimiseToSystemTray = value; }
		}

		public bool NotifyNewLogFiles
		{
			get { return this._NotifyNewLogFiles; }
			set { this._NotifyNewLogFiles = value; }
		}

		public bool DetectNewLogFiles
		{
			get { return this._DetectNewLogFiles; }
			set { this._DetectNewLogFiles = value; }
		}

		public bool CheckForNewVersion
		{
			get { return this._CheckForNewVersion; }
			set { this._CheckForNewVersion = value; }
		}
	}
}
