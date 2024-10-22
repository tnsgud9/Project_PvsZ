using Collections;

namespace Entities.UnitState
{
    public class IdleState : IState<Unit>
    {
        public void Start(Unit unit)
        {
            unit.StartIdle();
        }

        public void Handle(Unit unit)
        {
            unit.Idle(); // 대기
        }

        public void End(Unit unit)
        {
            unit.EndIdle();
        }
    }
}