using System;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace CombatLog.MyWebProxy
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class WebProxyFactory
	{
		public WebProxyFactory()
		{
		}

		/// <summary>
		/// Factory class which creates a WebProxy based on configuration options
		/// specified in the UserConfig
		/// </summary>
		/// <param name="Config">Instance of UserConfig containing Proxy Configuration</param>
		/// <returns>WebProxy or null</returns>
		public static System.Net.IWebProxy CreateProxy(HttpWebRequest request, CombatLog.Config.UserConfig Config)
		{
			if ( Config == null )
                return null; // GlobalProxySelection.GetEmptyWebProxy();

			if ( !Config.UseProxy )
			{
				Debug.WriteLine("Proxy: No proxy configured");
				//
				// Must return an proxy instance here. GetEmptyWebProxy creates one for us
				//
                return null; // GlobalProxySelection.GetEmptyWebProxy();
			}

			Debug.WriteLine("Proxy: Creating proxy object for " + request.RequestUri.ToString());

			WebProxy proxy = new System.Net.WebProxy();
			
			if ( Config.UseBrowserSettings )
			{
				proxy = (WebProxy)request.Proxy;

				Debug.WriteLine("Proxy: Configured to use web browser proxy settings");

				if ( proxy.Address != null )
					Debug.WriteLine("Proxy: Using browser settings - " + proxy.Address.ToString());
				else
					Debug.WriteLine("Proxy: Browser is not configured to use a proxy");
			}
			else
			{
				proxy.Address = new System.Uri(Config.ProxyHost.Trim(new char[] {'/', ' '}) + ":" + Config.ProxyPort.ToString());
				Debug.WriteLine("Proxy: Using user supplied deta: " + proxy.Address.ToString());
			}

			if ( Config.ProxyRequiresAuthentication )
			{
				Debug.WriteLine("Proxy: Using user supplied authentication");
				proxy.Credentials = new System.Net.NetworkCredential(Config.ProxyUserName, Config.ProxyPassword);
			}
			else
			{
				Debug.WriteLine("Proxy: Not using authentication");
			}

			Debug.WriteLine("Proxy: Returning Proxy Instance to caller");

			return proxy;
		}
	}
}
