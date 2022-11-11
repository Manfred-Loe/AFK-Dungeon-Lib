using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

public class HeroEntity : IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public IPawn? Target { get; set; }

	public HeroEntity(Hero h, Coordinate position)
	{
		Entity = h;
		Position = position;
	}
	public void GetTarget(List<IPawn> targets)
	{
		throw new NotImplementedException();
	}

	public void NextStep()
	{
		throw new NotImplementedException();
	}
}
