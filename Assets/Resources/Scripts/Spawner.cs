using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public float interval = 5.0f;
    public int checker = 0;
    int level = 0;

    float range = 0.5f;

    void Update()
    {
        if(FlappyBirdManager.Instance.Playing() == true)
        {
            if (checker == 0)
            {
                StartCoroutine(CreateWall());
                checker = 1;
            }
        }
    }
    IEnumerator CreateWall()
    {
        while (true)
        {
            level = FlappyBirdManager.Instance.GetWall() / 5;
            if (level > 5)
            {
                level = 5;
            }
            Debug.Log(level);

            float pos = Random.Range(-3.0f, 3.0f);


            GameObject wall = Instantiate(wallPrefab, this.transform.position, this.transform.rotation);
            GameObject cylinderUp = wall.transform.GetChild(0).gameObject;
            GameObject cylinderDown = wall.transform.GetChild(1).gameObject;
            GameObject passCollider = wall.transform.GetChild(2).gameObject;

            cylinderUp.transform.position = new Vector3(cylinderUp.transform.position.x,
                cylinderUp.transform.position.y - range * level + pos, cylinderUp.transform.position.z);
            cylinderDown.transform.position = new Vector3(cylinderDown.transform.position.x,
                cylinderDown.transform.position.y + range * level + pos, cylinderDown.transform.position.z);

            passCollider.GetComponent<BoxCollider>().size
                = new Vector3(1, 1 - range * level, 0);
            passCollider.transform.position = new Vector3(passCollider.transform.position.x,
                passCollider.transform.position.y + pos, passCollider.transform.position.z);


            yield return new WaitForSeconds(interval);
        }
    }
}
