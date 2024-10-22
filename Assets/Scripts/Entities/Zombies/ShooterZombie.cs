using Commons;
using UnityEngine;

namespace Entities.Zombies
{
    public class ShooterZombie : Zombie
    {
        public float projectileTime;
        public float projectileTimeDelay;
        public GameObject projectilePrefab;

        public override void Move()
        {
            base.Move();
            projectileTime -= Time.deltaTime;
            var distance = Vector3.Distance(Vector3.right * transform.position.x,
                Vector3.left * Const.DetectRayDistance);

            if (projectileTime <= 0 && transform.position.x <= Const.DetectRayDistance)
            {
                Debug.DrawRay(transform.position, Vector3.left * distance, Color.cyan);
                // 이벤트 발생
                var ptObj = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                ptObj.layer = gameObject.layer;
                var pt = ptObj.GetComponent<Projectile>();
                pt.Color = Color.red;
                pt.direction = Vector3.left;
                // AudioSource.PlayOneShot(fireAudio);
                // 공격 딜레이 다시 설정
                projectileTime = projectileTimeDelay;
            }
        }
    }
}