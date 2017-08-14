using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<Transform> targets = new List<Transform>();
        public List<GameObject> prefabs = new List<GameObject>();
        public int minAmount = 0, maxAmount = 20;
        public float spawnRate = 5f;
        public Transform target;


        delegate void SpawnType(int amount, Vector3 spawnPos);
        List<SpawnType> spawnType = new List<SpawnType>();

        void Awake()
        {
            InvokeRepeating("SpawnFunc", spawnRate, spawnRate);

            spawnType.Add(SpawnOrc);
            spawnType.Add(SpawnTroll);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnFunc()
        {
            int RR = Random.Range(0, 2);
            int amount = Random.Range(minAmount, maxAmount);
            var spawnPos = targets[Random.Range(0, 2)].position;
            spawnType[RR].Invoke(amount, spawnPos);
        }

        void SpawnOrc(int amount, Vector3 spawnPos)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject clone = Instantiate(prefabs[0], spawnPos, transform.rotation);
                clone.GetComponent<Orc>().SetTarget(target);
            }
        }

        void SpawnTroll(int amount, Vector3 spawnPos)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject clone = Instantiate(prefabs[1], spawnPos, transform.rotation);
                clone.GetComponent<Troll>().SetTarget(target);
            }
        }
    }
}
