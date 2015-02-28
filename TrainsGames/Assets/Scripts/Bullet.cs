using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
    public float speed = 1;
    public Vector3 rotation;

    private Vector3 movement;

    void Update()
    {
        movement = speed *  transform.up;
        Debug.Log("Moving?" + movement);
        transform.position += movement * Time.deltaTime;
    }
}
