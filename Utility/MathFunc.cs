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
	}
}