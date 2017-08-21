using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public int frameDelay = 1;
    private int frameCount = 0;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (frameCount > frameDelay)
		{
			Destroy(gameObject);
		}
    }

	void LateUpdate()
	{
		frameCount++;
	}
}
