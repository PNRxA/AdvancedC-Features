using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recursion
{
    public class RecursiveSpawner : MonoBehaviour
    {
        public GameObject spawnPrefab;
        public int amount = 100;
        public float positionOffset = 5f;
        public float scaleFactor = .7f;

        // Use this for initialization
        void Start()
        {
            Vector3 position = transform.position;
            Vector3 scale = spawnPrefab.transform.localScale;
            RecursiveSpawn(amount, position, scale);
        }

        void RecursiveSpawn(int currentAmount, Vector3 position, Vector3 scale)
        {
            amount--; // Decrement amount

            if (amount <= 0) return;

            Vector3 adjustedScale = scale * (1 - scaleFactor);
            Vector3 adjustedPosition = position + Vector3.up * adjustedScale.magnitude;
            GameObject clone = Instantiate(spawnPrefab);
            clone.transform.position = adjustedPosition;
            clone.transform.localScale = adjustedScale;

            RecursiveSpawn(amount, adjustedPosition, adjustedScale);
        }
    }
}
