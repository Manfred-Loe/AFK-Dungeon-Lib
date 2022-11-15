using AFK_Dungeon_Lib.Dungeon.DungeonObjects;

namespace AFK_Dungeon_Lib.Controllers
{
	public interface IEntityController
	{
		public void AdvanceTurn(IPawnEntity pawn);
	}
}