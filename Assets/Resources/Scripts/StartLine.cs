using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Car1" ||
            other.gameObject.name == "Car2" ||
            other.gameObject.name == "Car3" || other.gameObject.name == "CarPlayer")
        {
            Debug.Log(other.gameObject.name);

            GameManager.Instance.ArriveStartLine(other.gameObject.name);
            string temp = GameManager.Instance.ArriveCheck(other.gameObject.name);
            if (temp == other.gameObject.name)
            {
                GameManager.Instance.ArriveEnd(other.gameObject.name);
            }
        }
    }

}
