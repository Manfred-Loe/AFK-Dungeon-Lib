using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Factories;

namespace AFK_Dungeon_Lib.Items.Factories;
public class ItemFactory
{
	readonly GameRandom random;
	readonly HelmetFactory hf;
	readonly ChestFactory cf;
	readonly BootsFactory bf;
	readonly WeaponFactory wf;
	readonly OffhandFactory of;

	public ItemFactory(GameRandom random, HelmetFactory hf, ChestFactory cf, BootsFactory bf, WeaponFactory wf, OffhandFactory of)
	{
		this.random = random;
		this.hf = hf;
		this.cf = cf;
		this.bf = bf;
		this.wf = wf;
		this.of = of;
	}
	// cC = createCommon, cU = createUncommon
	//number chance, what "percent" chance it is that item. Note, it checks UP from the bottom
	//i.e. if cC = 80 (80%), there's only a 20% chance it will even get to cU
	//if cU is 70% it's only 70% of the 20% a.k.a. 14% chance, so only a 6% chance it gets to cR, so on and so forth
	public IEquipment CraftEquipment(int cC, int cU, int cR, int cL, int cUnique)
	{
		Rarity r;

		//determine rarity
		//first check if normal, then uncommon, etc.
		//if "
		if (random.Random.Next(0, 101) < cC)
		{
			r = Rarity.Common;
		}
		else if (random.Random.Next(0, 101) < cU)
		{
			r = Rarity.Uncommon;
		}
		else if (random.Random.Next(0, 101) < cR)
		{
			r = Rarity.Rare;
		}
		else if (random.Random.Next(0, 101) < cL)
		{
			r = Rarity.Legendary;
		}
		else if (random.Random.Next(0, 101) < cUnique)
		{
			r = Rarity.Unique;
		}
		else
		{
			r = Rarity.Common;
		}

		//determine the piece of equipment and call that specific factory
		int randomNumber = random.Random.Next(0, 5);
		return randomNumber switch
		{
			0 => hf.CraftHelmet(r),
			1 => cf.CraftChest(r),
			2 => bf.CraftBoots(r),
			3 => wf.CraftWeapon(r),
			4 => of.CraftOffhand(r),
			_ => hf.CraftHelmet(r),
		};
	}

	//"Basic" is used as there are two crafting methods:
	//Basic Crafting returns normal,uncommon, or rare.
	//Nonbasic can return Legendary and Unique as well.
	public IEquipment CraftItem(bool weapon, bool basic, bool phys)
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

	public Gear CraftGear(bool basic)
	{
		Rarity rarity;
		if (basic) { rarity = (Rarity)random.Random.Next(0, 2); }
		else { rarity = (Rarity)random.Random.Next(1, 4); }

		int gearType = random.Random.Next(0, 5);
		return gearType switch
		{
			0 => hf.CraftHelmet(rarity),
			1 => cf.CraftChest(rarity),
			2 => bf.CraftBoots(rarity),
			3 => of.CraftOffhand(rarity),
			_ => hf.CraftHelmet(rarity),
		};
	}

	public Weapon CraftWeapon(bool phys, bool basic)
	{
		Rarity rarity;
		if (basic) { rarity = (Rarity)random.Random.Next(0, 3); }
		else { rarity = (Rarity)random.Random.Next(2, 5); }

		if (phys)
		{
			return wf.CraftPhysWeapon(rarity);
		}
		else
		{
			return wf.CraftMagicWeapon(rarity);
		}
	}
}
