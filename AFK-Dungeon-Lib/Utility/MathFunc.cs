using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFK_Dungeon_Lib.Utility
{
	public static class MathFunc
	{
		public static float Round(float value, int decimalPlaces)
		{
			return (float)Math.Round(value, decimalPlaces);
		}

		public static int Distance(Coordinate a, Coordinate b)
		{
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y + b.Y);
		}
	}
}