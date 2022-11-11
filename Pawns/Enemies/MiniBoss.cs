
namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class MiniBoss : Enemy
{
	public int SizeX { get; set; }
	public int SizeY { get; set; }

	public MiniBoss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos) :
		this(name, level, phys, range, alternateDmg, pos, 25, 0.1f, 0.5f, 1.0f, 250, 20, 20, 10, 10, 10, 10, 10, 10, 10, 2, 2)
	{
	}
	public MiniBoss() : this("Basic Enemy", 1, true, false, false, new(0, 0)) { }

	public MiniBoss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos,
				int baseDamage, float baseCritChance, float baseCritDamage, float attackSpeed,
				int baseHealth, int baseDefense, int baseResist,
				int strength, int dexterity, int vitality, int intelligence, int wisdom, int fortitude, int will, int sizeX, int sizeY) :
				base(name, level, phys, range, alternateDmg, pos, baseDamage, baseCritChance, baseCritDamage, attackSpeed, baseHealth, baseDefense, baseResist,
				strength, dexterity, vitality, intelligence, wisdom, fortitude, will)
	{
		this.SizeX = sizeX;
		this.SizeY = sizeY;
	}
}
