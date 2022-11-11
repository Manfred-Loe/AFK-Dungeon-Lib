using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Factories;

namespace AFK_Dungeon_Lib.Items.Factories;
public static class ItemFactory
{
	// cC = createCommon, cU = createUncommon
	//number chance, what "percent" chance it is that item. Note, it checks UP from the bottom
	//i.e. if cC = 80 (80%), there's only a 20% chance it will even get to cU
	//if cU is 70% it's only 70% of the 20% a.k.a. 14% chance, so only a 6% chance it gets to cR, so on and so forth
	public static IEquipment CraftEquipment(int cC, int cU, int cR, int cL, int cUnique)
	{
		var rand = new Random();
		Rarity r;

		//determine rarity
		//first check if normal, then uncommon, etc.
		//if "
		if (RandomNumber(0, 101) < cC)
		{
			r = Rarity.Common;
		}
		else if (RandomNumber(0, 101) < cU)
		{
			r = Rarity.Uncommon;
		}
		else if (RandomNumber(0, 101) < cR)
		{
			r = Rarity.Rare;
		}
		else if (RandomNumber(0, 101) < cL)
		{
			r = Rarity.Legendary;
		}
		else if (RandomNumber(0, 101) < cUnique)
		{
			r = Rarity.Unique;
		}
		else
		{
			r = Rarity.Common;
		}

		//determine the piece of equipment and call that specific factory
		int random = rand.Next(0, 5);
		return random switch
		{
			0 => HelmetFactory.CraftHelmet(r),
			1 => ChestFactory.CraftChest(r),
			2 => BootsFactory.CraftBoots(r),
			3 => WeaponFactory.CraftWeapon(r),
			4 => OffhandFactory.CraftOffhand(r),
			_ => HelmetFactory.CraftHelmet(r),
		};
	}

	//"Basic" is used as there are two crafting methods:
	//Basic Crafting returns normal,uncommon, or rare.
	//Nonbasic can return Legendary and Unique as well.
	public static IEquipment CraftItem(bool weapon, bool basic, bool phys)
	{
		if (weapon)
		{
			if (basic)
			{
				if (phys)
				{
					return CraftWeapon(true, true);
				}
				else
				{
					return CraftWeapon(false, true);
				}
			}
			else
			{
				if (phys)
				{
					return CraftWeapon(true, false);
				}
				else
				{
					return CraftWeapon(false, false);
				}
			}
		}
		else
		{
			return CraftGear(basic);
		}
	}

	public static Gear CraftGear(bool basic)
	{
		var rand = new Random();
		Rarity rarity;
		if (basic) { rarity = (Rarity)rand.Next(0, 2); }
		else { rarity = (Rarity)rand.Next(1, 4); }

		int gearType = rand.Next(0, 5);
		return gearType switch
		{
			0 => HelmetFactory.CraftHelmet(rarity),
			1 => ChestFactory.CraftChest(rarity),
			2 => BootsFactory.CraftBoots(rarity),
			3 => OffhandFactory.CraftOffhand(rarity),
			_ => HelmetFactory.CraftHelmet(rarity),
		};
	}

	public static Weapon CraftWeapon(bool phys, bool basic)
	{
		Rarity rarity;
		var rand = new Random();
		if (basic) { rarity = (Rarity)rand.Next(0, 3); }
		else { rarity = (Rarity)rand.Next(2, 5); }

		if (phys)
		{
			return WeaponFactory.CraftPhysWeapon(rarity);
		}
		else
		{
			return WeaponFactory.CraftMagicWeapon(rarity);
		}
	}

	private static int RandomNumber(int start, int end)
	{
		var rand = new Random();
		return rand.Next(start, end);
	}
}
