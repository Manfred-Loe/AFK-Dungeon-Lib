using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Utility;

namespace AFK_Dungeon_Lib.Items.Factories;
public static class ChestFactory
{
	public static Chest CraftChest(Rarity r)
	{
		Rarity rarity = r;
		var random = new Random();
		float baseVit = 10f;
		float baseDef = 10f;
		float baseRes = 10f;

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
		a = random.Next(10, 80);
		if (a == 80)
		{
			b = 10;
			c = 10;
		}
		else
		{
			max = 90 - a;
			b = random.Next(10, max + 1);
			c = 100 - a - b;
		}

		//assign them randomly between 0-5 for each shuffle
		int assignment = random.Next(0, 6);
		int def, res, vit;
		switch (assignment)
		{
			case 0: vit = a; def = b; res = c; break;
			case 1: vit = a; def = c; res = b; break;
			case 2: vit = b; def = a; res = c; break;
			case 3: vit = b; def = c; res = a; break;
			case 4: vit = c; def = a; res = b; break;
			case 5: vit = c; def = b; res = a; break;
			default: vit = a; def = b; res = c; break;
		}

		//determine if base stats have bonus
		if (rarity == Rarity.Common)
		{
			baseVit += vit;
			baseDef += def; //this and the next line largely irrelevant here
			baseRes += res; //because common only has vitality
		}
		else
		{
			float x = (baseVit + vit) * 1.5f;
			float y = (baseDef + def) * 1.5f;
			float z = (baseRes + res) * 1.5f;
			baseVit = MathFunc.Round(x, 0);
			baseDef = MathFunc.Round(y, 0);
			baseRes = MathFunc.Round(z, 0);
		}

		var stats = new List<EquipmentStat>
		{
			new EquipmentStat(EquipmentMod.Vitality, baseVit)
		};

		if (rarity == Rarity.Rare)
		{
			//figure out second stat if Rare
			float x = random.Next(0, 2);
			if (x == 0)
			{
				stats.Add(new EquipmentStat(EquipmentMod.Defense, baseDef));
			}
			else
			{
				stats.Add(new EquipmentStat(EquipmentMod.Resistance, baseRes));
			}
		}
		else if (rarity == Rarity.Legendary || rarity == Rarity.Unique)
		{
			stats.Add(new EquipmentStat(EquipmentMod.Defense, baseDef));
			stats.Add(new EquipmentStat(EquipmentMod.Resistance, baseRes));
		}

		var chest = new Chest("Chest Piece", 0, rarity, stats);
		return chest;
	}
}
