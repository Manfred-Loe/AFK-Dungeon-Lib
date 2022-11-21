using AFK_Dungeon_Lib.Dungeon;

namespace AFK_Dungeon_Lib.API;

public class DungeonAPI
{
	internal DungeonDriver driver;
	public DungeonState State;

	internal DungeonAPI(DungeonDriver driver)
	{
		this.driver = driver;
		this.State = driver.DungeonState;
	}
}
