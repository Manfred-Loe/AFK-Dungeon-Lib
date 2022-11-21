using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Utility;

namespace AFK_Dungeon_Lib.Items.Factories;

public class HelmetFactory
{
	readonly GameRandom random;
	public HelmetFactory(GameRandom random)
	{
		this.random = random;
	}

	public static Helmet GetSimple()
	{
		List<EquipmentStat> listStats = new()
		{
			new(EquipmentMod.Vitality,50f),
			new(EquipmentMod.Strength,30f),
			new(EquipmentMod.Intelligence,30f),
		};
		return new("Simple Helmet", 0, Rarity.Rare, listStats);
	}

	public Helmet CraftHelmet(Rarity r)
	{
		var rarity = r;
		float base1 = 10f, base2 = 10f, base3 = 10f;
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
		a = random.Random.Next(10, 80);
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

		//determine category of helmet:
		/*  Basic (Vitality primary, secondary and tertiary random)
            Tank (Vitality Primary, Def/Res, Def/Res)
            Dex (Dex Primary, Vit)
            Wisdom (Wis Primary, Vit)
            Def (Defense Primary)
            Res (Res Primary)
         */

		int helmetType = random.Random.Next(0, 6);

		switch (helmetType)
		{
			case 0:
				listStats.Add(EquipmentMod.Vitality);
				listStats.Add(NewRandomStat(listStats));
				listStats.Add(NewRandomStat(listStats));
				break;
			case 1:
				listStats.Add(EquipmentMod.Vitality);
				listStats.Add(EquipmentMod.Defense);
				listStats.Add(EquipmentMod.Resistance);
				break;
			case 2:
				listStats.Add(EquipmentMod.Dexterity);
				listStats.Add(EquipmentMod.Vitality);
				listStats.Add(NewRandomStat(listStats));
				break;
			case 3:
				listStats.Add(EquipmentMod.Wisdom);
				listStats.Add(EquipmentMod.Vitality);
				listStats.Add(NewRandomStat(listStats));
				break;
			case 4:
				listStats.Add(EquipmentMod.Defense);
				listStats.Add(NewRandomStat(listStats));
				listStats.Add(NewRandomStat(listStats));
				break;
			case 5:
				listStats.Add(EquipmentMod.Resistance);
				listStats.Add(NewRandomStat(listStats));
				listStats.Add(NewRandomStat(listStats));
				break;
			default:
				listStats.Add(EquipmentMod.Vitality);
				listStats.Add(NewRandomStat(listStats));
				listStats.Add(NewRandomStat(listStats));
				break;
		}

		//determine if base stats have bonus from rarity
		if (rarity == Rarity.Common)
		{
			base1 += stat1;
			base2 += stat2;
			base3 += stat3;
		}
		else
		{
			float x = (base1 + stat1) * 1.5f;
			float y = (base2 + stat2) * 1.5f;
			float z = (base3 + stat3) * 1.5f;
			base1 = MathFunc.Round(x, 0);
			base2 = MathFunc.Round(y, 0);
			base3 = MathFunc.Round(z, 0);
		}

		List<EquipmentStat> stats = new();

		int index;
		stats.Add(new EquipmentStat(listStats[0], base1));
		listStats.RemoveAt(0);

		if (rarity == Rarity.Rare)
		{
			//figure out second stat if Rare
			index = random.Random.Next(0, listStats.Count);
			stats.Add(new EquipmentStat(listStats[index], base2));
		}
		else if (rarity == Rarity.Legendary || rarity == Rarity.Unique)
		{
			stats.Add(new EquipmentStat(listStats[0], base2));
			stats.Add(new EquipmentStat(listStats[1], base3));
		}

		var helmet = new Helmet("Helmet", 0, rarity, stats);

		return helmet;
	}

	private EquipmentMod NewRandomStat(List<EquipmentMod> list)
	{
		EquipmentMod newStat = RandomStatEnum();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == newStat)
			{
				EquipmentMod oldStat = newStat;
				while (newStat == oldStat)
				{
					newStat = RandomStatEnum();
				}
				i = 0;
			}
		}
		return newStat;
	}
	private EquipmentMod RandomStatEnum()
	{
		return (EquipmentMod)random.Random.Next(0, 9);
	}
}
