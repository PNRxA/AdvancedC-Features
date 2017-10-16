using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisownSelfOnStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		// Remove self from parent
		transform.SetParent(null);
    }
}
