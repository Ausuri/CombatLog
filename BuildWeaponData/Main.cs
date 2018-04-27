using System;
using CombatLog.WeaponDataDB;
using ObjectExplorer;
using System.IO;
using System.Xml.Serialization;

namespace BuildWeaponData
{
	/// <summary>
	/// A tool to extract Weapon Data from the EVE-I Object Explorer - a web based tool for retrieving information
	/// about objects in EVE
	/// 
	/// The EVE-I Object Explorer (OE) Provides a categorised view of EVE Objects. Clicking on a category link
	/// will result in a page containing a list of all of the objects within that category being displayed.
	/// 
	/// The purpose of this tool is to extract Weapon information from the OE. Most weapon types exist within a
	/// single category on the OE e.g. "Large Hybrid" will show all Large Hybrid weapons. The application will
	/// load the page for such a category, extract a list of all of the items on that page (the weapons) and
	/// for each of these items, extract the item detail. The results are in an XML Document which is saved to disk.
	/// 
	/// The list of categories to be extracted is defined in the Initialise() method
	/// 
	/// Here, Each "WeaponDataSource" has a number of properties which are used to segregate the weapon information.
	/// The properties are WeaponType (Laser, Projectile, Hybrid, Missile, Smartbomb, Drone) and WeaponClass (Large, 
	/// Medium, Small, Micro etc). Each WeaponDataSource also contains a URL which points to the category page on 
	/// the OE.
	/// 
	/// The WeaponDataSource class inherits from the EVEItem class which provides basic information such as Name, ID,
	/// Description and a collection of Item attributes.
	/// 
	/// The output from this tool is an XML Document of type WeaponDataSourceCollection which is a collection of
	/// WeaponDataSource objects.
	/// 
	/// Whilst most of the information recorded is extracted directly from the OE, there is some post processing
	/// used to sort out Missile and Smartbomb types as these are not correctly/appropriately categorised on the OE.
	/// 
	/// NOTE: The EVE Item fetcher has a timeout of 20 seconds and will retry up to 5 times in the event of an error
	/// (such as a http request timeout).
	/// 
	/// </summary>
	class Class1
	{
		private static WeaponDataSourceCollection Weapons = new WeaponDataSourceCollection();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if ( args.Length != 1 )
			{
				Console.WriteLine("\nUsage: BuildWeaponData [OutputFileName]\n");
				return;
			}

			string FileName = args[0];
			Initialise();
			Console.WriteLine("Processing " + Weapons.Count + " weapon types");

			WeaponDataSourceCollection WeaponData = GetWeaponData();

			SaveWeaponData(FileName, WeaponData);
		}

		private static void SaveWeaponData(string FileName, WeaponDataSourceCollection WeaponData)
		{
			FileStream f = new FileStream(FileName, FileMode.Create);

			XmlSerializer xs = new XmlSerializer(typeof(WeaponDataSourceCollection));

			xs.Serialize(f, WeaponData);

			f.Close();
		}

		private static WeaponDataSourceCollection GetWeaponData()
		{
			ObjectExplorer.OEParser Parser = new OEParser();
			WeaponDataSourceCollection WeaponData = new WeaponDataSourceCollection();
			
			int RunCount = 0;
			foreach ( WeaponDataSource w in Weapons )
			{
				ObjectExplorer.SearchResult[] Results = Parser.GetLinksOnPage(w.SourceUrl);

				Console.WriteLine(w.Class.ToString() + " " + w.Type.ToString());

				foreach ( ObjectExplorer.SearchResult sr in Results )
				{
					Console.WriteLine(sr.ItemName);

					bool FetchError = false;
					for ( int i = 0; i < 5; i++ )
					{
						try
						{
							Parser.GetItemDetail(sr.ItemID);
							FetchError = false;
							break;
						}
						catch (Exception Ex)
						{
							Console.WriteLine(Ex.Message);
							Console.WriteLine(5 - i + " retries left...");
							FetchError = true;
						}
					}

					if ( !FetchError )
					{

						WeaponDataSource wds = new WeaponDataSource();

						wds.Class		= w.Class;
						wds.Type		= w.Type;
						wds.SourceUrl	= w.SourceUrl;

						wds.ItemID		= Parser.Item.ItemID;
						wds.Name		= Parser.Item.Name;
						wds.Description = Parser.Item.Description;
						wds.Path		= Parser.Item.Path;
						wds.Attributes	= Parser.Item.Attributes;

						if ( wds.Type == CombatLog.WeaponDataDB.WeaponType.CombatDrone )
							GetDroneType(wds);
						else if ( wds.Type == CombatLog.WeaponDataDB.WeaponType.Missile )
							GetMissileType(wds);
						else if ( wds.Type == CombatLog.WeaponDataDB.WeaponType.SmartBomb )
							GetSmartBombType(wds);

						wds.Attributes = new ObjectExplorer.ItemAttributeCollection();

						foreach ( ObjectExplorer.ItemAttribute ia in Parser.Item.Attributes )
							wds.Attributes.Add(ia);

						WeaponData.Add(wds);
					}
					else
					{
						Console.WriteLine("Failed to fetch data for " + sr.ItemName + " (" + sr.ItemID.ToString() + ")");
					}

					//ShowItemDetail(wds);
				}
				RunCount++;
			}

			Console.WriteLine("\n\nTotal of " + WeaponData.Count + " collected");

			return WeaponData;
		}

