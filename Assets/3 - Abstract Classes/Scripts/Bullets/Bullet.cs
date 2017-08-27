using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float aliveDistance = 5f;
        public Rigidbody2D rigid;
        private Vector3 shotPos;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            shotPos = transform.position;
        }

        public abstract void Fire(Vector3 direction, float? speed = null);
    }
}
