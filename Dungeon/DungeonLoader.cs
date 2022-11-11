using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Dungeon.DungeonFactories;

namespace AFK_Dungeon_Lib.Dungeon;
public class DungeonLoader
{
	public int InitialZoneLevel { get; private set; }
	public int CurrentLevel { get; private set; }
	public int Scaling { get; private set; }
	public List<Zone> Zones { get; private set; }

	public DungeonLoader(int initialZoneLevel, int scaling) : this(initialZoneLevel, initialZoneLevel, scaling) { }

	//default - no custom scaling
	public DungeonLoader() : this(1, 1, 1) { }

	public DungeonLoader(int initialZoneLevel, int currentZoneLevel, int scaling)
	{
		InitialZoneLevel = initialZoneLevel;
		CurrentLevel = currentZoneLevel;
		Scaling = scaling;
		Zones = new List<Zone>
		{
			ZoneFactory.GenerateZone(Scaling, currentZoneLevel)
		};
	}

	public Zone GetCurrentZone() { return Zones[0]; }

	public Zone GetNextZone()
	{
		CurrentLevel++;
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
		Zones.Add(ZoneFactory.GenerateZone(Scaling, currentZoneLevel + 1));
	}
}
