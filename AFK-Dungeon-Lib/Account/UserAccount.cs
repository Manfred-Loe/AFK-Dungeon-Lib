using AFK_Dungeon_Lib.IO;
using AFK_Dungeon_Lib.Items;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Account;
public class UserAccount
{
	public string UserName { get; set; }
	public List<Hero> HeroList { get; }
	public Inventory UserInventory { get; }
	//personal settings
	//persistence

	public UserAccount()
	{
		UserName = "New Player";
		HeroList = new();
		UserInventory = new Inventory();

		TempUsers();
	}
	public UserAccount(string fileLocation)
	{
		HeroList = new List<Hero>();
		UserInventory = new Inventory();
		string serializedUser;

		if (File.Exists(fileLocation))
		{
			serializedUser = Serialize.SerializeFromFile(fileLocation);
			UserAccount? temp = JsonConvert.DeserializeObject<UserAccount>(serializedUser);
			if (temp == null)
			{
				throw new InvalidOperationException("Account File in Incorrect State");
			}
			UserName = temp.UserName;
			HeroList.AddRange(temp.HeroList);
			UserInventory = temp.UserInventory;
		}
		else
		{
			UserName = "New Player";
			HeroList = new List<Hero>();
			UserInventory = new Inventory();

			TempUsers();
		}
	}

	public void SaveUser()
	{
		Serialize.SerializeToFile(this, this.UserName);
	}
	private void TempUsers()
	{
		HeroList.Add(new Hero("Hero 1"));
		HeroList.Add(new Hero("Hero 2"));
		HeroList.Add(new Hero("Hero 3"));
		HeroList.Add(new Hero("Hero 4"));
		HeroList[0].SetPosition(new(1, 0));
		HeroList[1].SetPosition(new(1, 1));
		HeroList[2].SetPosition(new(1, 2));
		HeroList[3].SetPosition(new(1, 3));
	}
	public List<Hero> GetHeroes() { return HeroList; }
	public List<Hero> CloneHeroes()
	{
		var clones = new List<Hero>();
		foreach (var hero in HeroList)
		{
			var h = Clone.CloneObject<Hero>(hero);
			if (h == null) { throw new NullReferenceException(); }
			clones.Add(h);
		}
		return clones;
	}
	public Hero GetHero(int index) { return HeroList[index]; }
	//fetcher for settings

}
