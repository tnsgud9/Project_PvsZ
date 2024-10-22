using Collections;

namespace Entities.UnitState
{
    public class DyingState : IState<Unit>
    {
        private IState<Unit> _stateImplementation;

        public void Start(Unit unit)
        {
            unit.StartDie();
        }

        public void Handle(Unit unit)
        {
            unit.Die(); // 사망 처리
        }

        public void End(Unit unit)
        {
            unit.EndDie();
        }
    }
}