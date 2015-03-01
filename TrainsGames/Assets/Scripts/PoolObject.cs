using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour {

    public float lifespan = 5f;
    public bool dies = true;

	void Start () {
        Kill();
	}

    public void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0
            || screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            Deactivate();
        }
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


