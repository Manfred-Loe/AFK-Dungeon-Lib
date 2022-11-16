using AFK_Dungeon_Lib.Dungeon;

namespace AFK_Dungeon_Lib.API;

public class DungeonAPI
{
	readonly DungeonDriver driver;
	internal DungeonAPI(DungeonDriver driver)
	{
		this.driver = driver;
	}

	public void NextStep()
	{
		driver.NextStep();
	}

}
