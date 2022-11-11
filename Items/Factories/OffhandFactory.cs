using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;
using AFK_Dungeon_Lib.Utility;

namespace AFK_Dungeon_Lib.Items.Factories;
public static class OffhandFactory
{
	public static Offhand CraftOffhand(Rarity r)
	{
		Rarity rarity = r;
		var rand = new Random();
		float base1 = 10f;
		float base2 = 10f;
		float base3 = 10f;
		List<EquipmentMod> listStats = new();
		string itemName;
		bool twoHandEquip;
		SpellModifier spellMod;
		OffhandClass offhandClass;

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
		a = rand.Next(10, 80);
		if (a == 80)
		{
			b = 10;
			c = 10;
		}
		else
		{
			max = 90 - a;
			b = rand.Next(10, max + 1);
			c = 100 - a - b;
		}
		//assign them randomly between 0-5 for each shuffle
		int stat1, stat2, stat3;
		int assignment = rand.Next(0, 6);
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

		//determine type of item
		//first determine if Tome or Shield
		if (rand.Next(0, 2) == 0)
		{
			itemName = "Shield";
			offhandClass = OffhandClass.Shield;
			spellMod = SpellModifier.None;
			listStats.Add(EquipmentMod.Vitality);
			listStats.Add(EquipmentMod.Defense);
			listStats.Add(EquipmentMod.Resistance);
			twoHandEquip = false;
		}
		else
		{
			itemName = "Tome";
			offhandClass = OffhandClass.Tome;
			spellMod = (SpellModifier)rand.Next(0, 4);
			listStats = PickTomeMods(rand.Next(0, 2));
			twoHandEquip = true;
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
		int index = rand.Next(0, listStats.Count);
		stats.Add(new EquipmentStat(listStats[index], base1));
		listStats.RemoveAt(index);
		if (rarity == Rarity.Rare)
		{
			//figure out second stat if Rare
			index = rand.Next(0, listStats.Count);
			stats.Add(new EquipmentStat(listStats[index], base2));
		}
		else if (rarity == Rarity.Legendary || rarity == Rarity.Unique)
		{
			stats.Add(new EquipmentStat(listStats[0], base2));
			stats.Add(new EquipmentStat(listStats[1], base3));
		}

		return new Offhand(itemName, 0, rarity, stats, twoHandEquip, spellMod, offhandClass);
	}
	public static List<EquipmentMod> PickTomeMods(int p)
	{
		List<EquipmentMod> _stats = new();

		switch (p)
		{
			case 0:
				_stats.Add(EquipmentMod.Vitality);
				_stats.Add(EquipmentMod.Defense);
				_stats.Add(EquipmentMod.Resistance);
				break;
			case 1:
				_stats.Add(EquipmentMod.Vitality);
				_stats.Add(EquipmentMod.Wisdom);
				_stats.Add(EquipmentMod.Intelligence);
				break;
		}
		return _stats;
	}
}
