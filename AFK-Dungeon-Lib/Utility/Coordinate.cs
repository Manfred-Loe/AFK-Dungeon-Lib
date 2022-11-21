using System;

namespace AFK_Dungeon_Lib.Utility;

public struct Coordinate
{
	public int X;
	public int Y;

	public Coordinate(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}
	public static Coordinate Up => new(0, 1);
	public static Coordinate Right => new(1, 0);
	public static Coordinate Down => new(0, -1);
	public static Coordinate Left => new(-1, 0);

	public static Coordinate Cardinal(int x)
	{
		if (x == 0)
		{
			return Up;
		}
		else if (x == 1)
		{
			return Right;
		}
		else if (x == 2)
		{
			return Down;
		}
		else if (x == 3)
		{
			return Left;
		}
		else
		{
			return new Coordinate(0, 0);
		}
	}

	public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.X + b.X, a.Y + b.Y);
	public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.X - b.X, a.Y - b.Y);
	public static Coordinate operator *(Coordinate a, Coordinate b) => new(a.X * b.X, a.Y * b.Y);
	public static Coordinate operator *(Coordinate coord, int value) => new(coord.X * value, coord.Y * value);
	public static Coordinate operator *(int value, Coordinate coord) => new(coord.X * value, coord.Y * value);
	public static Coordinate operator /(Coordinate a, Coordinate b) => new(a.X / b.X, a.Y / b.Y);
	public static Coordinate operator /(Coordinate a, int value) => new(a.X + value, a.Y + value);
	public override string ToString() => X.ToString() + ", " + Y.ToString();
	public bool Equals(Coordinate c) { return X == c.X && Y == c.Y; }
}