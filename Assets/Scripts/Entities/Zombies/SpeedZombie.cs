using UnityEngine;

namespace Entities.Zombies
{
    public class SpeedZombie : Zombie
    {
        public float acceleration = 0.1f; // 가속도
        public float maxSpeed = 3f; // 최대 속도


        public override void StartAttack()
        {
            base.StartAttack();
            Animator.speed = animSpeed;
        }

        public override void Move()
        {
            // 속도에 가속도를 더함 (deltaTime으로 프레임마다 속도 일정하게 유지)
            speed += acceleration * Time.deltaTime;

            // 속도가 최대 속도를 넘지 않도록 제한
            speed = Mathf.Clamp(speed, 0, maxSpeed);

            Animator.speed = animSpeed * speed;

            // 물체를 앞으로 이동시킴
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }
}