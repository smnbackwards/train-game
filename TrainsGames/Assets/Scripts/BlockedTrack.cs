using UnityEngine;
using System.Collections;

public class BlockedTrack : MonoBehaviour
{
    public GameObject straightTrack;
    public const int damage = 20;
    public int score = 10;
    void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            collider.gameObject.GetComponent<PoolObject>().Deactivate();
            Destroy();
            Score.score += score;
        }

    }


    public void Destroy()
    {
        Instantiate(straightTrack, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
