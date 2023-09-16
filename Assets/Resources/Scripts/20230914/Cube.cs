using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    public event Action ResetCube;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemySword")
        {
            Destroy(this.gameObject);
            ResetCube.Invoke();
        }
    }


}
