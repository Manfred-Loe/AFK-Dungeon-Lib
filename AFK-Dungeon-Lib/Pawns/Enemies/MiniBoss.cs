
using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class MiniBoss : Enemy
{
	public int SizeX { get; set; }
	public int SizeY { get; set; }

	public MiniBoss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos, TargetPriority priority, bool healer, SpellModifier modifier) :
		this(name, level, phys, range, alternateDmg, pos, 25, 0.1f, 0.5f, 1.0f, 250, 20, 20, 10, 10, 10, 10, 10, 10, 10, 2, 2, priority, healer, modifier)
	{
	}
	public MiniBoss() : this("Basic Enemy", 1, true, false, false, new(0, 0), TargetPriority.Closest, false, SpellModifier.None) { }

	public MiniBoss(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos,
				int baseDamage, float baseCritChance, float baseCritDamage, float attackSpeed,
				int baseHealth, int baseDefense, int baseResist,
				int strength, int dexterity, int vitality, int intelligence, int wisdom, int fortitude, int will, int sizeX, int sizeY, TargetPriority priority, bool healer, SpellModifier modifier) :
				base(name, level, phys, range, alternateDmg, pos, baseDamage, baseCritChance, baseCritDamage, attackSpeed, baseHealth, baseDefense, baseResist,
				strength, dexterity, vitality, intelligence, wisdom, fortitude, will, priority, healer, modifier)
	{
		this.SizeX = sizeX;
		this.SizeY = sizeY;
	}
}
