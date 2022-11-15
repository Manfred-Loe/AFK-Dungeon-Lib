using AFK_Dungeon_Lib.Dungeon.DungeonObjects;
using AFK_Dungeon_Lib.Pawns;

namespace AFK_Dungeon_Lib.Dungeon;

public class DungeonGrid
{
	public IPawnEntity?[,] Grid;

	public DungeonGrid()
	{
		Grid = new IPawnEntity[4, 4];
	}

	public DungeonGrid(List<IPawnEntity> pawns)
	{
		Grid = new IPawnEntity[4, 4];
		foreach (var pawn in pawns)
		{
			Grid[pawn.Position.X, pawn.Position.Y] = pawn;
		}
	}

	public void RemoveEntityAt(Coordinate c)
	{
		if (Grid[c.X, c.Y] != null)
		{
			Grid[c.X, c.Y] = null;
		}
	}
}
