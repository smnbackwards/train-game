using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    public GameObject straightTrack;
    public GameObject brokenTrack;
    public GameObject blockedTrack;

    public int numberOfTracks = 5;




    // Use this for initialization
    void Start () {

        for (int i = 0; i < numberOfTracks; i++)
        {
            for (int j = -5; j < 100; j++)
            {
                Instantiate(straightTrack, new Vector3(i, j, 0), Quaternion.identity);
            }
            
        }

	}
	
	// Update is called once per frame
	void Update () {

	}
}
