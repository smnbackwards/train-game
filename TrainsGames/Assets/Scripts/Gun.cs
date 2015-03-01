using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public ObjectPool bullets;
    public Vector3 offset = new Vector3(0, 1, 0);

    float fireRate = 0.1f;
    bool fire = false;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Fire", 0, fireRate);
    }

    public void setFire(bool fire)
    {
        this.fire = fire;
    }
	
    void Fire()
    {
        if (fire)
        {
            var shot = bullets.PullFromPool();
            if (shot != null)
            {
                shot.gameObject.transform.position = transform.position;
                shot.transform.rotation = transform.parent.transform.rotation;
                
                fire = false;
            }
        }
    }
}
