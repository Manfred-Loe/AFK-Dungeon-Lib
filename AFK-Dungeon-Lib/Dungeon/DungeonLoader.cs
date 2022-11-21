using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Dungeon.DungeonFactories;
using AFK_Dungeon_Lib.IOC;

namespace AFK_Dungeon_Lib.Dungeon;
internal class DungeonLoader
{
	public int InitialZoneLevel { get; }
	public int CurrentZoneLevel { get; private set; }
	public int Scaling { get; }
	public List<Zone> Zones { get; }
	readonly ZoneFactory zf;

	public DungeonLoader(GameConfig gc, ZoneFactory zf)
	{
		InitialZoneLevel = gc.InitialZoneLevel;
		CurrentZoneLevel = gc.CurrentZoneLevel;
		Scaling = gc.ZoneScaling;
		this.zf = zf;
		Zones = new List<Zone>
		{
			zf.GenerateZone(CurrentZoneLevel)
		};
	}

	public Zone GetCurrentZone() { return Zones[0]; }

	public Zone GetNextZone()
	{
		CurrentZoneLevel++;
		Zone z = Zones[0];
		Zones.RemoveAt(0);
		if (Zones.Count == 0)
		{
			AddNewZone(z.ZoneNumber);
		}
		return Zones[0];
	}
	public void AddNewZone(int currentZoneLevel)
	{
		Zones.Add(zf.GenerateZone(currentZoneLevel + 1));
	}
}
