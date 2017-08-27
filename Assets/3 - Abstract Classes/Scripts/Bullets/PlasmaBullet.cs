using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class PlasmaBullet : Bullet
    {
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
    }
}