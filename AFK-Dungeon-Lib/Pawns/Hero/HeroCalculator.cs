using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

namespace AFK_Dungeon_Lib.Pawns.Hero;

public static class HeroCalculator
{
	public static void CalcBaseStats(Hero h)
	{
		h.Stats.Health.Initial = 30 + Convert.ToInt32(0.2f * h.Level * h.Stats.Vitality.Initial);
		h.Stats.DamagePhys.Initial = 5 + Convert.ToInt32(0.2f * h.Level * h.Stats.Strength.Initial);
		h.Stats.DamageDex.Initial = 5 + Convert.ToInt32(0.2f * h.Level * h.Stats.Dexterity.Initial);
		h.Stats.CritChancePhys.Initial = (float)h.Stats.Dexterity.Initial / 1000f;
		h.Stats.CritDmgPhys.Initial = 0.5f;
		h.Stats.DamageMage.Initial = 5 + Convert.ToInt32(0.2f * h.Level * h.Stats.Intelligence.Initial);
		h.Stats.DamageWis.Initial = 5 + Convert.ToInt32(0.2f * h.Level * h.Stats.Wisdom.Initial);
		h.Stats.CritChanceMage.Initial = (float)h.Stats.Wisdom.Initial / 100f;
		h.Stats.CritDmgMage.Initial = 0.5f;
		h.Stats.Defense.Initial = h.Stats.Fortitude.Initial * 2;
		h.Stats.Resistance.Initial = h.Stats.Will.Initial * 2;
	}

	//be careful, if State = Initial you can break heroes
	public static void ResetStats(Hero h, StatStateEnum state)
	{
		h.Stats.Strength.SetValue(state, 0);
		h.Stats.Dexterity.SetValue(state, 0);
		h.Stats.Vitality.SetValue(state, 0);
		h.Stats.Intelligence.SetValue(state, 0);
		h.Stats.Wisdom.SetValue(state, 0);
		h.Stats.Fortitude.SetValue(state, 0);
		h.Stats.Will.SetValue(state, 0);

		h.Stats.Health.SetValue(state, 0);
		h.Stats.Defense.SetValue(state, 0);
		h.Stats.Resistance.SetValue(state, 0);
		h.Stats.DamagePhys.SetValue(state, 0);
		h.Stats.DamageDex.SetValue(state, 0);
		h.Stats.CritChancePhys.SetValue(state, 0);
		h.Stats.CritDmgPhys.SetValue(state, 0);
		h.Stats.DamageMage.SetValue(state, 0);
		h.Stats.DamageWis.SetValue(state, 0);
		h.Stats.CritChanceMage.SetValue(state, 0);
		h.Stats.CritDmgMage.SetValue(state, 0);
		h.Stats.AttackSpeed.SetValue(state, 0);
		h.Stats.DamageModifier.SetValue(state, 0);
	}

	public static void ResetCurrent(Hero h)
	{
		h.Stats.Strength.SetValue(StatStateEnum.Current, h.Stats.Strength.Final);
		h.Stats.Dexterity.SetValue(StatStateEnum.Current, h.Stats.Dexterity.Final);
		h.Stats.Vitality.SetValue(StatStateEnum.Current, h.Stats.Vitality.Final);
		h.Stats.Intelligence.SetValue(StatStateEnum.Current, h.Stats.Intelligence.Final);
		h.Stats.Wisdom.SetValue(StatStateEnum.Current, h.Stats.Wisdom.Final);
		h.Stats.Fortitude.SetValue(StatStateEnum.Current, h.Stats.Fortitude.Final);
		h.Stats.Will.SetValue(StatStateEnum.Current, h.Stats.Will.Final);

		h.Stats.Health.SetValue(StatStateEnum.Current, h.Stats.Health.Final);
		h.Stats.Defense.SetValue(StatStateEnum.Current, h.Stats.Defense.Final);
		h.Stats.Resistance.SetValue(StatStateEnum.Current, h.Stats.Resistance.Final);
		h.Stats.DamagePhys.SetValue(StatStateEnum.Current, h.Stats.DamagePhys.Final);
		h.Stats.DamageDex.SetValue(StatStateEnum.Current, h.Stats.DamageDex.Final);
		h.Stats.CritChancePhys.SetValue(StatStateEnum.Current, h.Stats.CritChancePhys.Final);
		h.Stats.CritDmgPhys.SetValue(StatStateEnum.Current, h.Stats.CritDmgPhys.Final);
		h.Stats.DamageMage.SetValue(StatStateEnum.Current, h.Stats.DamageMage.Final);
		h.Stats.DamageWis.SetValue(StatStateEnum.Current, h.Stats.DamageWis.Final);
		h.Stats.CritChanceMage.SetValue(StatStateEnum.Current, h.Stats.CritChanceMage.Final);
		h.Stats.CritDmgMage.SetValue(StatStateEnum.Current, h.Stats.CritDmgMage.Final);
		h.Stats.AttackSpeed.SetValue(StatStateEnum.Current, h.Stats.AttackSpeed.Final);
	}

