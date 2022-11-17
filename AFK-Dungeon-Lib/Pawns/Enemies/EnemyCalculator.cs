using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFK_Dungeon_Lib.Pawns.Enemies
{
	public static class EnemyCalculator
	{
		public static void CalcStats(Enemy e)
		{
			e.Stats.Health.Final = e.Stats.Health.Initial + (int)MathFunc.Round(0.2f * e.Level * e.Stats.Vitality.Final, 0);
			e.Stats.Defense.Final = e.Stats.Defense.Initial + (e.Stats.Fortitude.Final * 2);
			e.Stats.Resistance.Final = e.Stats.Resistance.Initial + (e.Stats.Will.Final * 2);
			e.Stats.AttackSpeed.Final = e.Stats.AttackSpeed.Initial;

			if (e.PhysicalDamage)
			{
				if (!e.AlternateDamage)
				{
					e.Stats.Damage.Final = e.Stats.Damage.Initial + (int)MathFunc.Round(0.2f * e.Level * e.Stats.Strength.Final, 0);
				}
				else
				{
					e.Stats.Damage.Final = (int)MathFunc.Round(0.2f * e.Level * e.Stats.Dexterity.Final, 0);
				}
				e.Stats.CritChance.Final = e.Stats.CritChance.Initial + (e.Stats.Dexterity.Final / 100f);
				e.Stats.CritDamage.Final = e.Stats.CritDamage.Initial + (0.02f * e.Level);
			}
			else
			{
				if (!e.AlternateDamage)
				{
					e.Stats.Damage.Final = e.Stats.Damage.Initial + (int)MathFunc.Round(0.2f * e.Level * e.Stats.Intelligence.Final, 0);
				}
				else
				{
					e.Stats.Damage.Final = (int)MathFunc.Round(0.2f * e.Level * e.Stats.Wisdom.Final, 0);
				}
				e.Stats.CritChance.Final = e.Stats.CritChance.Initial + (e.Stats.Wisdom.Final / 100f);
				e.Stats.CritDamage.Final = e.Stats.CritDamage.Initial + (0.02f * e.Level);
			}
		}

		public static void ResetCurrent(Enemy e)
		{
			e.Stats.Strength.SetValue(StatStateEnum.Current, e.Stats.Strength.Final);
			e.Stats.Dexterity.SetValue(StatStateEnum.Current, e.Stats.Dexterity.Final);
			e.Stats.Vitality.SetValue(StatStateEnum.Current, e.Stats.Vitality.Final);
			e.Stats.Intelligence.SetValue(StatStateEnum.Current, e.Stats.Intelligence.Final);
			e.Stats.Wisdom.SetValue(StatStateEnum.Current, e.Stats.Wisdom.Final);
			e.Stats.Fortitude.SetValue(StatStateEnum.Current, e.Stats.Fortitude.Final);
			e.Stats.Will.SetValue(StatStateEnum.Current, e.Stats.Will.Final);
			e.Stats.Health.SetValue(StatStateEnum.Current, e.Stats.Health.Final);
			e.Stats.Defense.SetValue(StatStateEnum.Current, e.Stats.Defense.Final);
			e.Stats.Resistance.SetValue(StatStateEnum.Current, e.Stats.Resistance.Final);
			e.Stats.Damage.SetValue(StatStateEnum.Current, e.Stats.Damage.Final);
			e.Stats.CritChance.SetValue(StatStateEnum.Current, e.Stats.CritChance.Final);
			e.Stats.CritDamage.SetValue(StatStateEnum.Current, e.Stats.CritDamage.Final);
			e.Stats.AttackSpeed.SetValue(StatStateEnum.Current, e.Stats.AttackSpeed.Final);
		}

		public static void InitializeEnemy(Enemy e)
		{
			e.Stats.Strength.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Strength.Initial, e.Level));
			e.Stats.Dexterity.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Dexterity.Initial, e.Level));
			e.Stats.Vitality.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Vitality.Initial, e.Level));
			e.Stats.Intelligence.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Intelligence.Initial, e.Level));
			e.Stats.Wisdom.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Wisdom.Initial, e.Level));
			e.Stats.Fortitude.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Fortitude.Initial, e.Level));
			e.Stats.Will.SetValue(StatStateEnum.Final, AbilityStatScaling(e.Stats.Will.Initial, e.Level));
		}
		public static int AbilityStatScaling(int originalValue, int level)
		{
			return originalValue + level;
		}
	}
}