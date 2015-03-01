using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject straightTrack;
    public GameObject brokenTrack;
    public GameObject blockedTrack;
    public GameObject health;
    public GameObject station;

    public GameObject train;

    public int numberOfTracks = 5;

    const int trackSize = 5;

    int currentpos = trackSize;

    // Use this for initialization
    void Start()
    {
        broken = new bool[numberOfTracks];
        oldBroken = new bool[numberOfTracks];
        for (int i = 0; i < numberOfTracks; i++)
        {
            buildStraightTracks(i, -trackSize);
            buildStraightTracks(i, 0);
        }
        currentpos = trackSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (train.transform.position.y >= currentpos - 3 * trackSize)
        {
            generateTracks();
            if (Mathf.FloorToInt(currentpos + 20) % 100 == 0)
                Debug.Log("Town Coming");
            if(Mathf.FloorToInt(currentpos) % 100 == 0)
                generateStation();

            currentpos += trackSize;
        }


    }

    bool[] broken;
    bool[] oldBroken;
    bool b = false;
    void generateTracks()
    {
        int count = 0;
        int healthCount = 0;

        for (int i = 0; i < numberOfTracks; i++)
        {
            int r = Random.Range(0, 5);
            if (r < 3)
            {
                int health = Random.Range(0, 15);
                if (health == 0 && healthCount < 2)
                {
                    buildHealth(i, currentpos);
                    healthCount++;
                }
                buildStraightTracks(i, currentpos);
            }
            else if (r < 4)
            {
                buildBlockedTrack(i, currentpos);
            }
            else
            {
                //Don't build broken tracks twice in a row
                if (broken[i] || count > numberOfTracks / 2 || b)
                {
                    buildBlockedTrack(i, currentpos);
                }
                else
                {
                    buildBrokenTrack(i, currentpos);
                    count++;
                }
            }
        }
        b = count > 0;
    }

    void generateStation()
    {
        buildStation(numberOfTracks + 4, currentpos + numberOfTracks/2);

    }

    void buildStraightTracks(int x, int y)
    {
        for (int j = 0; j < trackSize; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y + j, 0), Quaternion.identity);
        }
        broken[x] = false;
    }


    void buildBrokenTrack(int x, int y)
    {

        Instantiate(brokenTrack, new Vector3(x, y, 0), Quaternion.identity);
        broken[x] = true;
    }

    void buildBlockedTrack(int x, int y)
    {
        for (int j = 0; j < trackSize / 2; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y + j, 0), Quaternion.identity);
        }

        Instantiate(blockedTrack, new Vector3(x, y + trackSize / 2, 0), Quaternion.identity);

        for (int j = trackSize / 2 + 1; j < trackSize; j++)
        {
            Instantiate(straightTrack, new Vector3(x, y + j, 0), Quaternion.identity);
        }
        broken[x] = false;
    }

    void buildHealth(int x, int y)
    {
        Instantiate(health, new Vector3(x, y, 0), Quaternion.identity);
    }

    void buildStation(int x, int y)
    {
        Instantiate(station, new Vector3(x, y, 0), Quaternion.identity);
    }

}
