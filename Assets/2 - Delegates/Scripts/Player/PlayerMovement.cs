using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 200f;
        public float deceleration = .01f;
        private Rigidbody rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Accelerate();
            Decelerate();
        }

        void Accelerate()
        {
            //Get input
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            //Calculate input direction
            Vector3 inputDir = new Vector3(inputH, 0, inputV);

            //Add force in direction by acceleration
            rigid.AddForce(inputDir * acceleration);
        }

        void Decelerate()
        {
            //Velocity = -velocity * deceleration
            rigid.velocity = -rigid.velocity * deceleration;
        }
    }
}
