using AFK_Dungeon_Lib.Account;
using AFK_Dungeon_Lib.IOC;

namespace AFK_Dungeon_Lib.API;
public class AFKDungeon
{
	public UserAccount user;
	public GameConfig config;
	public GameContainer gc;
	public DungeonAPI dungeon;
	public GameAPI game;

	public AFKDungeon(/*account loading parameters*/)
	{
		user = new();
		config = new(1, 1, 1, 1, 1, null, null);
		gc = new(user, config);
		dungeon = gc.container.GetInstance<DungeonAPI>();
		game = gc.container.GetInstance<GameAPI>();
	}

}