using System;
using System.Linq;
using Entities.Plants;
using UnityEngine;

namespace Events
{
    public class GroundDamage : MonoBehaviour
    {
        public int damage=1;
        public float timer=0f;
        public float timerCooldown=3f;

        void Start()
        {
            timer = timerCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            timer-=Time.deltaTime;
            if (timer <= 0)
            {
                foreach (var plant in GameObject.FindObjectsOfType<Plant>().ToList())
                {
                    plant.TakeDamage(damage);
                    
                }
                timer = timerCooldown;

            }
        }
    }
}
