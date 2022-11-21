using AFK_Dungeon_Lib.API;
using AFK_Dungeon_Lib.Items;
using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Factories;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Utility;
using AFK_Dungeon_Lib;

namespace AFK_Console.Game;
internal class GameProgram
{
	readonly AFKDungeon game;
	const int padding = 9;
	const int paddingStat = -14;

	public GameProgram()
	{
		game = new();
	}

	public void Run()
	{
		bool run = true;
		while (run)
		{
			Console.WriteLine("------------------\n" +
							  "AFK Dungeon Loaded\n" +
							  "------------------\n" +
							  "1: Enter Dungeon\n" +
							  "2: Manage Heroes\n" +
							  "3: Craft/Manage Gear\n" +
							  "0: Quit\n" +
							  "------------------");
			int? input = Convert.ToInt32(Console.ReadLine());
			switch (input)
			{
				case 0: run = false; continue;
				case 1: Dungeon(); break;
				case 2: Console.Clear(); HeroManagement(); break;
				case 3: DisplayCrafting(); break;
				default: break;
			}
		}
	}

	private void Dungeon()
	{
	}
	private void HeroManagement()
	{
		while (true)
		{
			Console.WriteLine("------------------\n" +
							  "Hero Management\n" +
							  "------------------\n" +
							  "1: Character Sheet\n" +
							  "2: Level Up\n" +
							  "3: Equipment\n" +
							  "4: Formation\n" +
							  "0: Main Menu\n"
							);
			int? input = Convert.ToInt32(Console.ReadLine());
			switch (input)
			{
				case 0: goto exit_loop;
				case 1: DisplayHero(SelectHero(true)); break;
				case 2: DisplayLevelUp(SelectHero(true)); break;
				case 3: ManageEquipment(SelectHero(true)); break;
				case 4: Formation(); break;
				case null: break;
				default: break;
			}
		}
	exit_loop:;
	}
	private Hero? SelectHero(bool clear)
	{
		List<Hero> heroes = game.data.GetHeroes();
		if (clear) { Console.Clear(); }
		Console.WriteLine("Select Hero:");
		for (int i = 0; i < heroes.Count; i++)
		{
			Console.WriteLine($"{i + 1}: {heroes[i].Name}");
		}
		Console.WriteLine("0: Hero Management");
		int? input = Convert.ToInt32(Console.ReadLine());
		if (input == 0 || input == null) { return null; }
		return heroes[(int)input - 1];
	}
	private static void DisplayHero(Hero? h)
	{
		if (h == null) { return; }
		string displayHero = "------------------------------------------------------\n" +
									$"Hero Stats for | {h.Name,padding}\n" +
									"------------------------------------------------------\n" +
									$"{"Stat",paddingStat} {"Initial",padding} {"Bonus",padding} {"Final",padding} {"Current",padding}\n" +
									"------------------------------------------------------\n" +
									$"{"Strength:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Dexterity:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Vitality:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Intelligence:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Wisdom:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Fortitude:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Will:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Health:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Defense:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Resistance:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Damage Mod:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Phys Damage:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Dex Damage:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Phys CC:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Phys CD:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Mage Damage:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Wid Damage",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Mage CC:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Mage CD:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									$"{"Attack Spd:",paddingStat} {h.Stats.Strength.Initial,padding} {h.Stats.Strength.Bonus,padding} {h.Stats.Strength.Final,padding} {h.Stats.Strength.Current,padding}\n" +
									"\n" +
									$"{"Position:",paddingStat} {"X: " + h.Position.X + " Y: " + h.Position.Y,padding}\n" +
									"------------------------------------------------------";
		Console.WriteLine(displayHero);
		Console.ReadLine();
	}
	public static void DisplayLevelUp(Hero? h)
	{
		if (h == null) { return; }
		string displayLevelup = "";
		while (h.AvailableAbilityPoints > 0)
		{
			displayLevelup = "------------------------------------------------------\n" +
							$"Hero Level Up | {h.Name} Available Points: {h.AvailableAbilityPoints}\n" +
							$"{"1 Strength:",paddingStat} {h.Stats.Strength.Initial,padding}\n" +
							$"{"2 Dexterity:",paddingStat} {h.Stats.Dexterity.Initial,padding}\n" +
							$"{"3 Vitality:",paddingStat} {h.Stats.Vitality.Initial,padding}\n" +
							$"{"4 Intelligence:",paddingStat} {h.Stats.Intelligence.Initial,padding}\n" +
							$"{"5 Wisdom:",paddingStat} {h.Stats.Wisdom.Initial,padding}\n" +
							$"{"6 Fortitude:",paddingStat} {h.Stats.Fortitude.Initial,padding}\n" +
							$"{"7 Will:",paddingStat} {h.Stats.Will.Initial,padding}\n" +
							$"{"-1 Reset:",paddingStat}\n" +
							$"{"0 Back:",paddingStat}\n" +
							"------------------------------------------------------";
			Console.Clear();
			Console.WriteLine(displayLevelup);
			int? input = Convert.ToInt32(Console.ReadLine());
			switch (input)
			{
				case 0: goto exit_levelup;
				case > 0: h.LevelUp((AFK_Dungeon_Lib.Pawns.StatsEnum)(input - 1)); break;
				case -1: h.ResetHero(); break;
				default: goto exit_levelup;
			}
		}
	exit_levelup: Console.WriteLine("------------------------------------------------------\n" +
							$"Hero Level Up | {h.Name} Available Points: {h.AvailableAbilityPoints}\n" +
							$"{"1 Strength:",paddingStat} {h.Stats.Strength.Initial,padding}\n" +
							$"{"2 Dexterity:",paddingStat} {h.Stats.Dexterity.Initial,padding}\n" +
							$"{"3 Vitality:",paddingStat} {h.Stats.Vitality.Initial,padding}\n" +
							$"{"4 Intelligence:",paddingStat} {h.Stats.Intelligence.Initial,padding}\n" +
							$"{"5 Wisdom:",paddingStat} {h.Stats.Wisdom.Initial,padding}\n" +
							$"{"6 Fortitude:",paddingStat} {h.Stats.Fortitude.Initial,padding}\n" +
							$"{"7 Will:",paddingStat} {h.Stats.Will.Initial,padding}\n" +
							$"{"-1 Reset:",paddingStat}\n" +
							$"{"0 Back:",paddingStat}\n" +
							"------------------------------------------------------");
	}
	private void ManageEquipment(Hero? h)
	{
		if (h == null) { return; }

		Console.WriteLine("------------------\n" +
						"Equipment Management\n" +
						"------------------\n" +
						"1: View Equipped\n" +
						"2: Change Equipment\n" +
						"3: Inventory Management\n" +
						"0: Back\n"
						);
		int? input = Convert.ToInt32(Console.ReadLine());
		int? selection = 0;
		if (input == 0 || input == null) { return; }
		switch (input)
		{
			case 1:
				if (h.EquippedHelmet != null) { DisplayEquipment(h.EquippedHelmet); }
				if (h.EquippedChest != null) { DisplayEquipment(h.EquippedChest); }
				if (h.EquippedBoots != null) { DisplayEquipment(h.EquippedBoots); }
				if (h.EquippedMainHand != null) { DisplayEquipment(h.EquippedMainHand); }
				if (h.EquippedOffhand != null) { DisplayEquipment(h.EquippedOffhand); }
				break;
			case 2:
				Console.WriteLine("------------------\n" +
							"Which Slot\n" +
							"------------------\n" +
							"1: Helmet\n" +
							"2: Chest\n" +
							"3: Boots\n" +
							"4: Main Hand\n" +
							"5: Off Hand\n" +
							"0: Back\n");
				selection = Convert.ToInt32(Console.ReadLine());
				break;
			case 3:
				DisplayInventory();
				break;
			default: break;
		}
		List<IEquipment> equipment = new();
		if (selection != 0 && selection != null)
		{
			equipment = DisplayInventoryType(selection switch
			{
				1 => EquipmentType.Helmet,
				2 => EquipmentType.Chest,
				3 => EquipmentType.Boots,
				4 => EquipmentType.Weapon,
				5 => EquipmentType.OffHand,
				_ => EquipmentType.Helmet
			});
		}
	}
	private void DisplayInventory()
	{
		Console.Clear();
		Inventory inventory = game.data.GetInventory();
		int count = inventory.Equipment.Count;
		Console.WriteLine("------------------------------------------------------\n" +
						$"Inventory | Gold: {inventory.Gold} Crystals: {inventory.Crystals}\n" +
						"------------------------------------------------------\n" +
						$"#  {"Item Name",-12} {"Level",-8} {"Equipped",-12}");
		string equipment = "";
		for (int i = 0; i < count; i++)
		{
			int x = i + 1;
			equipment += $"{x}: {inventory.Equipment[i].ItemName,-12} {inventory.Equipment[i].Level,-8} {inventory.Equipment[i].EquippedTo?.Name,-12}\n";
		}
		Console.WriteLine(equipment);
		Console.WriteLine("0: Back");
		int? input = Convert.ToInt32(Console.ReadLine());
		if (input == 0 || input == null) { return; }
		switch (input - 1)
		{
			case >= 0:
				if (input < count)
				{
					DisplayEquipment(inventory.Equipment[(int)input - 1]);
				}
				break;
			default: break;
		}
	}
	private List<IEquipment> DisplayInventoryType(EquipmentType type)
	{
		Inventory inventory = game.data.GetInventory();
		List<IEquipment> equipment = new();
		for (int i = 0; i < inventory.Equipment.Count; i++)
		{
			if (inventory.Equipment[i].Type == type)
			{
				equipment.Add(inventory.Equipment[i]);
			}
		}
		string displayInventory = $"#  {"Item Name",-5} {"Level",-5}\n";
		int x = 1;
		for (int i = 0; i < equipment.Count; i++)
		{
			displayInventory += $"{x}: {inventory.Equipment[i].ItemName,-5} {inventory.Equipment[i].Level,-5}\n";
			x++;
		}
		Console.WriteLine(displayInventory);
		return equipment;
	}
	private static void DisplayEquipment(IEquipment e)
	{
		Console.Clear();
		var count = e.GetStatCount();
		string equipment = $"Name: {e.ItemName} Level: {e.Level} Rarity: {e.Rarity} Type: {e.Type}\n";
		if (e.EquippedTo != null)
		{
			equipment += $"Equipped to: {e.EquippedTo?.Name}\n";
		}
		switch (e.Type)
		{
			case EquipmentType.OffHand:
				Offhand o = (Offhand)e;
				equipment += $"Offhand Class: {o.OffhandClass} Spell Modifier: {o.Modifier}\n";
				break;
			case EquipmentType.Weapon:
				Weapon w = (Weapon)e;
				equipment += $"Damage Modifier: {w.DamageModifier} Attack Speed: {w.AttackSpeed} Quality Modifier: {w.QualityModifier}\n" +
							$"Crit Chance: {w.CritChance} Crit Damage: {w.CritDamage}\n";
				break;
			default: break;
		}
		for (int i = 0; i < count; i++)
		{
			equipment += $"{e.GetStatType(i)}: {e.GetStatValue(i)}\n";
		}
		Console.WriteLine(equipment);
		Console.ReadLine();
		return;
	}
	private void Formation()
	{
		List<Hero> heroes = game.data.GetHeroes();
		string formationDisplay = "";
		int y = 0;
		for (int i = 0; i < 16; i++)
		{
			if (i % 4 == 0)
			{
				formationDisplay += "+-------------+-------------+\n";
			}
			else if (i % 2 == 0 && (i + 2) % 4 == 0)
			{
				Hero? hero1 = heroes.Find(hero => hero.Position.X == 0 && hero.Position.Y == y);
				Hero? hero2 = heroes.Find(hero => hero.Position.X == 1 && hero.Position.Y == y);
				formationDisplay += $"|{hero1?.Name,-13}|{hero2?.Name,-13}|\n";
				y++;
			}
			else
			{
				formationDisplay += "|             |             |\n";
			}
		}
		formationDisplay += "+-------------+-------------+";
		Console.WriteLine(formationDisplay);
		Hero? h = SelectHero(false);
		if (h == null) { return; }
		Console.WriteLine("Move hero to location:\n" +
						"1	2\n" +
						"3	4\n" +
						"5	6\n" +
						"7	8");
		int? input = Convert.ToInt32(Console.ReadLine());
		if (input == 0 || input == null) { return; }
		Coordinate targetCoord = input switch
		{
			1 => new(0, 0),
			2 => new(1, 0),
			3 => new(0, 1),
			4 => new(1, 1),
			5 => new(0, 2),
			6 => new(1, 2),
			7 => new(0, 3),
			8 => new(1, 3),
			_ => new(0, 0)
		};
		//try try to assign hero to new position, if there's a hero assigned already, offer to swap
		for (int i = 0; i < 4; i++)
		{
			if (h?.Name != heroes[i].Name)
			{
				if (heroes[i].Position.Equals(targetCoord))
				{
					Console.WriteLine("Target position is filled, do you want to swap? (y/n)");
					string? answer = Console.ReadLine();
					if (answer == "y")
					{
						heroes[i].SetPosition(h!.Position);
						h.SetPosition(targetCoord);
						goto end_formation;
					}
				}
			}
		}
		h!.SetPosition(targetCoord);
	end_formation:;
	}
	private void DisplayCrafting()
	{
		while (true)
		{
			Console.Clear();
			Inventory inventory = game.data.GetInventory();
			Console.WriteLine("------------------\n" +
							"Crafting Station |\n" +
							$"Gold: {inventory.Gold} Crystals: {inventory.Crystals}\n" +
							$"Metal Scraps: {inventory.MetalScraps} Ether Scraps: {inventory.EtherScraps}\n" +
							$"High Quality Metal: {inventory.HighQualityMetal} High Quality Ether: {inventory.HighQualityEther}\n" +
							"------------------\n" +
							"1: Craft Initial Set (free)\n" +
							"2: Craft Random Item (50 Metal Scraps, 20 Ether Scraps)\n" +
							"3: Craft High Quality Item (50 High Quality Metal, 20 High Quality Ether)\n" +
							"4: View Inventory\n" +
							"0: Back\n" +
							"------------------");
			int? input = Convert.ToInt32(Console.ReadLine());
			if (input == null || input == 0) { return; }
			switch (input)
			{
				case 1:
					inventory.Equipment.Add(BootsFactory.GetSimple());
					inventory.Equipment.Add(ChestFactory.GetSimple());
					inventory.Equipment.Add(HelmetFactory.GetSimple());
					inventory.Equipment.Add(WeaponFactory.GetSimple());
					inventory.Equipment.Add(OffhandFactory.GetSimple());
					break;
				case 2:
					if (inventory.MetalScraps >= 50 && inventory.EtherScraps >= 20)
					{
						inventory.MetalScraps -= 50;
						inventory.EtherScraps -= 20;
						inventory.Equipment.Add(game.itemfactory.CraftEquipment(90, 80, 100, 0, 0));
					}
					break;
				case 3:
					if (inventory.HighQualityMetal >= 50 && inventory.HighQualityEther >= 20)
					{
						inventory.HighQualityMetal -= 50;
						inventory.HighQualityEther -= 20;
						inventory.Equipment.Add(game.itemfactory.CraftEquipment(0, 0, 90, 100, 0));
					}
					break;
				case 4: DisplayInventory(); break;
				default: break;
			}
		}

	}
}