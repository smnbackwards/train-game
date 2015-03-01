using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Fire1") != 0 || Input.GetAxis("Fire2") != 0)
            Application.LoadLevel(1);
	}
}
