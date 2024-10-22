using Collections;

namespace Entities.UnitState
{
    public class MovingState : IState<Unit>
    {
        public void Start(Unit unit)
        {
            unit.StartMove();
        }

        public void Handle(Unit unit)
        {
            unit.Move(); // 이동 로직 수행
        }

        public void End(Unit unit)
        {
            unit.EndMove();
        }
    }
}