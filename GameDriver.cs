using AFK_Dungeon_Lib.Account;
using AFK_Dungeon_Lib.Items.Factories;
namespace AFK_Dungeon_Lib;
internal class GameDriver
{
	public UserAccount User { get; set; }

	public GameDriver()
	{
		User = new()
		{
			UserName = "Hells"
		};
		AddFunds();
	}

	private void AddFunds()
	{
		User.UserInventory.Gold += 10000;
		User.UserInventory.MetalScraps += 2000;
		User.UserInventory.HighQualityMetal += 2000;
		User.UserInventory.EtherScraps += 2000;
		User.UserInventory.HighQualityEther += 2000;
	}
}
