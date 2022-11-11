using AFK_Dungeon_Lib.Pawns;
namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;
public interface IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public IPawn? Target { get; set; }
	public void NextStep();
	public void GetTarget(List<IPawn> targets);
}
