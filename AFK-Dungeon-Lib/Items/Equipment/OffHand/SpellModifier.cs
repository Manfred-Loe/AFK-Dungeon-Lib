namespace AFK_Dungeon_Lib.Items.Equipment.Offhand;
public enum SpellModifier
{
	None = 0,
	Cube = 1,
	Line = 2,
	Point = 3
}

static class SpellModifierExtension
{
	public static string ToString(this SpellModifier sm)
	{
		return sm switch
		{
			SpellModifier.None => "None",
			SpellModifier.Cube => "Cube",
			SpellModifier.Line => "Line",
			SpellModifier.Point => "Point",
			_ => "Error Incorrect value",
		};
	}
}
