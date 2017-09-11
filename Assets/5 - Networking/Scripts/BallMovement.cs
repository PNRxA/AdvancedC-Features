using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class BallMovement : NetworkBehaviour
    {
        public float speed = 20f;
        public Camera currentCam;
        Rigidbody rigid;

        // Use this for initialization
        void Start()
        {
            if (!isLocalPlayer)
            {
                Destroy(currentCam.gameObject);
            }
        }

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                Control();
            }
        }

        void Control()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            Vector3 euler = currentCam.transform.eulerAngles;

            Quaternion localRot = Quaternion.Euler(0f, euler.y, 0f);
            Vector3 inputDir = localRot * new Vector3(inputH, 0, inputV);
            rigid.AddForce(inputDir * speed);
        }
    }
}
