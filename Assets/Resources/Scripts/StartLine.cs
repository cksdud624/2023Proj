using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cars;
using AICars;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StartLine : MonoBehaviour
{
    public int[] racers;
    // Start is called before the first frame update
    void Start()
    {
        racers = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "MiddleLine")
        {
            if(other.gameObject.GetComponent<Car>() != null)
            {
                other.gameObject.GetComponent<Car>().index = 1;
            }
            else if(other.gameObject.GetComponent<AICar>() != null)
            {
                other.gameObject.GetComponent<AICar>().index = 1;
            }
        }
        else
        {
            if (other.gameObject.name == "Car1" ||
                other.gameObject.name == "Car2" ||
                other.gameObject.name == "Car3" || other.gameObject.name == "CarPlayer")
            {
                int check = 0;

                if (other.gameObject.GetComponent<Car>() != null)
                {
                    if(other.gameObject.GetComponent<Car>().index == 1)
                    {
                        other.gameObject.GetComponent<Car>().index = 0;
                        other.gameObject.GetComponent<Car>().text.text = "Lap : " + GameManager.Instance.getLaps(other.gameObject.name);
                        check = 1;
                    }
                }
                else if (other.gameObject.GetComponent<AICar>() != null)
                {
                    if(other.gameObject.GetComponent<AICar>().index == 1)
                    {
                        other.gameObject.GetComponent<AICar>().index = 0;
                        check = 1;
                    }
                    else
                    {
                        other.gameObject.transform.position = other.gameObject.GetComponent<AICar>().startpoint;
                    }
                }

                if (check == 1)
                {
                    GameManager.Instance.ArriveStartLine(other.gameObject.name);
                    string temp = GameManager.Instance.ArriveCheck(other.gameObject.name);
                    if (temp == other.gameObject.name)
                    {
                        GameManager.Instance.ArriveEnd(other.gameObject.name);
                    }
                }
            }
        }
    }

}
