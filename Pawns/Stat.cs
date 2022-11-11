using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFK_Dungeon_Lib.Pawns;
public struct Stat<T>
{
	public T Initial;
	public T Bonus;
	public T Final;
	public T Current;
	public Stat(T initial, T bonus, T final, T current)
	{
		this.Initial = initial;
		this.Bonus = bonus;
		this.Final = final;
		this.Current = current;
	}

	public void SetValue(StatStateEnum state, T value)
	{
		switch (state)
		{
			case StatStateEnum.Initial: Initial = value; break;
			case StatStateEnum.Bonus: Bonus = value; break;
			case StatStateEnum.Final: Final = value; break;
			case StatStateEnum.Current: Current = value; break;
			default: break;
		}
	}

	public T GetValue(StatStateEnum state)
	{
		return state switch
		{
			StatStateEnum.Initial => Initial,
			StatStateEnum.Bonus => Bonus,
			StatStateEnum.Final => Final,
			StatStateEnum.Current => Current,
			_ => throw new ArgumentOutOfRangeException(nameof(state), $"State is not valid: {state}"),
		};
	}
}