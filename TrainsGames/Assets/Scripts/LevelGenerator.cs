using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject straightTrack;
    public GameObject brokenTrack;
    public GameObject blockedTrack;
    public GameObject health;
    public GameObject station;
    public GameObject sign;

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

    bool right = false;
    // Update is called once per frame
    void Update()
    {
        if (train.transform.position.y >= currentpos - 3 * trackSize)
        {
            generateTracks();
            if (Mathf.FloorToInt(currentpos + 20) % 100 == 0)
            {
                right = 0 == Random.Range(0, 2);
                buildSign(right ? numberOfTracks + 3 : -5, currentpos, generateTownName());
            }
            if(Mathf.FloorToInt(currentpos) % 100 == 0)
            {
                buildStation(right, currentpos + numberOfTracks / 2);
            }



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

    void buildStation(bool right, int y)
    {
        if (right)
            Instantiate(station, new Vector3(numberOfTracks + 4, y, 0), Quaternion.identity);
        else
        {
            GameObject s = (GameObject)Instantiate(station, new Vector3(-5, y, 0), Quaternion.identity);
            s.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void buildSign(int x, int y, string name)
    {
        GameObject s = (GameObject)Instantiate(sign, new Vector3(x, y, 0), Quaternion.identity);
        UnityEngine.UI.Text t = s.GetComponentInChildren<UnityEngine.UI.Text>();
        t.text = name;
    }


    string[] preNames = new string[] { "North", "South", "East", "West", "New", "Old" };
    string[] Names1 = new string[] { "San", "Santa", "El", "Rio", "Los" };
    string[] Names2 = new string[] { "Francisco", "Cruz", "Marin", "Dorado", "Clara", "Diego" , "Grande"};

    string[] postNames = new string[] { " County", "ville", " Hills", " Town", "Bay", "City" };

    public string generateTownName()
    {
        string pre = preNames[ Random.Range(0, preNames.Length) ];
        string name1 = Names1[ Random.Range(0, Names1.Length) ];
        string name2 = Names2[ Random.Range(0, Names2.Length) ];
        string post = postNames[ Random.Range(0, postNames.Length) ];


        return string.Format("{0} {1} {2}{3}",pre,name1,name2,post);
    }

}
