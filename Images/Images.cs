using System;
using System.Drawing;

namespace CombatLog.Images
{
	/// <summary>
	/// Summary description for Images.
	/// </summary>
	public class Images
	{
		public readonly static Image GunIcon;

		static Images()
		{
			System.Reflection.Assembly l_ExecAs = System.Reflection.Assembly.GetExecutingAssembly();
			GunIcon = System.Drawing.Image.FromStream(l_ExecAs.GetManifestResourceStream("CombatLog.Images.icon26_01.png"));
		}
	}
}
