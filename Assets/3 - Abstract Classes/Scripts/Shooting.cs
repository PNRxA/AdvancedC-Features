using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class Shooting : MonoBehaviour
    {
        public int weaponIndex = 0;

        private Weapon[] attachedWeapon;
        private Rigidbody2D rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            attachedWeapon = GetComponentsInChildren<Weapon>();
        }

        void Start()
        {
            SwitchWeapon(weaponIndex);
        }

        // Update is called once per frame
        void Update()
        {
            CheckFire();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleWeapon(-1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleWeapon(1);
            }
        }

        void CheckFire()
        {
			//Set currentWeapon to attachedWeaons[weaponIndex]
			Weapon currentWeapon = attachedWeapon[weaponIndex];
			//If space is down 
			if (Input.GetKeyDown(KeyCode.Space))
			{
				//fire currentWeapon
				currentWeapon.Fire();
				//Apply currentWeapon's recoil to player
				rigid.AddForce(-transform.right * currentWeapon.recoil, ForceMode2D.Impulse);
			}
        }

        void CycleWeapon(int amount)
        {
            //Set desiredIndex to weaponIndex + amount
            int desiredIndex = weaponIndex + amount;
            //If desiredIndex >= weapons length
            if (desiredIndex >= attachedWeapon.Length)
            {
                //Set desiredIndex to zero
                desiredIndex = 0;
            }
            //Else if desiredIndex < 0
            else if (desiredIndex < 0)
            {
                //Set desiredIndex to weapons length - 1
                desiredIndex = attachedWeapon.Length - 1;
            }
            //Set weaponIndex to desiredIndex
            weaponIndex = desiredIndex;
            //SwitchWeapon() and pass weaponIndex
            SwitchWeapon(desiredIndex);
        }

        Weapon SwitchWeapon(int weaponIndex)
        {
            //Check bounds
            if (weaponIndex < 0 || weaponIndex > attachedWeapon.Length)
            {
                //Return null as error
                return null;
            }

            for (int i = 0; i < attachedWeapon.Length; i++)
            {
                Weapon w = attachedWeapon[i];
                if (i == weaponIndex)
                {
                    w.gameObject.SetActive(true);
                }
                else
                {
                    w.gameObject.SetActive(false);
                }
            }
            return attachedWeapon[weaponIndex];
        }
    }
}
