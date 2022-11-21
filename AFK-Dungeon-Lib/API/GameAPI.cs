using AFK_Dungeon_Lib.Items;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.API;

public class GameAPI
{
	internal GameDriver Driver;

	internal GameAPI(GameDriver driver)
	{
		this.Driver = driver;
	}

	public Hero GetHero(int n)
	{
		return Driver.User.GetHero(n);
	}

	public List<Hero> GetHeroes()
	{
		return Driver.User.GetHeroes();
	}

	public Inventory GetInventory()
	{
		return Driver.User.UserInventory;
	}
}
