using System;
using System.IO;
using System.Xml.Serialization;
using ObjectExplorer;
using System.Collections;

namespace OEParse
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// ShowArgs(args);

			if ( args.Length == 0 )
			{
				ShowUsage();
				return;
			}

			string ObjectIDStr = args[0];
			int ObjectID;

			switch (args[0].ToLower())
			{
				case "-nodes":
					GetNodeData(null);
					break;

				case "-appendnodes":
					Append(args[1]);
					break;

				case "-trim":

					if ( args.Length != 5 )
					{
						ShowTrimUsage();
						break;
					}

					TrimObExDataFile(args[1], args[2], args[3], args[4]);
					break;
				default:
					try
					{
						ObjectID = Convert.ToInt32(ObjectIDStr);
						ParsePage(ObjectID);
						return;
					}
					catch // Probably a string, do a search
					{
						DoObjectSearch(args[0]);
						return;
					}
			}

		}

		static void ShowTrimUsage()
		{
			Console.WriteLine("\nUsage:\n\nOEParser -trim [infile] [outfile] [Version (Text)] [Version ID (Int)]\n");
		}

		static void TrimObExDataFile(string inFile, string outFile, string VersionStr, string VersionInt)
		{
			NodeCollection Nodes = LoadNodesFile(inFile);
			Console.WriteLine("Loaded {0} nodes", Nodes.Count);

			if ( Nodes == null )
			{
				Console.WriteLine("No nodes. Exiting");
				return;
			}

			OBExNodes TrimmedNodes = TrimNodes(Nodes);
			TrimmedNodes.VersionStr = VersionStr;
			TrimmedNodes.VersionID = Convert.ToInt32(VersionInt);
			TrimmedNodes.LastUpdated = DateTime.Now;

			SaveObExNodes(TrimmedNodes, outFile);
		}

		static OBExNodes TrimNodes(NodeCollection inNodes)
		{
			Hashtable UniqueAttributes = new Hashtable();

			// Sort out the imageURL i.e. remove path prefix
			//
			// Convert &raquo; to ">" in EVE Item Path property

			foreach ( Node n in inNodes )
			{
				Console.Write(".");

				if ( n.ItemsInGroup != null )
				{
					foreach ( EVEItem ei in n.ItemsInGroup )
					{
						if ( ei.ImageURL != null )
							ei.ImageURL = Path.GetFileName(ei.ImageURL);

						if ( ei.Path != null )
							ei.Path = ei.Path.Replace("&raquo;", " > ");

						if ( ei.Attributes != null )
						{
							foreach ( ItemAttribute ia in ei.Attributes )
							{
								if ( !UniqueAttributes.ContainsKey(ia.Name) )
								{
									ItemAttribute attrRef = new ItemAttribute();

									attrRef.Name = ia.Name;
									attrRef.Description = ia.Description;
									attrRef.GroupName = ia.GroupName;
									attrRef.AttrType = ia.AttrType;
									attrRef.IconURL = ia.IconURL;

									UniqueAttributes.Add(ia.Name, attrRef);
								}
								else
								{
									// We have already stored this attribute
									// Check to see if the GROUP property differs

									ItemAttribute storedAttr = (ItemAttribute)UniqueAttributes[ia.Name];

									if ( storedAttr.GroupName != ia.GroupName )
									{
										Console.WriteLine("ATTRIBUTE HAS DIFFERENT GROUP: {0} - {1}", storedAttr.GroupName, ia.GroupName);
									}
								}

								ia.Description = null;
								ia.IconURL = null;
								ia.GroupName = null;

								ia.Value = ia.Value.Replace("<sup>", "");
								ia.Value = ia.Value.Replace("<small>", "");
								ia.Value = ia.Value.Replace("</sup>", "");
								ia.Value = ia.Value.Replace("</small>", "");
							}
						}
					}
				}
			}

			OBExNodes xNodes = new OBExNodes();
			xNodes.Nodes = inNodes;
			xNodes.AttributeLookup = new ItemAttributeCollection();

			Console.WriteLine("\nUnique Attributes = {0}", UniqueAttributes.Count);

			inNodes.AttributeRef = new ItemAttributeCollection();
			
			foreach ( object o in UniqueAttributes.Values )
			{
				ItemAttribute ia = (ItemAttribute)o;
				Console.Write("+");
				xNodes.AttributeLookup.Add(ia);
			}

			return xNodes;
		}

		static NodeCollection LoadNodesFile(string fileName)
		{
			if ( !File.Exists(fileName) )
			{
				Console.WriteLine("The nodes file {0} does not exist", fileName);
				return null;
			}

			NodeCollection Nodes = new NodeCollection();

			try
			{
				FileStream fs;
				fs = new FileStream(fileName, FileMode.Open);

				XmlSerializer xs = new XmlSerializer(typeof(NodeCollection));

				Nodes = (NodeCollection)xs.Deserialize(fs);

				fs.Close();

				return Nodes;

			}
			catch (Exception e)
			{
				Console.WriteLine("There was a problem loading the nodes file:\n\n{0}", e.ToString());
				return null;
			}
		}


		static void AppendNodeData(string FileName)
		{
			NodeCollection AppendNodes = LoadNodesFile(FileName);
			GetNodeData(AppendNodes);
		}

		static void Append(string FileName)
		{
			ObjectExplorer.OEParser Parser = new OEParser();
            
			NodeCollection AppendNodes = LoadNodesFile(FileName);
			
			NodeCollection WebItemNodes = Parser.GetItemNodes();
			NodeCollection WebEntityNodes = Parser.GetEntityNodes();

			foreach ( Node n in WebItemNodes )
			{
				if ( AppendNodes[n.NodeID] == null )
				{
					Console.WriteLine("Appending item node: " + n.NodeName + " [" + n.NodeID + "]");
					AppendNodes.Add(n);
				}
			}

			foreach ( Node n in WebEntityNodes )
			{
				if ( AppendNodes[n.NodeID] == null )
				{
					Console.WriteLine("Appending entity node: " + n.NodeName + " [" + n.NodeID + "]");
					AppendNodes.Add(n);
				}
			}
		}

		static void GetNodeData(NodeCollection AppendToNodes)
		{
			ObjectExplorer.OEParser Parser = new OEParser();

			NodeCollection Nodes = new NodeCollection();

			try
			{
				NodeCollection EntityNodes = Parser.GetEntityNodes();
				Nodes = Parser.GetItemNodes(); // Get a collection of the tree nodes from the ObEx javascript

				//
				// Append entity nodes to item node collection
				//
				foreach ( Node n in EntityNodes )
				{
					Node newNode = new Node(n.NodeName, n.NodeID, n.NodeURL);
					newNode.IsItemGroup = n.IsItemGroup;

					Nodes.Add(newNode);
				}
			}
			catch (Exception e)	{ Console.WriteLine(e.ToString()); }

			ShowNodeTree(Nodes);

			//
			// For each node, get a list of the group items
			//
			int i = 0;
			foreach ( Node n in Nodes )
			{
				if ( n.IsItemGroup )
				{
					try
					{
						n.ItemsInGroup = Parser.GetItemsFromGroupPage(n.NodeURL.Trim(new char[] {'\''}));

						Console.WriteLine("Items in group: {0}", n.NodeName);

						i++;
					}
					catch (Exception e) { Console.WriteLine("Error: " + e.ToString()); }
				}
				else
					Console.WriteLine(n.NodeName + " is not an itemgroup!");
			}

			try
			{
				//
				// Get data for each item contained in each node
				//

				foreach ( Node n in Nodes )
				{
					if ( n.ItemsInGroup != null )
					{
						foreach ( EVEItem ei in n.ItemsInGroup )
						{
							if ( ei.Attributes == null )
							{
								Console.WriteLine("Getting detail for '{0}'", ei.Name);
								Parser.GetItemDetail(ei.ItemID);
								Parser.GetItemDetail(ei);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error getting Item Detail: {0}", e.ToString());
			}

			SaveNodes(Nodes, "NodeFile.xml");
		}

		static void ShowNodeTree(NodeCollection Nodes)
		{
			foreach ( Node n in Nodes )
			{
				Console.WriteLine("{0}{1} - {2} [{3}]", GetPadding(n.Level), n.NodeName, n.NodeID, Nodes.NodeHasChildren(n)?"+":"-");
			}

			Console.WriteLine("Total Nodes: {0}", Nodes.Count.ToString());
		}

		static void ShowItemsInGroup(NodeCollection Nodes)
		{
//			int i = 0;
//			foreach ( Node n in Nodes )
//			{
//				if ( n.IsItemGroup )
//				{
//					try
//					{
//						n.ItemsInGroup = Parser.GetItemsFromGroupPage(n.NodeURL.Trim(new char[] {'\''}));
//
//						Console.WriteLine("Items in group: {0}", n.NodeName);
//
//						foreach ( EVEItem ei in n.ItemsInGroup )
//						{
//							Console.WriteLine("\t{0} [{1}]", ei.Name, ei.ItemID);
//						}
//
//						i++;
//
//						//						if ( i > 2 )
//						//							break;
//					}
//					catch (Exception e)
//					{
//						Console.WriteLine("Error: " + e.ToString());
//					}
//				}
//			}
		}

		static void SaveNodes(NodeCollection Nodes, string FileName)
		{
			FileStream fs;

			try
			{
				fs = new FileStream(FileName, FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(NodeCollection));

				xs.Serialize(fs, Nodes);

				fs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("\nThere was a problem saving the nodes file to {0}. The error message reported by the system was: {1}", FileName, e.ToString());
			}
		}

		static void SaveObExNodes(OBExNodes Nodes, string FileName)
		{
			FileStream fs;

			try
			{
				fs = new FileStream(FileName, FileMode.Create);

				XmlSerializer xs = new XmlSerializer(typeof(OBExNodes));

				xs.Serialize(fs, Nodes);

				fs.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("\nThere was a problem saving the nodes file to {0}. The error message reported by the system was: {1}", FileName, e.ToString());
			}
		}

		static string GetPadding(int count)
		{
			string pad = "";
			string padstring = "   ";

			for( int i = 0; i < count; i++ )
				pad += padstring;
			
			return pad;
		}

		static void ShowArgs(string[] args)
		{
			for ( int i = 0; i < args.Length; i++ )
				Console.WriteLine("Arg: " + i.ToString() + "='" + args[i] + "'");
		}

		static void DoObjectSearch(string searchString)
		{
			ObjectExplorer.OEParser Parse = new OEParser();
			ObjectExplorer.SearchResult[] Results = Parse.GetSearchResults(searchString);

			Console.WriteLine("");
			foreach ( ObjectExplorer.SearchResult r in Results )
			{
				Console.WriteLine(r.ItemName + " : " + r.ItemID.ToString());
			}
			Console.WriteLine("");

			if ( Results.Length == 1 )
				ParsePage(Results[0].ItemID);
		}

		static void ParsePage(int id)
		{
			OEParser Parse = new OEParser();

			Parse.GetItemDetail(id);
		}

		static void ShowUsage()
		{
			Console.WriteLine("\nUsage: OEParse [EVE Object ID]\n");
		}
	}
}
