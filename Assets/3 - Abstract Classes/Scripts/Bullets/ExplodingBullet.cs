using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class ExplodingBullet : Bullet
    {
        public int explodingDamage;
        public float explodingRadius;
        public GameObject explosion;

        public override void Fire(Vector3 direction, float? speed = null)
        {
            float currentSpeed = this.speed;
            //Check if speed has been used
            if (speed != null)
            {
                currentSpeed = speed.Value;
            }
            //Fire off in that direction with currentSpeed
            rigid.AddForce(direction * currentSpeed, ForceMode2D.Impulse);
        }

        void Explode()
        {
            Instantiate(explosion, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Explode();
            }
        }
    }
}