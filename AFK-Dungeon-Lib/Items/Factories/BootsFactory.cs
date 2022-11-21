using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Utility;

namespace AFK_Dungeon_Lib.Items.Factories;
public class BootsFactory
{
	readonly GameRandom random;
	/*boots are always base vitality, however there are 3 types
    1) Phys: Vit, Dex/Str
    2) Mage: Vit, Wis/Int
    3) Tank: Vit, Def/Res
    Primary stat is RANDOM - so if it happens to be Vit it's hard to tell
    what the base was.
    Similar to chests, the numbers can be skewed towards any of the stats
    */

	public BootsFactory(GameRandom random)
	{
		this.random = random;
	}

	public static Boots GetSimple()
	{
		List<EquipmentStat> listStats = new()
		{
			new(EquipmentMod.Vitality,50f),
			new(EquipmentMod.Dexterity,30f),
			new(EquipmentMod.Wisdom,30f),
		};
		return new("Simple Boots", 0, Rarity.Rare, listStats);
	}

	public Boots CraftBoots(Rarity r)
	{
		Rarity rarity = r;
		float base1 = 10f;
		float base2 = 10f;
		float base3 = 10f;
		List<EquipmentMod> listStats = new();

		//check if unique and if so - skip all of this to unique roller
		//currently uniques unimplemented
		if (rarity == Rarity.Unique)
		{
			//return randomUnique
		}

		//generate the ratios between the 3 stats
		//presently a is favored to be larger, roughly "45" average
		//meaning the other two split "55" approx evenly
		int a, b, c, max;
		a = random.Random.Next(10, 81);
		if (a == 80)
		{
			b = 10;
			c = 10;
		}
		else
		{
			max = 90 - a;
			b = random.Random.Next(10, max + 1);
			c = 100 - a - b;
		}
		//assign them randomly between 0-5 for each shuffle
		int stat1, stat2, stat3;
		int assignment = random.Random.Next(0, 6);
		switch (assignment)
		{
			case 0: stat1 = a; stat2 = b; stat3 = c; break;
			case 1: stat1 = a; stat2 = c; stat3 = b; break;
			case 2: stat1 = b; stat2 = a; stat3 = c; break;
			case 3: stat1 = b; stat2 = c; stat3 = a; break;
			case 4: stat1 = c; stat2 = a; stat3 = b; break;
			case 5: stat1 = c; stat2 = b; stat3 = a; break;
			default: stat1 = a; stat2 = b; stat3 = c; break;
		}

		//determine type of boots
		int bootType = random.Random.Next(0, 3);
		listStats.Add(EquipmentMod.Vitality);
		switch (bootType)
		{
			case 0:
				listStats.Add(EquipmentMod.Defense);
				listStats.Add(EquipmentMod.Resistance);
				break;
			case 1:
				listStats.Add(EquipmentMod.Dexterity);
				listStats.Add(EquipmentMod.Strength);
				break;
			case 2:
				listStats.Add(EquipmentMod.Wisdom);
				listStats.Add(EquipmentMod.Intelligence);
				break;
			default:
				listStats.Add(EquipmentMod.Defense);
				listStats.Add(EquipmentMod.Resistance);
				break;
		}

		//determine if base stats have bonus from rarity
		if (rarity == Rarity.Common)
		{
			base1 += (float)stat1;
			base2 += (float)stat2;
			base3 += (float)stat3;
		}
		else
		{
			float x = (base1 + (float)stat1) * 1.5f;
			float y = (base2 + (float)stat2) * 1.5f;
			float z = (base3 + (float)stat3) * 1.5f;
			base1 = MathFunc.Round(x, 0);
			base2 = MathFunc.Round(y, 0);
			base3 = MathFunc.Round(z, 0);
		}
		List<EquipmentStat> stats = new();

		int index = random.Random.Next(0, listStats.Count);
		stats.Add(new EquipmentStat(listStats[index], base1));
		listStats.RemoveAt(index);
		if (rarity == Rarity.Rare)
		{
			//figure out second stat if Rare
			index = random.Random.Next(0, listStats.Count);
			stats.Add(new EquipmentStat(listStats[index], base2));
		}
		else if (rarity == Rarity.Legendary || rarity == Rarity.Unique) //unique to be removed when uniques implemented
		{
			stats.Add(new EquipmentStat(listStats[0], base2));
			stats.Add(new EquipmentStat(listStats[1], base3));
		}

		var boots = new Boots("Boots", 0, rarity, stats);

		return boots;
	}
}
