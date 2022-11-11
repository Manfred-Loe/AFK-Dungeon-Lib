using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Pawns.Factories;
public static class EnemyFactory
{
	public static Enemy GetMeleePhys(int level)
	{
		return new Enemy("Melee", level, true, false, false, new(0, 0));
	}
	public static Enemy GetRangePhys(int level)
	{
		return new Enemy("Ranger", level, true, true, false, new(0, 0));
	}
	public static Enemy GetRangeMage(int level)
	{
		return new Enemy("Mage", level, false, true, false, new(0, 0));
	}
	public static Enemy GetMeleeMage(int level)
	{
		return new Enemy("Melee Mage", level, false, false, false, new(0, 0));
	}

	public static Enemy GetTank(int i)
	{
		return new Enemy("Tank", i, true, false, false, new(0, 0), 5, 0.0f, 0.5f, 1f, 50, 15, 15, 15, 10, 15, 10, 10, 15, 15);
	}

	public static Boss GetBossBasic(int i, bool phys, bool ranged)
	{
		return new Boss("Tank", i, phys, ranged, false, new(0, 0));
	}

	public static MiniBoss GetMiniBossBasic(int i, bool phys, bool ranged)
	{
		return new MiniBoss("Tank", i, phys, ranged, false, new(0, 0));
	}
}
