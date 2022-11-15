using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Controllers;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

public class HeroEntity : IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public IPawn? Target { get; set; }

	public float AttackSpeed;
	public float TimeBetweenAttacks;
	public float TimeEllapsed;

	public HeroEntity(Hero h, Coordinate position)
	{
		Entity = h;
		Position = position;
		TimeBetweenAttacks = 1f / h.Stats.AttackSpeed.Current;
		TimeEllapsed = 0f;
	}
	public void GetTarget(List<IPawn> targets)
	{
		throw new NotImplementedException();
	}

	public void NextStep()
	{
		switch (EntityState)
		{
			case EntityState.Wait: Wait(); break;
			case EntityState.TakeAction: TakeAction(); break;
			case EntityState.Dead: Kill(); break;
			case EntityState.Incapacitated: break;
			case EntityState.Untargetable: TakeAction(); break;
			default: break;
		}
	}

	public void Wait()
	{
		TimeEllapsed += 0.02f;
		if (TimeEllapsed >= TimeBetweenAttacks)
		{
			EntityState = EntityState.TakeAction;
			TimeEllapsed -= TimeBetweenAttacks;
		}
	}
	public void TakeAction()
	{

	}

	public void Kill()
	{

	}
}
