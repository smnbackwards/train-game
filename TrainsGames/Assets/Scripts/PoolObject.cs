using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour {

    public float lifespan = 5f;
    public bool dies = true;

	void Start () {
        Kill();
	}
	
    public void Activate()
    {
        gameObject.SetActive(true);
        Kill();
    }

    private void Kill()
    {
        if(dies) Invoke("Deactivate", lifespan);
    }

    public void Deactivate()
    {
        CancelInvoke("Deactivate");
        gameObject.SetActive(false);
    }


}


