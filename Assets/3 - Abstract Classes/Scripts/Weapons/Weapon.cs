using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public abstract class Weapon : MonoBehaviour
    {
        public GameObject muzzleFlash;
        public GameObject bulletPrefab;

        public int damage = 10;
        public int ammo = 0;
        public int maxAmmo = 30;
        public float fireInterval = .2f;
        public float recoil = 5f;
        public bool isReady = true;

        public abstract void Fire();

        public virtual void Reload()
        {
            ammo = maxAmmo;
        }

        public Bullet SpawnBullet(Vector3 pos, Quaternion rot)
        {
            //Instantiate a new bullet
            GameObject clone = Instantiate(bulletPrefab, pos, rot);
            Bullet b = clone.GetComponent<Bullet>();
            //>>Play sound here<<
            //>>Play MuzzleFlash<<
            Instantiate(muzzleFlash, pos, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            //Reduce current ammo by 1
            ammo--;
            //Return new bullet
            return b;
        }

    }
}
