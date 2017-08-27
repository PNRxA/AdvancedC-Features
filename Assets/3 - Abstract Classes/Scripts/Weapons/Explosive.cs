using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class Explosive : Weapon
    {
        public int shells = 1;
        public float shootAngle = 10f;
        public float shootRadius = 5f;
        public int explosionDamage = 1;
        public float explosionRadius = 1f;
        private Bullet b;

        public Vector2 GetDir(float angleD)
        {
            float angleR = angleD * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angleR), Mathf.Sin(angleR));
            return transform.rotation * dir;
        }

        public override void Fire()
        {
            //Loop through shells
            for (int i = 0; i < shells; i++)
            {
                //Set b to spawnbullet()
                b = SpawnBullet(transform.position, transform.rotation);
                //Set randomAngle to random range between -shootAngle to shootAngle
                float randomAngle = Random.Range(-shootAngle, shootAngle);
                //Set direction to GetDir() and pass randomAngle
                Vector2 direction = GetDir(randomAngle);
                //Set b's aliveDistance to shootRadius
                b.aliveDistance = shootRadius;
                //Call b's Fire() and pass direction.
                b.Fire(direction);
            }
        }
    }
}