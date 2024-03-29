using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class Boss : Enemy
{
	public int SizeX { get; set; }
	public int SizeY { get; set; }

	public Boss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos, TargetPriority priority, bool healer, SpellModifier modifier) :
		this(name, level, phys, range, alternateDmg, pos, 50, 0.1f, 0.5f, 1.2f, 500, 50, 50, 10, 10, 10, 10, 10, 10, 10, 2, 2, priority, healer, modifier)
	{
	}
	public Boss() : this("Basic Enemy", 1, true, false, false, new(0, 0), TargetPriority.Closest, false, SpellModifier.None) { }

	public Boss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos,
				int baseDamage, float baseCritChance, float baseCritDamage, float attackSpeed,
				int baseHealth, int baseDefense, int baseResist,
				int strength, int dexterity, int vitality, int intelligence, int wisdom, int fortitude, int will, int sizeX, int sizeY, TargetPriority priority, bool healer, SpellModifier modifier) : base(name, level, phys, range, alternateDmg, pos, baseDamage, baseCritChance, baseCritDamage, attackSpeed, baseHealth, baseDefense, baseResist,
				strength, dexterity, vitality, intelligence, wisdom, fortitude, will, priority, healer, modifier)
	{
		this.SizeX = sizeX;
		this.SizeY = sizeY;
	}
}
