using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CombatLog.Config;

namespace ObjectExplorer
{
	public struct SearchResult
	{
		public string ItemName;
		public int ItemID;
	}

	public struct AttributeHeading
	{
		public string HeadingName;
		public int PositionInFile;
	}

	/// <summary>
	/// Summary description for OEParser.
	/// </summary>
	public class OEParser
	{
		public UserConfig MyConfig = null;
		private const string ItemDataURL = "http://www.eve-i.com/home/crowley/page/page_ptype.php?id=";
		private const string ItemSearchURL = "http://www.eve-i.com/home/crowley/page/page_objectsearch.php?q=";
		private const string UserAgentText = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322)";

		private EVEItem _Item;

		#region Regular Expressions
		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Mon, Sep 13, 2004, 11:51:39 AM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  class="head">
		///  [Heading]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </TD>
		///  
		///  
		/// </summary>
		private Regex OEGetItemAttributeHeadings = new Regex(
			@"class=""head"">(?<Heading>.*?)</TD>",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Mon, Sep 13, 2004, 11:52:08 AM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  <TRtitle\=\"
		///      <TRtitle
		///      =
		///      "
		///  [AttrTitle]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ".*?<IMGsrc="
		///      "
		///      Any character, any number of repetitions, as few as possible
		///  [AttrImg]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ".*?<TD>
		///      "
		///      Any character, any number of repetitions, as few as possible
		///  [AttrName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </TD>.*?<TD>
		///      </TD>
		///      Any character, any number of repetitions, as few as possible
		///  [AttrValue]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </TD>
		///  
		///  
		/// </summary>
		private Regex OEGetItemAttributes = new Regex(
			@"<TR title\=\""(?<AttrTitle>.*?)"".*?<IMG src=""(?<AttrImg>.*"
			+ @"?)"".*?<TD>(?<AttrName>.*?)</TD>.*?<TD>(?<AttrValue>.*?)</TD"
			+ @">",
			RegexOptions.IgnoreCase
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Sep 16, 2004, 11:43:01 AM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  <trtitle="
		///  [ItemName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ".*?<ahref="javascript:;"onClick="showtype\(
		///      "
		///      Any character, any number of repetitions, as few as possible
		///      <ahref="javascript:;"onClick="showtype
		///      (
		///  [ItemID]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  )
		///  
		///  
		/// </summary>
		private Regex OEGetSearchResultItems = new Regex(
			@"<tr height=""32"" class=""val"".*?<a href=""javascript:;"" o"
			+ @"nClick=""showtype\((?<ItemID>.*?)\).*?\>(?<ItemName>.*?)</a>",
			RegexOptions.IgnoreCase
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Wed, Sep 15, 2004, 12:20:02 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  <H2>
		///  [ItemName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </H2>.*?<b>
		///      </H2>
		///      Any character, any number of repetitions, as few as possible
		///  [Path]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </b>.*?
		///      </b>
		///      Any character, any number of repetitions, as few as possible
		///  Match expression but don't capture it. [(IMG.*?src="(?<ImgName>.*?)".*?)?]
		///      [1]: A numbered capture group. [IMG.*?src="(?<ImgName>.*?)".*?], zero or one repetitions
		///          IMG.*?src="(?<ImgName>.*?)".*?
		///              IMG
		///              Any character, any number of repetitions, as few as possible
		///              src="
		///              [ImgName]: A named capture group. [.*?]
		///                  Any character, any number of repetitions, as few as possible
		///              "
		///              Any character, any number of repetitions, as few as possible
		///  <TDcolspan="2">
		///  [ItemDesc]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </TD>
		///  
		///  
		/// </summary>
		private Regex OEGetDetailPageSummary = new Regex(
			@"<H2>(?<ItemName>.*?)</H2>.*?<b>(?<Path>.*?)</b>.*?(?:(IMG.*?"
			+ @"src=""(?<ImgName>.*?)"".*?)?)<TD colspan=""2"">(?<ItemDesc>."
			+ @"*?)</TD>",
			RegexOptions.IgnoreCase
			| RegexOptions.Compiled
			);

		#endregion

		#region Node Processing regular Expressions

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Oct 14, 2004, 03:36:00 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  objTreeMenu_1=
		///  [Javascript]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </script>
		///  
		///  
		/// </summary>
		private Regex OBEXGetItemsJavascript = new Regex(
			@"objTreeMenu_1 = (?<Javascript>.*?)</script>",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Oct 14, 2004, 03:35:01 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  objTreeMenu_2=
		///  [Javascript]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </script>
		///  
		///  
		/// </summary>
		private Regex OBEXGetEntitiesJavascript = new Regex(
			@"objTreeMenu_2 = (?<Javascript>.*?)</script>",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Oct 14, 2004, 04:08:37 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  <trtitle="
		///  [Name]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ".*?onclick=\"showtype\(
		///      "
		///      Any character, any number of repetitions, as few as possible
		///      onclick=
		///      "
		///      showtype
		///      (
		///  [ItemID]: A named capture group. [[0-9]*]
		///      Any character in this class: [0-9], any number of repetitions
		///  \);"
		///      )
		///  
		///  
		/// </summary>
		private Regex OBEXGetGroupItems = new Regex(
			@"<tr title=""(?<Name>.*?)"".*?onclick=\""showtype\((?<ItemID>"
			+ @"[0-9]*)\);""",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.CultureInvariant
			| RegexOptions.Compiled
			);


		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Oct 14, 2004, 02:03:21 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  "
		///  [FullNodeName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  
		///  
		/// </summary>
		private Regex OBEXGetFullNodeName = new Regex(
			@"""(?<FullNodeName>.*?)""",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.ExplicitCapture
			| RegexOptions.CultureInvariant
			| RegexOptions.IgnorePatternWhitespace
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Fri, Oct 22, 2004, 01:20:19 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  newNode_
		///  [NodeID]: A named capture group. [([0-9]*)|([0-9*]_[0-9]*)|([0-9]*_[0-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*)]
		///      Select from 7 alternatives
		///          [1]: A numbered capture group. [[0-9]*]
		///              Any character in this class: [0-9], any number of repetitions
		///          [2]: A numbered capture group. [[0-9*]_[0-9]*]
		///              [0-9*]_[0-9]*
		///                  Any character in this class: [0-9*]
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///          [3]: A numbered capture group. [[0-9]*_[0-9]*_[0-9]*]
		///              [0-9]*_[0-9]*_[0-9]*
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///          [4]: A numbered capture group. [[0-9]*_[0-9]*_[0-9]*_[0-9]*]
		///              [0-9]*_[0-9]*_[0-9]*_[0-9]*
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///          [5]: A numbered capture group. [[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*]
		///              [0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///          [6]: A numbered capture group. [[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*]
		///              [0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///          [7]: A numbered capture group. [[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*]
		///              [0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///                  _
		///                  Any character in this class: [0-9], any number of repetitions
		///   =.*?TreeNode\('
		///      Space
		///      =
		///      Any character, any number of repetitions, as few as possible
		///      TreeNode
		///      (
		///  [NodeName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ',
		///  [Ignore]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  , 
		///      ,
		///      Space
		///  [URL]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  
		///  
		/// </summary>
		private Regex OBEXGetNodes = new Regex(
			@"newNode_(?<NodeID>([0-9]*)|([0-9*]_[0-9]*)|([0-9]*_[0-9]*_[0"
			+ @"-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0"
			+ @"-9]*_[0-9]*)|([0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*)|([0"
			+ @"-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*_[0-9]*)) =.*?TreeNod"
			+ @"e\('(?<NodeName>.*?)',(?<Ignore>.*?), (?<URL>.*?),",
			RegexOptions.ExplicitCapture
			| RegexOptions.CultureInvariant
			);


		#endregion

		public OEParser()
		{
		}

		public void GetItemDetail(int ItemID)
		{
			string ItemURL = ItemDataURL + ItemID.ToString();
	
			StringBuilder html = new StringBuilder();

			try
			{
				html.Append(GetHTTPPage(ItemURL));
			}
			catch (Exception err)
			{
				throw new Exception("Error fetching web page: " + err.ToString());
			}

			Item = new EVEItem();
			Item.ItemID = ItemID;
			GetItemInfoFromHTML(html.ToString());
			GetAttributesFromHTML(html.ToString());
		}

		public void GetItemDetail(EVEItem Item)
		{
			string ItemURL = ItemDataURL + Item.ItemID.ToString();
	
			StringBuilder html = new StringBuilder();

			try
			{
				html.Append(GetHTTPPage(ItemURL));
			}
			catch (Exception err)
			{
				throw new Exception("Error fetching web page: " + err.ToString());
			}

			GetItemInfoFromHTML(html.ToString(), Item);
			GetAttributesFromHTML(html.ToString(), Item);
		}

		public NodeCollection GetItemNodes()
		{
			// Get main page

			// Find Javascript blocks

			// Extract node data from javascript blocks

			// Create a nodes collection

			// return to caller

			StringBuilder MainPage = new StringBuilder();
			StringBuilder JavaScriptBlocks = new StringBuilder();
			NodeCollection Nodes = new NodeCollection();

			try
			{
				MainPage.Append(GetHTTPPage(@"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php"));
				JavaScriptBlocks.Append(GetJavaScriptItemBlock(MainPage.ToString()));
				Nodes = GetNodesFromJavaScript(JavaScriptBlocks.ToString(), "item_");
			}
			catch (Exception e)
			{
				throw e;
			}

			return Nodes;
		}

		public NodeCollection GetEntityNodes()
		{
			StringBuilder MainPage = new StringBuilder();
			StringBuilder JavaScriptEntityBlock = new StringBuilder();
			NodeCollection Nodes = new NodeCollection();

			try
			{
				MainPage.Append(GetHTTPPage(@"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php"));
				JavaScriptEntityBlock.Append(GetJavaScriptEntityBlock(MainPage.ToString()));
				Nodes = GetNodesFromJavaScript(JavaScriptEntityBlock.ToString(), "entity_");
			}
			catch (Exception e)
			{
				throw e;
			}

			return Nodes;

		}


		public EVEItemCollection GetItemsFromGroupPage(string ItemGroupURL)
		{
			StringBuilder GroupPage = new StringBuilder();
			GroupPage.Append(GetHTTPPage(ItemGroupURL));

			EVEItemCollection EVEItems = new EVEItemCollection();
			
			try
			{
				EVEItems = GetItems(GroupPage.ToString());
			}
			catch (Exception e)
			{
				Exception err = new Exception("Failed to get items for this group", e);
			}

			return EVEItems;
		}

		private EVEItemCollection GetItems(string Page)
		{
			if ( !OBEXGetGroupItems.IsMatch(Page) )
				return null;

			EVEItemCollection Items = new EVEItemCollection();

			foreach ( Match m in OBEXGetGroupItems.Matches(Page) )
			{
				EVEItem i = new EVEItem();

				i.Name = m.Groups["Name"].ToString();
				i.ItemID = Convert.ToInt32(m.Groups["ItemID"].ToString());

				Items.Add(i);
			}

			return Items;
		}

		private NodeCollection GetNodesFromJavaScript(string text, string IDPrefix)
		{
			if ( !OBEXGetNodes.IsMatch(text) )
			{
				throw new Exception("Could not find any nodes in supplied javascript block");
			}

			NodeCollection Nodes = new NodeCollection();

			foreach ( Match m in OBEXGetNodes.Matches(text) )
			{
				Node n = new Node(m.Groups["NodeName"].ToString(), IDPrefix + m.Groups["NodeID"].ToString(), m.Groups["URL"].ToString());

				
				if ( n.NodeName.IndexOf("&raquo;") != -1 )
					n.IsItemGroup = true;

				if ( n.NodeName.IndexOf("<span") != -1 )
				{
					if ( OBEXGetFullNodeName.IsMatch(n.NodeName) )
						n.NodeName = OBEXGetFullNodeName.Match(n.NodeName).Groups["FullNodeName"].ToString();
				}

				n.NodeName = n.NodeName.Replace("&nbsp;", "");
				n.NodeName = n.NodeName.Replace("&raquo;", "");

				n.LastUpdated = DateTime.Now;

				Nodes.Add(n);
			}

			return Nodes;
		}

		private string GetJavaScriptItemBlock(string html)
		{
			if ( !OBEXGetItemsJavascript.IsMatch(html) )
				return null;

			StringBuilder result = new StringBuilder();

			foreach ( Match m in OBEXGetItemsJavascript.Matches(html) )
			{
				result.Append(m.Groups["Javascript"].ToString());
			}

			return result.ToString();
		}

		private string GetJavaScriptEntityBlock(string html)
		{
			if ( !OBEXGetEntitiesJavascript.IsMatch(html) )
				return null;

			StringBuilder result = new StringBuilder();

			foreach ( Match m in OBEXGetEntitiesJavascript.Matches(html) )
			{
				result.Append(m.Groups["Javascript"].ToString());
			}

			return result.ToString();
		}


		private void GetItemInfoFromHTML(string html, EVEItem Item)
		{
			if ( !OEGetDetailPageSummary.IsMatch(html) )
				return;

			try
			{
				Match m					= OEGetDetailPageSummary.Match(html);

				Item.Name			= m.Groups["ItemName"].Value;
				Item.Description	= m.Groups["ItemDesc"].Value;
				Item.ImageURL		= m.Groups["ImgName"].Value;
				Item.Path			= m.Groups["Path"].Value;
				Item.LastUpdated	= DateTime.Now;
			}
			catch (Exception err)
			{
				Debug.WriteLine("Error parsing results: " + err.ToString());
			}

		}
		
		private void GetItemInfoFromHTML(string html)
		{
			if ( !OEGetDetailPageSummary.IsMatch(html) )
				return;

			try
			{
				Match m					= OEGetDetailPageSummary.Match(html);

				this.Item.Name			= m.Groups["ItemName"].Value;
				this.Item.Description	= m.Groups["ItemDesc"].Value;
				this.Item.ImageURL		= m.Groups["ImgName"].Value;
				this.Item.Path			= m.Groups["Path"].Value;
			}
			catch (Exception err)
			{
				Debug.WriteLine("Error parsing results: " + err.ToString());
			}
		}

		public SearchResult[] GetSearchResults(string SearchString)
		{
			StringBuilder resultHTML = new StringBuilder();
	
			try
			{
				resultHTML.Append(GetHTTPPage(ItemSearchURL + SearchString));
			}
			catch (Exception err)
			{
				throw new Exception("Unable to retrieve search results page. Aborting.", err);
			}

			SearchResult[] FoundItems = GetSearchResultItems(resultHTML.ToString());

			return FoundItems;
		}

		public SearchResult[] GetLinksOnPage(string url)
		{
			StringBuilder html = new StringBuilder();
			SearchResult[] Results = new SearchResult[1];

			try
			{
				html.Append(GetHTTPPage(url));
				Results = GetSearchResultItems(html.ToString());
			}
			catch (Exception Ex)
			{
				throw new Exception("Could not extract links from page: " + url + ". " + Ex.Message);
			}

			return Results;
		}

		private SearchResult[] GetSearchResultItems(string html)
		{
			if ( !OEGetSearchResultItems.IsMatch(html) )
				throw new Exception("Could not find any items in the search results page");

			SearchResult[] Results = new SearchResult[1];

			try
			{
				Results = new SearchResult[OEGetSearchResultItems.Matches(html).Count];
			}
			catch (Exception err)
			{
				throw err;
			}

			int i = 0;
			foreach ( Match m in OEGetSearchResultItems.Matches(html) )
			{
				SearchResult Result = new SearchResult();

				Result.ItemID = Convert.ToInt32(m.Groups["ItemID"].Value);
				Result.ItemName = m.Groups["ItemName"].Value;

				Results[i] = Result;
				i++;
			}

			return Results;
		}

		private string GetHTTPPage(string SiteURL)
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
				loHttp.Timeout = 20000; // Only wait 5 seconds for a response
		
				loHttp.UserAgent	= UserAgentText;
				loHttp.Method		= "GET";
				loHttp.ContentType	= "text/html";

				loHttp.Proxy = CombatLog.MyWebProxy.WebProxyFactory.CreateProxy(loHttp, MyConfig);

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


		private void GetAttributesFromHTML(string html, EVEItem Item)
		{
			if ( !OEGetItemAttributeHeadings.IsMatch(html) )
			{
				Debug.WriteLine("This does not look like an item info page, cannot find any attribute headings. Aborting...");
				return;
			}

			AttributeHeading[] Headings = new AttributeHeading[OEGetItemAttributeHeadings.Matches(html).Count];

			int i = 0;
			foreach ( Match m in OEGetItemAttributeHeadings.Matches(html) )
			{
				Headings[i].HeadingName = m.Groups["Heading"].Value;
				Headings[i].PositionInFile = m.Index;
				i++;
			}

			if ( !OEGetItemAttributes.IsMatch(html) )
			{
				Debug.WriteLine("Cannot find any attributes in the item info page. Aborting...");
				return;
			}

			ItemAttributeCollection Items = new ItemAttributeCollection();

			foreach ( Match m in OEGetItemAttributes.Matches(html) )
			{
				ItemAttribute attr = new ItemAttribute();

				attr.Name = m.Groups["AttrName"].Value;
				attr.Description = m.Groups["AttrTitle"].Value;
				attr.IconURL = m.Groups["AttrImg"].Value;
				attr.Value = m.Groups["AttrValue"].Value;

				attr.GroupName = GetGroupName(m.Index, Headings);

				Items.Add(attr);
			}

			Item.Attributes = Items;

			foreach ( ItemAttribute ia in Item.Attributes )
			{
				Debug.WriteLine("[ " + ia.GroupName + " ] " + ia.Name + " : " + ia.Value);
			}
		}

		private void GetAttributesFromHTML(string html)
		{
			if ( !OEGetItemAttributeHeadings.IsMatch(html) )
			{
				Debug.WriteLine("This does not look like an item info page, cannot find any attribute headings. Aborting...");
				return;
			}

			AttributeHeading[] Headings = new AttributeHeading[OEGetItemAttributeHeadings.Matches(html).Count];

			int i = 0;
			foreach ( Match m in OEGetItemAttributeHeadings.Matches(html) )
			{
				Headings[i].HeadingName = m.Groups["Heading"].Value;
				Headings[i].PositionInFile = m.Index;
				i++;
			}

			if ( !OEGetItemAttributes.IsMatch(html) )
			{
				Debug.WriteLine("Cannot find any attributes in the item info page. Aborting...");
				return;
			}

			ItemAttributeCollection Items = new ItemAttributeCollection();

			foreach ( Match m in OEGetItemAttributes.Matches(html) )
			{
				ItemAttribute attr = new ItemAttribute();

				attr.Name = m.Groups["AttrName"].Value;
				attr.Description = m.Groups["AttrTitle"].Value;
				attr.IconURL = m.Groups["AttrImg"].Value;
				attr.Value = m.Groups["AttrValue"].Value;

				attr.GroupName = GetGroupName(m.Index, Headings);

				Items.Add(attr);
			}

			Item.Attributes = Items;

			foreach ( ItemAttribute ia in Item.Attributes )
			{
				Debug.WriteLine("[ " + ia.GroupName + " ] " + ia.Name + " : " + ia.Value);
			}
		}

		private string GetGroupName(int AttrPositionInFile, AttributeHeading[] Headings)
		{
			for ( int i = 0; i < Headings.Length; i++ )
			{
				if ( i < Headings.Length - 1 )
				{
					if ( AttrPositionInFile > Headings[i].PositionInFile && AttrPositionInFile < Headings[i+1].PositionInFile )
						return Headings[i].HeadingName;
				}
				else
				{
					return Headings[i].HeadingName;
				}
			}

			return "Unknown";
		}

		public EVEItem Item
		{
			get { return this._Item; }
			set { this._Item = value; }
		}
	}
}
