using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultUIProject : MonoBehaviour
{
    public GameObject popupObj;
    public void onPopUp()
    {
        Debug.Log("a");
        if(popupObj.activeSelf)
        {
            popupObj.SetActive(false);
        }
        else
        {
            popupObj.SetActive(true);
        }
    }
}