		private static void GetSmartBombType(WeaponDataSource Wep)
		{
			if ( Wep.Name.IndexOf("Smartbomb") != -1 )
			{
				if ( Wep.Name.IndexOf("Large") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.Large;
				else if ( Wep.Name.IndexOf("Medium") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.Medium;
				else if ( Wep.Name.IndexOf("Small") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.Small;
				else if ( Wep.Name.IndexOf("Micro") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.Micro;
			}
		}

		private static void GetMissileType(WeaponDataSource Wep)
		{
			if ( Wep.Name.IndexOf("F.O.F.") != -1 )
			{
				if ( Wep.Name.IndexOf("Cruise") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.FOFCruiseMissile;
				else if ( Wep.Name.IndexOf("Heavy") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.FOFHeavyMissile;
				else if ( Wep.Name.IndexOf("Light") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.FOFLightMissile;
			}
			else
			{
				if ( Wep.Name.IndexOf("Torpedo") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.TorpedoMissile;
				if ( Wep.Name.IndexOf("Cruise") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.CruiseMissile;
				else if ( Wep.Name.IndexOf("Heavy") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.HeavyMissile;
				else if ( Wep.Name.IndexOf("Defender") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.DefenderMissile;
				else if ( Wep.Name.IndexOf("Light") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.LightMissile;
				else if ( Wep.Name.IndexOf("Rocket") != -1 )
					Wep.Class = CombatLog.WeaponDataDB.WeaponClass.RocketMissile;
			}
		}

		private static void GetDroneType(WeaponDataSource Wep)
		{
			if ( Wep.Description.IndexOf("Light ",0) != -1 )
				Wep.Class = CombatLog.WeaponDataDB.WeaponClass.LightDrone;
			else if ( Wep.Description.IndexOf("Medium ", 0) != -1 )
				Wep.Class = CombatLog.WeaponDataDB.WeaponClass.MediumDrone;
			else if ( Wep.Description.IndexOf("Heavy ", 0) != -1 )
				Wep.Class = CombatLog.WeaponDataDB.WeaponClass.HeavyDrone;
		}

		private static void ShowItemDetail(WeaponDataSource w)
		{
			Console.WriteLine("\t" + w.Name);
			Console.WriteLine("\n\t" + w.Description);

			foreach ( ObjectExplorer.ItemAttribute ia in w.Attributes )
			{
				Console.WriteLine("\t\t" + ia.Name + ": " + ia.Value);
			}
		}

		private static void Initialise()
		{

			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Hybrid, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=41"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Medium,	WeaponType.Hybrid, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=42"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Small,		WeaponType.Hybrid, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=43"));

			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Laser, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=51"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Medium,	WeaponType.Laser, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=50"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Small,		WeaponType.Laser, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=49"));

			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Projectile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=47"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Medium,	WeaponType.Projectile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=46"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Small,		WeaponType.Projectile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=45"));

			Weapons.Add(new WeaponDataSource(WeaponClass.LightDrone,WeaponType.CombatDrone, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=104"));

			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.SmartBomb, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=99"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Medium,	WeaponType.SmartBomb, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=101"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Small,		WeaponType.SmartBomb, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=100"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Micro,		WeaponType.SmartBomb, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=98"));

			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Missile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=69"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Missile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=70"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Missile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=71"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Missile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=72"));
			Weapons.Add(new WeaponDataSource(WeaponClass.Large,		WeaponType.Missile, @"http://www.eve-i.com/home/crowley/page/page_objectexplorer.php?id=73"));
		}
	}
}
