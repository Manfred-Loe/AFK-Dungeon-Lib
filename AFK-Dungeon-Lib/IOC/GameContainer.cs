using SimpleInjector;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Items.Factories;
using AFK_Dungeon_Lib.Dungeon.DungeonFactories;
using AFK_Dungeon_Lib.API;
using AFK_Dungeon_Lib.Account;

namespace AFK_Dungeon_Lib.IOC;

public class GameContainer
{
	readonly public Container container;
	public GameContainer(UserAccount user, GameConfig config)
	{
		//create container
		container = new();

		//register game configuration and user
		container.RegisterInstance<UserAccount>(user);
		container.RegisterInstance<GameConfig>(config);
		container.RegisterInstance<ClonedHeroes>(new(user));

		//register randoms
		container.Register<GameRandom>(Lifestyle.Singleton);
		container.Register<DungeonRandom>(Lifestyle.Singleton);
		//register game driver and dungeon driver
		container.Register<GameDriver>(Lifestyle.Singleton);
		container.Register<DungeonDriver>(Lifestyle.Singleton);

		//register all factories
		//item factories
		container.Register<BootsFactory>(Lifestyle.Singleton);
		container.Register<ChestFactory>(Lifestyle.Singleton);
		container.Register<HelmetFactory>(Lifestyle.Singleton);
		container.Register<OffhandFactory>(Lifestyle.Singleton);
		container.Register<WeaponFactory>(Lifestyle.Singleton);
		container.Register<ItemFactory>(Lifestyle.Singleton);
		//zone factory
		container.Register<ZoneFactory>(Lifestyle.Singleton);
	}
}
