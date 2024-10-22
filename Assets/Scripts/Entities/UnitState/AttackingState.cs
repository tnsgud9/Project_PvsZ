using Collections;

namespace Entities.UnitState
{
    public class AttackingState : IState<Unit>
    {
        public void Start(Unit unit)
        {
            unit.StartAttack();
        }

        public void Handle(Unit unit)
        {
            unit.Attack(); // 공격 로직 수행
        }

        public void End(Unit unit)
        {
            unit.EndAttack();
        }
    }
}