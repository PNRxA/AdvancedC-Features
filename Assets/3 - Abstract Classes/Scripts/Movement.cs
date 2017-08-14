using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public float acceleration = 25f;
        public float hyperSpeed = 150f;
        public float deceleration = .1f;
        public float rotationSpeed = 5f;

        private Rigidbody2D rigid;
        private float inputH, inputV;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");
        }

        void FixedUpdate()
        {
            Accelerate();
            Decelerate();
            Rotate();
        }

        void Accelerate()
        {
            Vector2 force = transform.right * inputV;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                force *= hyperSpeed;
            }
            else
            {
                force *= acceleration;
            }
            rigid.AddForce(force);
        }

        void Decelerate()
        {
            //velocity += -velocity * deceleration
            rigid.velocity += -rigid.velocity * deceleration;
        }

        void Rotate()
        {
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * inputH, Vector3.back);
        }
    }
}