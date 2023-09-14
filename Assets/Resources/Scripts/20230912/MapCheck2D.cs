using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck2D : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Cloud")
        {
            Destroy(collision.gameObject);
        }
    }
}
