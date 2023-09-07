using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement; // << : ¾À °ü¸®

public class GameManager : MonoBehaviour//½Ì±ÛÅæ
{
    private static GameManager sInstance;

    List<string> racer = new List<string>();
    List<int> racertrack = new List<int>();
    List<string> arrivedRacer = new List<string>();

    int arrivedCar = 0;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    public int changeScene = 0;

    private void Awake()
    {
        if(sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        sInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public void ChangeScene()
    {
        int sceneIndex = changeScene++ % 2;
        //string sceneName = string.Format("Scene_ {0:2d}", scene);
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ArriveStartLine(string name)
    {
        int checker = -1;
        for(int i = 0; i < racer.Count; i++)
        {
            if (racer[i] == name)
            {
                checker = i;
                break;
            }
        }

        if(checker != -1)
        {
            racertrack[checker]++;
        }
        else
        {
            racer.Add(name);
            racertrack.Add(1);
        }
    }

    public string ArriveCheck(string name)
    {
        for(int i = 0; i < racer.Count; i++)
        {
            if (racertrack[i] >= 3 && name == racer[i])
            {
                return racer[i];
            }
        }

        return null;
    }

    public void ArriveEnd(string name)
    {
        arrivedCar++;

        arrivedRacer.Add(name);


        if(arrivedCar >= 3)
        {
            Debug.Log("Game End");
            ChangeScene("99_End");
            changeScene++;
        }
    }

    public List<string> getRanking()
    {
        return arrivedRacer;
    }

    public void Restart()
    {
        sInstance = null;
        ChangeScene("Game");
    }
}
