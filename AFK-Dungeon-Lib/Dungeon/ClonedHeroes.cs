using AFK_Dungeon_Lib.Account;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon;

public class ClonedHeroes
{
	internal readonly List<Hero> Heroes;
	public ClonedHeroes(UserAccount user)
	{
		Heroes = user.CloneHeroes();
	}
}
