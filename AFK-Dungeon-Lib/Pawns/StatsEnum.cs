using System.Collections;
using System.Collections.Generic;

namespace AFK_Dungeon_Lib.Pawns;
public enum StatsEnum
{
	Strength = 0,
	Dexterity = 1,
	Vitality = 2,
	Intelligence = 3,
	Wisdom = 4,
	Fortitude = 5,
	Will = 6,
	Defense = 7,
	Resistance = 8
}

static class StatsExtension
{
	public static string ToString(this StatsEnum type)
	{
		return type switch
		{
			StatsEnum.Strength => "Strength",
			StatsEnum.Dexterity => "Dexterity",
			StatsEnum.Vitality => "Vitality",
			StatsEnum.Intelligence => "Intelligence",
			StatsEnum.Wisdom => "Wisdom",
			StatsEnum.Fortitude => "Fortitude",
			StatsEnum.Will => "Will",
			StatsEnum.Defense => "Defense",
			StatsEnum.Resistance => "Resistance",
			_ => "Error Incorrect value",
		};
	}
}