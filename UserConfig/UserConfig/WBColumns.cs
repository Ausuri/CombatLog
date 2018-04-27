using System;

namespace CombatLog.Config
{
	/// <summary>
	/// Records users Weapon Browser column display preferences. Referenced by the UserConfig class
	/// </summary>
	[Serializable()]
	public class WBColumns
	{
		private string _AttributeName;
		private bool _ShowAttribute;

		public WBColumns()
		{
		}

		public string AttributeName
		{
			get { return this._AttributeName; }
			set { this._AttributeName = value; }
		}

		public bool ShowAttribute
		{
			get { return this._ShowAttribute; }
			set { this._ShowAttribute = value; }
		}
	}
}
