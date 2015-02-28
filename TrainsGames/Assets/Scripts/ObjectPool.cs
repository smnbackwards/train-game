using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour
{

    public GameObject poolItem;
    GameObject[] pool;
    public int poolSize = 10;

    void Start()
    {

        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(poolItem) as GameObject;
            pool[i].SetActive(false);
        }
    }

    public GameObject PullFromPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].GetComponent<PoolObject>().Activate();
                return pool[i];
            }
        }
        return null;
    }

    public void DeactivatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool[i].GetComponent<PoolObject>().Deactivate();
        }
    }

    public GameObject getObject(int n)
    {
        return n<poolSize && n>=0 ? pool[n] : null;
    }
}
