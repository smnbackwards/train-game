﻿using UnityEngine;
using System.Collections;

public class KillOffScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if ( transform.position.y < Camera.main.transform.position.y -10)
            Destroy(this.gameObject);
    }
}
