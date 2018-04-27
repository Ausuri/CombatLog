using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using CombatLog.VersionManager;

using CombatLog.Config;

namespace CombatLog.App
{
	/// <summary>
	/// Summary description for VersionChecker.
	/// </summary>
	public class VersionChecker
	{
		private static VersionInfoCollection _Versions = new VersionInfoCollection();
		public static UserConfig MyConfig = null;
		public const string UserAgentText = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322)";

		public VersionChecker()
		{
		}

		public static VersionInfo LatestVersion()
		{
			VersionInfo latestVersion = null;

			foreach ( VersionInfo vi in _Versions )
			{
				if ( latestVersion == null )
					latestVersion = vi;
				else if ( vi.VersionNumber > latestVersion.VersionNumber )
					latestVersion = vi;
			}

			return latestVersion;
		}

		public static VersionInfoCollection LoadFromWeb(string[] SiteURL)
		{
			string txt = "";
			Exception error = null;

			foreach ( string url in SiteURL )
			{
				try
				{
					txt = GetHTTPPage(url);
					_Versions = Deserialixe(txt);
					return _Versions;
				}
				catch (Exception e)
				{
					error = e;
					Debug.WriteLine("Error getting HTTP Page: " + error.ToString());
				}
			}

			if ( error != null )
			{
				Debug.WriteLine("VersionInfoCollection: error object is not null, throwing it to caller: " + error.ToString());
				throw error;
			}

			if ( _Versions == null )
				throw new Exception("Unable to retrieve version history information. Please try again later. If the problem persists, please contact the author by email");
			else
				return _Versions;
		}

		private static string GetHTTPPage(string SiteURL)
		{
			StringBuilder _RawHTMLData = new StringBuilder();
			UriBuilder ub;
			char[] FetchBuffer = new char[512];

			HttpWebRequest	loHttp				= null;
			HttpWebResponse loWebResponse		= null;
			StreamReader	loResponseStream	= null;

			try
			{
				ub = new UriBuilder(SiteURL);
			}
			catch (Exception err)
			{
				throw (new Exception("That URL does not look valid, please check it and try again.\n" + err.Message));
			}

			try
			{
				loHttp = (HttpWebRequest) WebRequest.Create(SiteURL);
				loHttp.Timeout = 5000; // Only wait 5 seconds for a response
		
				loHttp.UserAgent	= UserAgentText;
				loHttp.Method		= "GET";
				loHttp.ContentType	= "text/html";

				loHttp.Proxy = MyWebProxy.WebProxyFactory.CreateProxy(loHttp, MyConfig);

				loWebResponse		= (HttpWebResponse) loHttp.GetResponse();
				loResponseStream	= new StreamReader(loWebResponse.GetResponseStream()); //,Encoding.ASCII);

				_RawHTMLData.Remove(0, _RawHTMLData.Length);
				_RawHTMLData.Length = 0;

				int bytesRead = 0;
				int byteCount = 0;
				bool DoneFetching = false;

				while ( !DoneFetching ) 
				{
					bytesRead = loResponseStream.Read(FetchBuffer, 0, 512);

					if ( bytesRead != 0 )
					{
						byteCount += bytesRead;

						_RawHTMLData.Append(FetchBuffer,0,bytesRead);
					}
					else
						DoneFetching = true;
				}

				loWebResponse.Close();
				loResponseStream.Close();
			}
			catch (WebException e)
			{
				if ( loWebResponse != null )
				{
					try
					{
						loWebResponse.Close();
					}
					catch
					{
						Debug.WriteLine("ERROR CLOSING loWebResponse object");
					}
				}

				if ( loResponseStream != null )
				{
					try
					{
						loResponseStream.Close();
					}
					catch
					{
						Debug.WriteLine("ERROR CLOSING loResponseStream");
					}
				}

				throw e;
			}

			return _RawHTMLData.ToString();
		}

		public static VersionInfoCollection LoadFromFile(string FileName)
		{
			_Versions = new CombatLog.VersionManager.VersionInfoCollection();

			FileInfo f = new FileInfo(FileName);

			byte[] FileBuffer = new byte[f.Length];

			FileStream fs;
			try
			{
				fs = new FileStream(FileName, FileMode.Open);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Problem opening version history file: " + e.ToString());
				return _Versions;
			}

			fs.Read(FileBuffer, 0, (int)f.Length);
			fs.Close();

			_Versions = Deserialixe(System.Text.Encoding.UTF8.GetString(FileBuffer));

			return _Versions;
		}

		private static VersionInfoCollection Deserialixe(string XMLDoc)
		{
			VersionInfoCollection _Versions = new VersionInfoCollection();

			XmlSerializer xs = new XmlSerializer(typeof(VersionInfoCollection));

			try
			{
				TextReader tr = new StringReader(XMLDoc);

				_Versions = (VersionInfoCollection)xs.Deserialize(tr);
			}
			catch
			{
				return null;
			}

			return _Versions;
		}

		public static VersionInfo GetLatestVersion()
		{
			VersionInfo testVersion = new VersionInfo();
			testVersion.ReleaseDate = DateTime.Now;
			testVersion.ReleaseType = VersionType.Beta;
			testVersion.VersionString = "0.7";
			testVersion.VersionNumber = 7000;

			return testVersion;
		}
	}
}
