using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class EnemyStat
{
	public Stat<int> Strength;
	public Stat<int> Dexterity;
	public Stat<int> Vitality;
	public Stat<int> Intelligence;
	public Stat<int> Wisdom;
	public Stat<int> Fortitude;
	public Stat<int> Will;

	public Stat<int> Health;
	public Stat<int> Defense;
	public Stat<int> Resistance;
	public Stat<int> Damage;
	public Stat<float> CritChance;
	public Stat<float> CritDamage;
	public Stat<float> AttackSpeed;
}
