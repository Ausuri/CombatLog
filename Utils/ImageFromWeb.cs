using System;
using System.Drawing;
using System.Net;
using System.IO;
using System.Diagnostics;


namespace CombatLog.Utils
{
	/// <summary>
	/// Summary description for ImageFromWeb.
	/// </summary>
	public class ImageFromWeb
	{

		public static CombatLog.Config.UserConfig MyConfig;

		private string ImageCachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\EVECombatLog\IconCache\";
		public ImageFromWeb()
		{
		}

		private bool Cached(string ImageURL)
		{
			if ( !Directory.Exists(ImageCachePath) )
			{
				// No cache directory so create it
				Directory.CreateDirectory(ImageCachePath);
				return false;
			}

			if ( File.Exists(ImageCachePath + FileNameFromURL(ImageURL)) )
				return true;

			return false;
		}

		private string FileNameFromURL(string URL)
		{
			string ImageName = URL;

			if ( URL.IndexOf('/') != -1 )
			{
				string[] UrlSegments = URL.Split('/');
				ImageName = UrlSegments[UrlSegments.Length - 1];
			}

			return ImageName;
		}

		private Image GetImageFromCache(string URL)
		{
			try
			{
				Debug.WriteLine("Getting image [" + URL + "] from cache");
				return Image.FromFile(ImageCachePath + FileNameFromURL(URL));
			}
			catch
			{
				Debug.WriteLine("Unable to retrieve image from local cache!");
			}

			return null;
		}

		public Image GetImage(string sURL)
		{
			if ( Cached(sURL) )
				return GetImageFromCache(sURL);

			Stream str = null; 		
			
			try
			{
				//Create a web request to the url containing the image
				HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(sURL);
	
				//optional: use only if you're using a proxy to connect to the internet			
				wReq.Proxy = MyWebProxy.WebProxyFactory.CreateProxy(wReq, MyConfig);
				
				//gets the response from the web request 
				HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
					
				//return the image stream from the URL specified earlier
				str = wRes.GetResponseStream();
			}
			catch (Exception Ex)
			{
				throw Ex;
			}

			if (str != null)
				return Image.FromStream(str);
			else 
				return null;
		}

		public void CacheImage(string ImageURL, Image ImageToCache)
		{
			string ImageFileName = ImageCachePath + FileNameFromURL(ImageURL);

			if ( File.Exists(ImageFileName) )
				File.Delete(ImageFileName);

			try
			{
				ImageToCache.Save(ImageFileName);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Error trying to cache image file (" + ImageFileName + "): " + e.ToString());
			}
		}
	}
}
