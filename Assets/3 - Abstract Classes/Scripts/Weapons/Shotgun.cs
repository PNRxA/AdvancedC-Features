using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Preprocessor Directives
#if UNITY_EDITOR    
using UnityEditor;
#endif

namespace AbstractClasses
{
    public class Shotgun : Weapon
    {
        public int shells = 5;
        public float shootAngle = 45f;
        public float shootRadius = 5f;

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
                Bullet b = SpawnBullet(transform.position, transform.rotation);
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

#if UNITY_EDITOR
    [CustomEditor(typeof(Shotgun))]
    public class ShotgunEditor : Editor
    {
        void OnSceneGUI()
        {
            Shotgun shotgun = (Shotgun)target;

            Transform transform = shotgun.transform;
            Vector2 pos = transform.position;

            float angle = shotgun.shootAngle;
            float radius = shotgun.shootRadius;

            Vector2 leftDir = shotgun.GetDir(angle);
            Vector2 rightDir = shotgun.GetDir(-angle);

            Handles.color = Color.red;
            Handles.DrawLine(pos, pos + rightDir * radius);

            Handles.color = Color.green;
            Handles.DrawLine(pos, pos + leftDir * radius);

            Handles.color = Color.gray;
            Handles.DrawWireArc(pos, Vector3.forward, rightDir, angle * 2, radius);
        }
    }
#endif
}