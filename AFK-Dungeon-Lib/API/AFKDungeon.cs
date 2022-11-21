using AFK_Dungeon_Lib.Account;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.IOC;
using AFK_Dungeon_Lib.Items.Factories;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.API;
public class AFKDungeon
{
	internal GameConfig config;
	internal GameContainer gc;
	public DungeonAPI dungeon;
	public GameAPI data;
	readonly public ItemFactory itemfactory;

	public AFKDungeon()
	{
		UserAccount user = new();
		config = new(GameConstants.DEFAULT_SCALING, 1, 1, 1, 1, null, null);
		gc = new(user, config);
		this.dungeon = new(gc.container.GetInstance<DungeonDriver>());
		this.data = new(gc.container.GetInstance<GameDriver>());
		this.itemfactory = gc.container.GetInstance<ItemFactory>();
	}

	public void StartDungeon()
	{
	}
}