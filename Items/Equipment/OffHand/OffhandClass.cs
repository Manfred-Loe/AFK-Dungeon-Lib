namespace AFK_Dungeon_Lib.Items.Equipment.Offhand;

public enum OffhandClass
{
	Shield = 0,
	Tome = 1
}

static class OffhandClassExtension
{
	public static string ToString(this OffhandClass type)
	{
		return type switch
		{
			OffhandClass.Shield => "Shield",
			OffhandClass.Tome => "Tome",
			_ => "Error Incorrect value",
		};
	}
}