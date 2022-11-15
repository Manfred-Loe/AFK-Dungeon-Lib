using System.Runtime.InteropServices.ComTypes;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Dungeon.DungeonObjects;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

namespace AFK_Dungeon_Lib.AI;

internal static class Targeter

{
	public static Coordinate GetTarget(IPawnEntity self, List<IPawnEntity> targets, DungeonRandom rand)
	{
		Random random = rand.Random;
		TargetPriority priority = TargetPriority.Closest;

		if (self.Entity is Hero h)
		{
			if (h.EquippedMainHand == null)
			{
				priority = TargetPriority.Closest;
			}
			else if (!h.RangedWeapon)
			{
				priority = TargetPriority.Closest;
			}
			else if (h.RangedWeapon && h.Phys)
			{
				priority = h.Priority;
			}
			else
			{
				return GetMageTarget(self, targets, random);
			}
		}
		else if (self.Entity is Enemy e)
		{
			priority = e.Priority;
		}
		return priority switch
		{
			TargetPriority.Closest => GetClosest(self, targets, random),
			TargetPriority.Furthest => GetFurthest(self, targets, random),
			TargetPriority.Frontline => GetFrontline(self, targets, random),
			TargetPriority.Backline => GetBackline(self, targets, random),
			TargetPriority.Weakest => GetWeakest(targets, random),
			TargetPriority.Strongest => GetSrongest(targets, random),
			_ => GetClosest(self, targets, random)
		};
	}
	public static Coordinate GetMageTarget(IPawnEntity self, List<IPawnEntity> targets, Random random)
	{
		SpellModifier modifier = SpellModifier.None;

		if (self is Hero h)
		{
			if (h.EquippedOffhand is Offhand offhand)
			{
				modifier = offhand.Modifier;
			}
		}
		else
		{
			return GetClosest(self, targets, random);
		}

		return modifier switch
		{
			SpellModifier.None => GetClosest(self, targets, random),
			SpellModifier.Point => GetClosest(self, targets, random),
			SpellModifier.Line => GetLineTarget(targets),
			SpellModifier.Cube => GetCubeTarget(targets, random),
			_ => GetClosest(self, targets, random),
		};
	}

