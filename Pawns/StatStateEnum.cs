namespace AFK_Dungeon_Lib.Pawns;

public enum StatStateEnum
{
	Initial = 0,
	Bonus = 1,
	Final = 2,
	Current = 3
}
static class StatsStateExtension
{
	public static string ToString(this StatStateEnum type)
	{
		return type switch
		{
			StatStateEnum.Initial => "Initial",
			StatStateEnum.Bonus => "Bonus",
			StatStateEnum.Final => "Final",
			StatStateEnum.Current => "Modified",

			_ => "Error Incorrect value",
		};
	}
}