﻿using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    public GameObject straightTrack;
    public GameObject brokenTrack;
    public GameObject blockedTrack;

    public GameObject train;

    public int numberOfTracks = 5;

    const int trackSize = 5;

    int currentpos = trackSize;

    // Use this for initialization
    void Start () {

        for (int i = 0; i < numberOfTracks; i++)
        {
            buildStraightTracks(i, -trackSize);
            buildStraightTracks(i, 0);
        }
        currentpos = trackSize;
    }
	
	// Update is called once per frame
	void Update () {
        if(train.transform.position.y >= currentpos - 2 * trackSize)
        {
            generateTracks();
            currentpos += trackSize;
        }


	}

    void generateTracks()
    {
        for (int i = 0; i < numberOfTracks; i++)
        {
            int r = Random.Range(0, 5);
            if(r < 3)
            {
                buildStraightTracks(i, currentpos);
            } else if(r<4)
            {
                buildBlockedTrack(i, currentpos);
            }
            else
            {
                buildBrokenTrack(i, currentpos);
            }
        }
    }

    void buildStraightTracks(int x, int y)
    {
        for (int j = 0; j < trackSize; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y + j, 0), Quaternion.identity);
        }
    }


    void buildBrokenTrack(int x, int y)
    {

        Instantiate(brokenTrack, new Vector3(x, y, 0), Quaternion.identity);

    }

    void buildBlockedTrack(int x, int y)
    {
        for (int j = 0; j < trackSize/2; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y+j, 0), Quaternion.identity);
        }

        Instantiate(blockedTrack, new Vector3(x,y+ trackSize / 2, 0), Quaternion.identity);

        for (int j = trackSize / 2 +1; j < trackSize; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y+j, 0), Quaternion.identity);
        }
    }

}
