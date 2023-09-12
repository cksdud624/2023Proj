using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2D : MonoBehaviour
{
    private static GameManager2D sInstance;

    public static GameManager2D Instance()
    {
        if (sInstance == null)
        {
            GameObject newGameObject = new GameObject("GameManager2D");
            sInstance = newGameObject.AddComponent<GameManager2D>();
        }
        return sInstance;
    }

    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