	public static void CalcEquippedStats(Hero h)
	{
		var modifierList = new List<(EquipmentMod, float)>();

		switch (h.EquippedMainHand?.ItemStats[0].StatType)
		{
			case EquipmentMod.Strength: h.Phys = true; h.AlternateDmg = false; break;
			case EquipmentMod.Dexterity: h.Phys = true; h.AlternateDmg = true; break;
			case EquipmentMod.Intelligence: h.Phys = false; h.AlternateDmg = false; break;
			case EquipmentMod.Wisdom: h.Phys = false; h.AlternateDmg = true; break;
			default: h.Phys = true; h.AlternateDmg = false; break;
		}
		ResetStats(h, StatStateEnum.Bonus);

		if (h.EquippedOffhand is not null and Offhand)
		{
			h.DualWielding = false;
		}
		else if (h.EquippedOffhand is not null and Weapon)
		{
			h.DualWielding = true;
		}

		//crit rate, crit damage, attack speed
		//bonus dmg percent
		//any flat damage
		if (h.Phys)
		{
			if (h.EquippedMainHand != null)
			{
				Weapon w = (Weapon)h.EquippedMainHand;
				h.Stats.DamageModifier.Bonus = w.DamageModifier;
				if (!h.DualWielding)
				{
					h.Stats.AttackSpeed.Final = w.AttackSpeed;
					h.Stats.CritChancePhys.Bonus = w.CritChance;
					h.Stats.CritDmgPhys.Bonus = w.CritDamage;
				}
				else
				{
					if (h.EquippedOffhand != null)
					{
						Weapon o = (Weapon)h.EquippedOffhand;
						h.Stats.DamageModifier.Bonus += o.DamageModifier / 2f;
						h.Stats.AttackSpeed.Final = (w.AttackSpeed + o.AttackSpeed) / 2;
						h.Stats.CritChancePhys.Bonus = (w.CritChance + o.CritChance) / 2;
						h.Stats.CritDmgPhys.Bonus = (w.CritDamage + o.CritDamage) / 2;
					}
				}
			}
		}
		else
		{
			if (h.EquippedMainHand != null)
			{
				Weapon w = (Weapon)h.EquippedMainHand;
				h.Stats.DamageModifier.Bonus = w.DamageModifier;
				if (!h.DualWielding)
				{
					h.Stats.AttackSpeed.Final = w.AttackSpeed;
					h.Stats.CritChanceMage.Bonus = w.CritChance;
					h.Stats.CritDmgMage.Bonus = w.CritDamage;
				}
				else
				{
					if (h.EquippedOffhand != null)
					{
						Weapon o = (Weapon)h.EquippedOffhand;
						h.Stats.DamageModifier.Bonus += o.DamageModifier / 2f;
						h.Stats.AttackSpeed.Final = (w.AttackSpeed + o.AttackSpeed) / 2;
						h.Stats.CritChanceMage.Bonus = (w.CritChance + o.CritChance) / 2;
						h.Stats.CritChanceMage.Bonus = (w.CritDamage + o.CritDamage) / 2;
					}
				}
			}
		}

		if (h.EquippedHelmet != null)
		{
			for (int i = 0; i < h.EquippedHelmet.ItemStats.Count; i++)
			{
				modifierList.Add((h.EquippedHelmet.ItemStats[i].StatType, h.EquippedHelmet.ItemStats[i].StatValue));
			}
		}
		if (h.EquippedChest != null)
		{
			for (int i = 0; i < h.EquippedChest.ItemStats.Count; i++)
			{
				modifierList.Add((h.EquippedChest.ItemStats[i].StatType, h.EquippedChest.ItemStats[i].StatValue));
			}
		}
		if (h.EquippedBoots != null)
		{
			for (int i = 0; i < h.EquippedBoots.ItemStats.Count; i++)
			{
				modifierList.Add((h.EquippedBoots.ItemStats[i].StatType, h.EquippedBoots.ItemStats[i].StatValue));
			}
		}
		if (h.EquippedMainHand != null)
		{
			for (int i = 0; i < h.EquippedMainHand.ItemStats.Count; i++)
			{
				modifierList.Add((h.EquippedMainHand.ItemStats[i].StatType, h.EquippedMainHand.ItemStats[i].StatValue));
			}
		}
		if (h.EquippedOffhand != null)
		{
			for (int i = 0; i < h.EquippedOffhand.ItemStats.Count; i++)
			{
				modifierList.Add((h.EquippedOffhand.ItemStats[i].StatType, h.EquippedOffhand.ItemStats[i].StatValue));
			}
		}

		for (int i = 0; i < modifierList.Count; i++)
		{
			switch (modifierList[i].Item1)
			{
				case EquipmentMod.Strength: h.Stats.Strength.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Dexterity: h.Stats.Dexterity.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Vitality: h.Stats.Vitality.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Intelligence: h.Stats.Intelligence.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Wisdom: h.Stats.Wisdom.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Fortitude: h.Stats.Fortitude.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Will: h.Stats.Will.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Defense: h.Stats.Defense.Bonus += (int)modifierList[i].Item2; break;
				case EquipmentMod.Resistance: h.Stats.Resistance.Bonus += (int)modifierList[i].Item2; break;
				default: break;
			}
		}
	}

