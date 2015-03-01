using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour {

    public float value = 0;
    public float maxValue = 10;
    public RectTransform needle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        needle.rotation = Quaternion.Euler(0, 0, 180* -value/maxValue  );
	}
}
