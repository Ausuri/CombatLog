using System;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EVEAttributeTypeHelper
{
	public enum ValueType {String, Integer, Float, Double, URL, Unknown};

	public class EVEItemType
	{
		private string _DisplayString;
		private string _DisplayStringSuffix;
		private double _DoubleValue;
		private string _StringValue;
		private int _IntVal;
		private string _URLHref;
		private ValueType _Type;

		#region Regular Expressions

		#endregion

		public EVEItemType()
		{
		}
		#region public Properties
		public string DisplayString
		{
			get { return this._DisplayString; }
			set { this._DisplayString = value; }
		}

		public string StringValue
		{
			get { return this._StringValue; }
			set { this._StringValue = value; }
		}

		public string DisplayStringSuffix
		{
			get { return this._DisplayStringSuffix; }
			set { this._DisplayStringSuffix = value; }
		}

		public double DoubleValue
		{
			get { return this._DoubleValue; }
			set { this._DoubleValue = value; }
		}

		public string URLHref
		{
			get { return this._URLHref; }
			set { this._URLHref = value; }
		}

		public ValueType Type
		{
			get { return this._Type; }
			set { this._Type = value; }
		}

		#endregion

		public int IntVal
		{
			get { return this._IntVal; }
			set { this._IntVal = value; }
		}
	}

	public class TypeHelper
	{
		public TypeHelper()
		{
		}

		#region Regular Expressions

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Wed, Sep 22, 2004, 03:31:04 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  [Result]: A named capture group. [[a-zA-Z]*]
		///      Any character in this class: [a-zA-Z], any number of repetitions
		///  
		///  
		/// </summary>
		private static Regex RXContainsAlphaChars = new Regex(
			@"(?<Result>[a-zA-Z]*)",
			RegexOptions.Singleline
			| RegexOptions.ExplicitCapture
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Wed, Sep 22, 2004, 03:49:09 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  Match if suffix is absent. [[a-zA-Z]]
		///      Any character in this class: [a-zA-Z]
		///  Any character in this class: [0-9.], any number of repetitions
		///  Any digit, any number of repetitions
		///  
		///  
		/// </summary>
		private static Regex RXFindNums = new Regex(
			@"(?<![a-zA-Z])[0-9.]*\d*",
			RegexOptions.IgnoreCase
			| RegexOptions.IgnorePatternWhitespace
			);


		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Fri, Sep 17, 2004, 02:53:37 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  href="
		///  [Url]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  ">
		///  [Title]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  </a>
		///  [Level]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  End of line or string
		///  
		///  
		/// </summary>
		private static Regex RXParseURL = new Regex(
			@"href=""(?<Url>.*?)"">(?<Title>.*?)</a> (?<Level>.*?)$",
			RegexOptions.IgnoreCase
			| RegexOptions.ExplicitCapture
			| RegexOptions.Compiled
			);

		#endregion

		public static EVEItemType EmptyAttribute(ValueType AttrType)
		{
			EVEItemType item = new EVEItemType();
			switch ( AttrType )
			{
				case ValueType.Double:
					item.Type = ValueType.Double;
					item.DisplayString = "N/A";
					item.DoubleValue = -1;
					break;

				case ValueType.String:
					item.Type = ValueType.String;
					item.DisplayString = "N/A";
					item.StringValue = "";
					break;

				case ValueType.URL:
					item.Type = ValueType.URL;
					item.DisplayString = "N/A";
					item.URLHref = "";
					break;
			}

			return item;
		}

		public static ValueType GetAttrType(string AttrName)
		{
			switch ( AttrName.Trim().ToLower() )
			{
					#region Turret attributes
				case "damage modifier":
					return ValueType.Double;
					
				case "rate of fire":
					return ValueType.Double;
					
				case "normalized damage modifier":
					return ValueType.Double;
					
				case "optimal range":
					return ValueType.Double;

				case "accuracy falloff":
					return ValueType.Double;

				case "trackingspeed / accuracy":
					return ValueType.Double;

				case "activation cost":
					return ValueType.Double;

				case "energy use per second":
					return ValueType.Double;

				case "charge size":
					return ValueType.String;

				case "used with (chargegroup)":
					return ValueType.String;

				case "mass":
					return ValueType.Double;

				case "hp":
					return ValueType.Double;

				case "capacity":
					return ValueType.Double;

				case "volume":
					return ValueType.Double;

				case "signature resolution":
					return ValueType.Double;

				case "powergrid usage":
					return ValueType.Integer;

				case "cpu usage":
					return ValueType.Integer;

				case "primary skill required":
					return ValueType.URL;

				case "secondary skill required":
					return ValueType.URL;

				case "tertiary skill required":
					return ValueType.URL;

				case "tech level":
					return ValueType.Integer;

				case "base price":
					return ValueType.Double;

					#endregion

					#region Drone Attributes
				case "base shield damage":
					return ValueType.Double;

				case "base armor damage":
					return ValueType.Double;

				case "em damage":
					return ValueType.Double;

				case "explosive damage":
					return ValueType.Double;

				case "kinetic damage":
					return ValueType.Double;

				case "activation proximity":
					return ValueType.Double;

				case "incapacitation ratio":
					return ValueType.Double;

				case "max velocity":
					return ValueType.Double;

				case "agility":
					return ValueType.Double;

				case "armor hp":
					return ValueType.Double;

				case "armor em damage resistance":
					return ValueType.Double;

				case "armor explosive damage resistance":
					return ValueType.Double;

				case "armor kinetic damage resistance":
					return ValueType.Double;

				case "armor thermal damage resistance":
					return ValueType.Double;

				case "shield capacity":
					return ValueType.Double;

				case "shield em damage resistance":
					return ValueType.Double;

				case "shield explosive damage resistance":
					return ValueType.Double;

				case "shield kinetic damage resistance":
					return ValueType.Double;
				
				case "shield thermal damage resistance":
					return ValueType.Double;

				case "shield recharge time":
					return ValueType.Double;

				case "kinetic dmg resistance":
					return ValueType.Double;

				case "thermal dmg resistance":
					return ValueType.Double;

				case "explosive dmg resistance":
					return ValueType.Double;

				case "em dmg resistance":
					return ValueType.Double;

				case "recharge time":
					return ValueType.Double;

				case "capacitor capacity":
					return ValueType.Double;

				case "targeting speed":
					return ValueType.Double;

				case "max locked targets":
					return ValueType.Double;

				case "radar sensor strength":
					return ValueType.Double;

				case "ladar sensor strength":
					return ValueType.Double;

				case "magnetometric sensor strength":
					return ValueType.Double;

				case "gravimetric sensor strength":
					return ValueType.Double;

				case "signature radius":
					return ValueType.Double;

					#endregion

					#region Missile attributes
				case "explosion radius":
					return ValueType.Double;

				case "max flight time":
					return ValueType.Double;

				case "detonation proximity":
					return ValueType.Double;

				case "used with (launchergroup)":
					return ValueType.String;

				case "rate of fire bonus":
					return ValueType.Double;

				case "portion size":
					return ValueType.Double;
					#endregion

					#region SmartBomb Attributes

				case "activation time / duration":
					return ValueType.Double;

				case "area of effect":
					return ValueType.Double;

				case "thermal damage":
					return ValueType.Double;

					#endregion

				default:
					// Debug.WriteLine("Unknown attribute name: " + AttrName);
					return ValueType.Unknown;
			}
		}

		private static EVEItemType GetDoubleType(string AttrValue)
		{
			System.IFormatProvider format =
				new System.Globalization.CultureInfo("en-GB", true);

			EVEItemType e = new EVEItemType();
			e.Type = ValueType.Double;

			string[] parts = AttrValue.Split(' ');

			if ( RXFindNums.IsMatch(AttrValue) )
			{
				MatchCollection numbers = RXFindNums.Matches(AttrValue);
				StringBuilder Num = new StringBuilder();

				foreach ( Match m in numbers )
					Num.Append(m.Groups[0].Value);

				e.DoubleValue = Convert.ToDouble(Num.ToString(), format);
			}

			e.DisplayString = AttrValue;
			e.DisplayStringSuffix = "";

			return e;
		}

		private static EVEItemType GetStringType(string AttrValue)
		{
			EVEItemType e = new EVEItemType();
			e.Type = ValueType.String;
			e.DisplayString = AttrValue;
			e.DisplayStringSuffix = "";
			e.StringValue = AttrValue;

			return e;
		}

		private static EVEItemType GetURLType(string AttrValue)
		{
			EVEItemType e = new EVEItemType();
			e.Type = ValueType.URL;

			if ( !RXParseURL.IsMatch(AttrValue) )
			{
				e.DisplayString = "Unknown";
				return e;
			}

			Match m = RXParseURL.Match(AttrValue);

			e.DisplayString = m.Groups["Title"].Value.ToString() + " " + m.Groups["Level"].Value.ToString();
			e.URLHref = m.Groups["Url"].Value;

			e.DisplayStringSuffix = "";

			return e;
		}

		private static EVEItemType GetIntType(string AttrValue)
		{
			string[] parts;
			EVEItemType e = new EVEItemType();
			e.Type = ValueType.Integer;

			if ( AttrValue.IndexOf(" ") == -1 )
			{
				e.DisplayString = AttrValue;
				e.DisplayStringSuffix = "";
				e.IntVal = Convert.ToInt32(AttrValue);
				return e;
			}

			parts = AttrValue.Split(' ');

			if ( parts.Length > 2 )
			{
				e.DisplayStringSuffix = parts[parts.Length-1];

				string NumVal = "";
				for ( int i = 0; i < parts.Length - 1; i++)
				{
					NumVal += parts[i];
					e.DisplayString += parts[i] + " ";
				}

				e.IntVal = Convert.ToInt32(NumVal);
			}
			else
			{
				e.DisplayString = parts[0];
				e.DisplayStringSuffix = parts[1];
				e.IntVal = Convert.ToInt32(parts[0]);
			}

			return e;
		}

		public static EVEItemType GetTypeObject(string AttrName, string AttrValue)
		{
			EVEItemType e = new EVEItemType();

			switch ( AttrName.Trim().ToLower() )
			{
					#region Turret Attributes
				case "damage modifier":
					return GetDoubleType(AttrValue);

				case "rate of fire":
					return GetDoubleType(AttrValue);
					
				case "normalized damage modifier":
					return GetDoubleType(AttrValue);
					
				case "optimal range":
					return GetDoubleType(AttrValue);

				case "accuracy falloff":
					return GetDoubleType(AttrValue);

				case "trackingspeed / accuracy":
					return GetDoubleType(AttrValue);

				case "activation cost":
					return GetDoubleType(AttrValue);

				case "energy use per second":
					return GetDoubleType(AttrValue);

				case "charge size":
					return GetStringType(AttrValue);

				case "used with (chargegroup)":
					return GetStringType(AttrValue);

				case "mass":
					return GetDoubleType(AttrValue);

				case "hp":
					return GetDoubleType(AttrValue);

				case "capacity":
					return GetDoubleType(AttrValue);

				case "volume":
					return GetDoubleType(AttrValue);

				case "signature resolution":
					return GetDoubleType(AttrValue);

				case "powergrid usage":
					return GetDoubleType(AttrValue);

				case "cpu usage":
					return GetDoubleType(AttrValue);

				case "primary skill required":
					return GetURLType(AttrValue);

				case "secondary skill required":
					return GetURLType(AttrValue);

				case "tertiary skill required":
					return GetURLType(AttrValue);

				case "tech level":
					return GetDoubleType(AttrValue);

				case "base price":
					return GetDoubleType(AttrValue);
					#endregion

					#region Drone attributes
				case "base shield damage":
					return GetDoubleType(AttrValue);

				case "base armor damage":
					return GetDoubleType(AttrValue);

				case "em damage":
					return GetDoubleType(AttrValue);

				case "explosive damage":
					return GetDoubleType(AttrValue);

				case "kinetic damage":
					return GetDoubleType(AttrValue);

				case "activation proximity":
					return GetDoubleType(AttrValue);

				case "incapacitation ratio":
					return GetDoubleType(AttrValue);

				case "max velocity":
					return GetDoubleType(AttrValue);

				case "agility":
					return GetDoubleType(AttrValue);

				case "armor hp":
					return GetDoubleType(AttrValue);

				case "armor em damage resistance":
					return GetDoubleType(AttrValue);

				case "armor explosive damage resistance":
					return GetDoubleType(AttrValue);

				case "armor kinetic damage resistance":
					return GetDoubleType(AttrValue);

				case "armor thermal damage resistance":
					return GetDoubleType(AttrValue);

				case "shield capacity":
					return GetDoubleType(AttrValue);

				case "shield em damage resistance":
					return GetDoubleType(AttrValue);

				case "shield explosive damage resistance":
					return GetDoubleType(AttrValue);

				case "shield kinetic damage resistance":
					return GetDoubleType(AttrValue);
				
				case "shield thermal damage resistance":
					return GetDoubleType(AttrValue);

				case "shield recharge time":
					return GetDoubleType(AttrValue);

				case "kinetic dmg resistance":
					return GetDoubleType(AttrValue);

				case "thermal dmg resistance":
					return GetDoubleType(AttrValue);

				case "explosive dmg resistance":
					return GetDoubleType(AttrValue);

				case "em dmg resistance":
					return GetDoubleType(AttrValue);

				case "recharge time":
					return GetDoubleType(AttrValue);

				case "capacitor capacity":
					return GetDoubleType(AttrValue);

				case "targeting speed":
					return GetDoubleType(AttrValue);

				case "max locked targets":
					return GetDoubleType(AttrValue);

				case "radar sensor strength":
					return GetDoubleType(AttrValue);

				case "ladar sensor strength":
					return GetDoubleType(AttrValue);

				case "magnetometric sensor strength":
					return GetDoubleType(AttrValue);

				case "gravimetric sensor strength":
					return GetDoubleType(AttrValue);

				case "signature radius":
					return GetDoubleType(AttrValue);

					#endregion

					#region Missile Attributes

				case "explosion radius":
					return GetDoubleType(AttrValue);

				case "max flight time":
					e = GetDoubleType(AttrValue);
					e.DoubleValue = e.DoubleValue / 1000;
					return e;

				case "detonation proximity":
					return GetDoubleType(AttrValue);

				case "used with (launchergroup)":
					return GetStringType(AttrValue);

				case "rate of fire bonus":
					return GetDoubleType(AttrValue);

				case "portion size":
					return GetDoubleType(AttrValue);

					#endregion

					#region SmartBomb Attributes

				case "activation time / duration":
					return GetDoubleType(AttrValue);

				case "area of effect":
					return GetDoubleType(AttrValue);

				case "thermal damage":
					return GetDoubleType(AttrValue);

					#endregion

				default:
					// Debug.WriteLine("Unknown attribute name: " + AttrName);
					e.Type = ValueType.Unknown;
					e.DisplayString = "Unknown ??";
					e.DisplayStringSuffix = "??";
					return e;
			}
		}
	}
}
