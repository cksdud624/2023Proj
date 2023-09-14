using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    public Action<int> SetLevel;

    public event Action<string, string> Tester;


    private void Update()
    {
        Tester.Invoke("¾Æ", "¿À");
    }

    Toggle[] toggles;

    void Start()
    {
        toggles = this.GetComponentsInChildren<Toggle>();
    }

    public void ChangeDifficulty()
    {

        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                SetLevel(i);
                Debug.Log(i);
                break;
            }
        }
    }

}