	//complicated - prioritizes highest in square area
	// if tied among square areas, targets the one with the most in the front line
	// if the front line is the same, randomly assign it.
	public static Coordinate GetCubeTarget(List<IPawnEntity> targets, Random random)
	{
		var validTargets = new List<Coordinate>();
		Coordinate top, middle, bottom;
		var row = new int[4] { 0, 0, 0, 0 };
		var column = new int[4] { 0, 0, 0, 0 };
		var squareFrontline = new int[3] { 0, 0, 0 };
		for (int i = 0; i < targets.Count; i++)
		{
			switch (targets[i].Position.Y)
			{
				case 0: row[0]++; break;
				case 1: row[1]++; break;
				case 2: row[2]++; break;
				case 3: row[3]++; break;
				default: break;
			}
			switch (targets[i].Position.X)
			{
				case 0:
					column[0]++;
					switch (targets[i].Position.Y)
					{
						case 0: squareFrontline[0]++; break;
						case 1: squareFrontline[0]++; squareFrontline[1]++; break;
						case 2: squareFrontline[1]++; squareFrontline[2]++; break;
						case 3: squareFrontline[2]++; break;
						default: break;
					}
					break;
				case 1:
					column[1]++;
					switch (targets[i].Position.Y)
					{
						case 0: squareFrontline[0]++; break;
						case 1: squareFrontline[0]++; squareFrontline[1]++; break;
						case 2: squareFrontline[1]++; squareFrontline[2]++; break;
						case 3: squareFrontline[2]++; break;
						default: break;
					}
					break;
				case 2:
					column[2]++;
					switch (targets[i].Position.Y)
					{
						case 0: squareFrontline[0]++; break;
						case 1: squareFrontline[0]++; squareFrontline[1]++; break;
						case 2: squareFrontline[1]++; squareFrontline[2]++; break;
						case 3: squareFrontline[2]++; break;
						default: break;
					}
					break;
				case 3:
					column[3]++;
					switch (targets[i].Position.Y)
					{
						case 0: squareFrontline[0]++; break;
						case 1: squareFrontline[0]++; squareFrontline[1]++; break;
						case 2: squareFrontline[1]++; squareFrontline[2]++; break;
						case 3: squareFrontline[2]++; break;
						default: break;
					}
					break;
				default: break;
			}
		}
		var squares = new int[3]
		{
			row[0] + row[1],
			row[1] + row[2],
			row[2] + row[3]
		};

		//se the top/middle/bottom squares
		if (column[0] == 0 && column[1] == 0)
		{
			top = new(2, 0);
			middle = new(2, 1);
			bottom = new(2, 2);
		}
		else
		{
			top = new(0, 0);
			middle = new(0, 1);
			bottom = new(0, 2);
		}

		//return the top left of the square that's got the highest count
		if (squares[0] > squares[1] && squares[0] > squares[2])
		{
			return top;
		}
		else if (squares[1] > squares[0] && squares[1] > squares[2])
		{
			return middle;
		}
		else if (squares[2] > squares[0] && squares[2] > squares[1])
		{
			return bottom;
		}

		//if some are tied, return the on with the most in the front line
		if (squareFrontline[0] > squareFrontline[1] && squareFrontline[0] > squareFrontline[2])
		{
			return top;
		}
		else if (squareFrontline[1] > squareFrontline[0] && squareFrontline[1] > squareFrontline[2])
		{
			return middle;
		}
		else if (squareFrontline[2] > squareFrontline[0] && squareFrontline[2] > squareFrontline[1])
		{
			return bottom;
		}
		//if that's a tie, find the ones that are tied for the most, and return randomly
		int count = 0;
		Coordinate[] squareCoord = new Coordinate[3] { top, middle, bottom };
		for (int i = 0; i < 3; i++)
		{
			if (squares[i] > count)
			{
				count = squares[i];
				validTargets.Clear();
				validTargets.Add(squareCoord[i]);
			}
			else if (squares[i] == count)
			{
				validTargets.Add(squareCoord[i]);
			}
		}
		return validTargets[random.Next(validTargets.Count)];
	}
	public static Coordinate GetLineTarget(List<IPawnEntity> targets)
	{
		int countFrontline = 0;
		int countBackline = 0;
		for (int i = 0; i < targets.Count; i++)
		{
			if (targets[i].Position.X == 1 || targets[i].Position.X == 2)
			{
				countFrontline++;
			}
			else
			{
				countBackline++;
			}
		}
		if (countFrontline >= countBackline)
		{
			return targets.Find(x => x.Position.X == 1 || x.Position.X == 2)!.Entity.Position;
		}
		else
		{
			return targets.Find(x => x.Position.X == 0 || x.Position.X == 3)!.Entity.Position;
		}
	}
	public static Coordinate GetClosest(IPawnEntity self, List<IPawnEntity> targets, Random random)
	{
		Coordinate myPosition = self.Position;
		int distance = 100;
		var validTargets = new List<IPawnEntity>();
		for (int i = 0; i < targets.Count; i++)
		{
			int tempDistance = MathFunc.Distance(myPosition, targets[i].Position);
			if (tempDistance < distance)
			{
				distance = tempDistance;
				validTargets.Clear();
				validTargets.Add(targets[i]);
			}
			else if (tempDistance == distance)
			{
				validTargets.Add(targets[i]);
			}
		}
		return validTargets[random.Next(validTargets.Count)].Entity.Position;
	}
	public static Coordinate GetFurthest(IPawnEntity self, List<IPawnEntity> targets, Random random)
	{
		Coordinate myPosition = self.Position;
		int distance = 0;
		var validTargets = new List<IPawnEntity>();
		for (int i = 0; i < targets.Count; i++)
		{
			int tempDistance = MathFunc.Distance(myPosition, targets[i].Position);
			if (tempDistance > distance)
			{
				distance = tempDistance;
				validTargets.Clear();
				validTargets.Add(targets[i]);
			}
			else if (tempDistance == distance)
			{
				validTargets.Add(targets[i]);
			}
		}
		return validTargets[random.Next(validTargets.Count)].Entity.Position;
	}
	public static Coordinate GetFrontline(IPawnEntity self, List<IPawnEntity> targets, Random random)
	{
		int xFrontline;
		var validTargets = new List<IPawnEntity>();
		if (self is Hero)
		{
			xFrontline = 2;
		}
		else
		{
			xFrontline = 1;
		}

		for (int i = 0; i < targets.Count; i++)
		{
			if (targets[i].Position.X == xFrontline)
			{
				validTargets.Add(targets[i]);
			}
		}
		if (validTargets.Count == 0)
		{
			return GetClosest(self, targets, random);
		}
		else
		{
			return GetClosest(self, validTargets, random);
		}
	}
	public static Coordinate GetBackline(IPawnEntity self, List<IPawnEntity> targets, Random random)
	{
		int xBackline;
		var validTargets = new List<IPawnEntity>();
		if (self is Hero)
		{
			xBackline = 3;
		}
		else
		{
			xBackline = 0;
		}

		for (int i = 0; i < targets.Count; i++)
		{
			if (targets[i].Position.X == xBackline)
			{
				validTargets.Add(targets[i]);
			}
		}
		if (validTargets.Count == 0)
		{
			return GetClosest(self, targets, random);
		}
		else
		{
			return GetClosest(self, validTargets, random);
		}
	}
	public static Coordinate GetWeakest(List<IPawnEntity> targets, Random random)
	{
		int health = -1;
		var validTargets = new List<IPawnEntity>();
		for (int i = 0; i < targets.Count; i++)
		{
			if (targets[i] is Enemy e)
			{
				if (e.Stats.Health.Current < health)
				{
					validTargets.Clear();
					validTargets.Add(targets[i]);
					health = e.Stats.Health.Current;
				}
				else if (e.Stats.Health.Current == health)
				{
					validTargets.Add(targets[i]);
				}
				else if (health < 0)
				{
					validTargets.Add(targets[i]);
					health = e.Stats.Health.Current;
				}
			}
			else if (targets[i] is Hero h)
			{
				if (h.Stats.Health.Current < health)
				{
					validTargets.Add(targets[i]);
					health = h.Stats.Health.Current;
				}
				else if (h.Stats.Health.Current == health)
				{
					validTargets.Add(targets[i]);
				}
				else if (health < 0)
				{
					validTargets.Add(targets[i]);
					health = h.Stats.Health.Current;
				}
			}
		}
		return validTargets[random.Next(validTargets.Count)].Entity.Position;
	}
	public static Coordinate GetSrongest(List<IPawnEntity> targets, Random random)
	{
		int health = -1;
		var validTargets = new List<IPawnEntity>();
		for (int i = 0; i < targets.Count; i++)
		{
			if (targets[i] is Enemy e)
			{
				if (e.Stats.Health.Current > health)
				{
					validTargets.Clear();
					validTargets.Add(targets[i]);
					health = e.Stats.Health.Current;
				}
				else if (e.Stats.Health.Current == health)
				{
					validTargets.Add(targets[i]);
				}
			}
			else if (targets[i] is Hero h)
			{
				if (h.Stats.Health.Current > health)
				{
					validTargets.Add(targets[i]);
					health = h.Stats.Health.Current;
				}
				else if (h.Stats.Health.Current == health)
				{
					validTargets.Add(targets[i]);
				}
			}
		}
		return validTargets[random.Next(validTargets.Count)].Entity.Position;
	}
}
