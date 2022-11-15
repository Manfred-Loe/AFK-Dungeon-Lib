using SimpleInjector;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Items.Factories;
using AFK_Dungeon_Lib.Dungeon.DungeonFactories;

namespace AFK_Dungeon_Lib.IOC;

public class GameContainer
{
	readonly Container container;
	public GameContainer(GameConfig config)
	{
		//create container
		container = new();

		//register game configuration
		container.RegisterInstance<GameConfig>(config);

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
