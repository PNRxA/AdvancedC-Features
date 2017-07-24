using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics
{
    public class GenericsTest : MonoBehaviour
    {
        public Camera cam;
        public GameObject prefab;
        public int spawnAmount = 20;
        public float spawnRadius = 50f;
        public CustomList<GameObject> gameObjects = new CustomList<GameObject>();

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject clone = Instantiate(prefab);
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
                clone.transform.position = randomPos;
                clone.name = "clone" + i;
                gameObjects.Add(clone);
                print("added " + gameObjects[i].name);
                print(gameObjects.amount);
            }
        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    //print(objectHit.name + " removed from list.");

                    for (int i = 0; i < gameObjects.amount; i++)
                    {
                        print(gameObjects[i].name);
                    }
                    print(gameObjects.Contains(objectHit.gameObject));
                    gameObjects.Clear();
                }
            }

        }
    }
}