	public static void CalcFinalStats(Hero h)
	{
		h.Stats.Strength.Final = h.Stats.Strength.Initial + h.Stats.Strength.Bonus;
		h.Stats.Dexterity.Final = h.Stats.Dexterity.Initial + h.Stats.Dexterity.Bonus;
		h.Stats.Vitality.Final = h.Stats.Vitality.Initial + h.Stats.Vitality.Bonus;
		h.Stats.Intelligence.Final = h.Stats.Intelligence.Initial + h.Stats.Intelligence.Bonus;
		h.Stats.Wisdom.Final = h.Stats.Wisdom.Initial + h.Stats.Wisdom.Bonus;
		h.Stats.Fortitude.Final = h.Stats.Fortitude.Initial + h.Stats.Fortitude.Bonus;
		h.Stats.Will.Final = h.Stats.Will.Initial + h.Stats.Will.Bonus;

		h.Stats.Health.Final = 30 + Convert.ToInt32(0.2f * h.Level * h.Stats.Vitality.Final) + h.Stats.Health.Bonus;
		h.Stats.Defense.Final = (h.Stats.Fortitude.Final * 2) + h.Stats.Defense.Bonus;
		h.Stats.Resistance.Final = (h.Stats.Will.Final * 2) + h.Stats.Resistance.Bonus;

		//crit rate is weird
		//crit chance = (([attribute base] / 100) + 1 ) * WeaponCritChance )
		//WeaponCritChance stored in CritChance.Bonus as bonuses could come from more than just the weapon
		h.Stats.CritChancePhys.Final = (((float)h.Stats.Dexterity.Final / 100f) + 1f) * h.Stats.CritChancePhys.Bonus;
		h.Stats.CritChanceMage.Final = (((float)h.Stats.Wisdom.Final / 100f) + 1f) * h.Stats.CritChanceMage.Bonus;
		h.Stats.CritDmgPhys.Final = h.Stats.CritDmgPhys.Initial + h.Stats.CritDmgPhys.Bonus;
		h.Stats.CritDmgMage.Final = h.Stats.CritDmgMage.Initial + h.Stats.CritDmgMage.Bonus;

		if (!h.DualWielding)
		{
			if (h.EquippedMainHand != null)
			{
				h.Stats.AttackSpeed.Final = ((Weapon)h.EquippedMainHand).AttackSpeed;
			}
			else
			{
				h.Stats.AttackSpeed.Final = 1.0f;
			}
		}
		else
		{
			if (h.EquippedMainHand != null && h.EquippedOffhand != null)
			{
				h.Stats.AttackSpeed.Final = MathFunc.Round((((Weapon)h.EquippedMainHand).AttackSpeed + ((Weapon)h.EquippedOffhand).AttackSpeed) / 2, 2);
			}
		}

		if (h.Phys)
		{
			if (!h.AlternateDmg)
			{
				h.Stats.DamagePhys.Final = 5 + Convert.ToInt32(0.2f * h.Level * (float)h.Stats.Strength.Final * (float)h.Stats.DamagePhys.Bonus * h.Stats.DamageModifier.Final);
			}
			else
			{
				h.Stats.DamagePhys.Final = 5 + Convert.ToInt32(0.2f * h.Level * (float)h.Stats.Dexterity.Final * (float)h.Stats.DamagePhys.Bonus * h.Stats.DamageModifier.Final);
			}
		}
		else
		{
			if (!h.AlternateDmg)
			{
				h.Stats.DamageMage.Final = 5 + Convert.ToInt32(0.2f * h.Level * (float)h.Stats.Intelligence.Final * (float)h.Stats.DamageMage.Bonus * h.Stats.DamageModifier.Final);
			}
			else
			{
				h.Stats.DamageMage.Final = 5 + Convert.ToInt32(0.2f * h.Level * (float)h.Stats.Wisdom.Final * (float)h.Stats.DamageMage.Bonus * h.Stats.DamageModifier.Final);
			}
		}
	}

	public static void CalcStats(Hero h)
	{
		CalcBaseStats(h);
		CalcEquippedStats(h);
		CalcFinalStats(h);
		ResetCurrent(h);
	}

	public static long CalcExpToNextLevel(int currentLevel)
	{
		const float baseExpToLevel = 100.00f;
		int nextLevel = currentLevel + 1;
		float currentTarget = baseExpToLevel;

		for (int i = 1; i < nextLevel; i++)
		{
			currentTarget *= 1.075f;
		}
		return (long)currentTarget;
	}

	public static void LevelUp(Hero h)
	{
		bool enoughExp = true;
		while (enoughExp)
		{
			if (h.CurrentExp < h.ExpToLevel) { enoughExp = false; continue; }
			h.IncrementLevel(1);
			h.GiveAbilityPoints(3);
			h.ExpToLevel = CalcExpToNextLevel(h.Level);
		}
		CalcStats(h);
	}
}
