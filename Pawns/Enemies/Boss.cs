namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class Boss : Enemy
{
	public int SizeX { get; set; }
	public int SizeY { get; set; }

	public Boss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos) :
		this(name, level, phys, range, alternateDmg, pos, 50, 0.1f, 0.5f, 1.2f, 500, 50, 50, 10, 10, 10, 10, 10, 10, 10, 2, 2)
	{
	}
	public Boss() : this("Basic Enemy", 1, true, false, false, new(0, 0)) { }

	public Boss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos,
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
