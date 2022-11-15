using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFK_Dungeon_Lib.AI;

namespace AFK_Dungeon_Lib.Pawns;

public interface IPawn
{
	public Coordinate Position { get; }
	public string Name { get; }
	public int Level { get; }
	public void IncrementLevel(int level);
	public void ChangeName(string newName);
	public void SetPosition(Coordinate c);
	public int GetAbilityScore(StatsEnum ability, StatStateEnum state);
	public TargetPriority Priority { get; set; }
}
