namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;
public enum EntityState
{
	Wait,
	TakeAction,
	Dead,
	Incapacitated,
	Untargetable
}

static class EntityEnumExtension
{
	public static string ToString(this EntityState entityEnum)
	{
		return entityEnum switch
		{
			EntityState.Wait => "Wait",
			EntityState.TakeAction => "Take Action",
			EntityState.Dead => "Dead",
			EntityState.Incapacitated => "Incapacitated",
			EntityState.Untargetable => "Untargetable",
			_ => "Error Incorrect value",
		};
	}
}
