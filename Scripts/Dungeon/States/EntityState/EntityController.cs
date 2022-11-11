using System.Collections;
using System.Collections.Generic;

public class EntityController : MonoBehaviour
{
	/*
     * #wait#
     * #attack#
     * #dead#
     * #incapacitated#
     * #Untargetable# 
     * 
     * TimeBetweenAttacks
     * 
     * 
    */
	public int EntityID { get; set; }
	public float TimeBetweenAttacks { get; set; }
	public float Timer { get; set; }

	//references
	private DungeonDriver _dd;
	private GameEntity _gameEntity;
	public bool Hero { get; set; }
	public Coordinate TargetPosition { get; set; }
	public bool TargetSet;

	private EntityState _currentState;

	//TimeBetweenAttacks Needs to Be Set
	void Start()
	{
		Timer = 0f;
		Hero = true;
	}

	// Update is called once per frame
	void Update()
	{
		switch (_currentState)
		{
			case EntityState.Wait: Wait(); break;
			case EntityState.Attack: Attack(); break;
			case EntityState.Dead: Dead(); break;
			case EntityState.Incapacitated: Incapacitated(); break;
			case EntityState.Untargetable: Untargetable(); break;
			default: Wait(); break;
		}
	}

	public void Setup(DungeonDriver d, GameEntity ge)
	{
		_dd = d;
		_gameEntity = ge;
		//check if hero/enemy
		if (ge.GetType() == typeof(HeroGameEntity))
		{
			_gameEntity = (HeroGameEntity)ge;
			Hero = true;
		}

		if (ge.GetType() == typeof(EnemyGameEntity))
		{
			_gameEntity = (EnemyGameEntity)ge;
			Hero = false;
		}
	}

	public void Wait()
	{
		//countdown to attack
		Timer -= Time.deltaTime;
		if (Timer <= 0f)
		{
			Timer = TimeBetweenAttacks + Timer; //accounts for negative counting towards next attack
			_currentState = EntityState.Attack;
		}
		//pick target
		if (!TargetSet)
		{
			TargetSet = true;
			PickTarget();
		}
	}

	public void Attack()
	{
		//still subtract from timer
		//this negates the animation time
		Timer -= Time.deltaTime;
		//validate target and if invalid pick new target

		//attack target
	}

	public void Dead()
	{

	}

	public void Incapacitated()
	{

	}

	public void Untargetable()
	{

	}

	public void PickTarget()
	{
		if (Hero)
		{
			//_dd.GetEnemyTarget();
		}
		else
		{

		}

	}

}
