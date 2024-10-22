using System.Collections;
using Collections;
using Commons;
using Entities.UnitState;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Unit : MonoBehaviour
{
    public int health;

    [FormerlySerializedAs("attackCooldown")]
    public float eventCooldown;

    [FormerlySerializedAs("attackTimer")] public float eventTimer;
    public readonly IState<Unit> IdleState = new IdleState();
    public IState<Unit> AttackingState = new AttackingState();
    public IState<Unit> DyingState = new DyingState();
    public IState<Unit> MovingState = new MovingState();
    public StateContext<Unit> State;

    /// <summary>
    ///     컴포넌트 초기화 및 유니티 자원들의 초기화
    /// </summary>
    protected virtual void Awake()
    {
        State = new StateContext<Unit>(this);
        State.CurrentState = IdleState; // 초기 상태는 Idle
    }


    /// <summary>
    ///     유닛의 설정 값 혹은 상태 초기화
    /// </summary>
    protected virtual void Start()
    {
    }

    private void Update()
    {
        State.CurrentState.Handle(this);
    }


    public virtual int TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) State.CurrentState = DyingState; // 체력이 0이면 죽음 상태로 전환
        return health;
    }


    public virtual void StartIdle()
    {
    }

    public virtual void StartAttack()
    {
    }

    public virtual void StartMove()
    {
    }

    public virtual void StartDie()
    {
        Destroy(gameObject);
    }


    public virtual void Idle()
    {
    }

    public virtual void Attack()
    {
    }

    public virtual void Move()
    {
    }

    public virtual void Die()
    {
    }

    public virtual void EndIdle()
    {
    }

    public virtual void EndAttack()
    {
    }

    public virtual void EndMove()
    {
    }

    public virtual void EndDie()
    {
    }
}