using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyBirdManager : MonoBehaviour
{
    public enum GameState
    {
        Start, Ready, Play, End
    }

    GameState gameState = GameState.Start;

    int walls = 0;
    int passWalls = 0;


    private static FlappyBirdManager sInstance;

    public static FlappyBirdManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("FlappyBirdManager");
                sInstance = newGameObject.AddComponent<FlappyBirdManager>();
            }
            return sInstance;
        }
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

    public void GameOver()
    {
        gameState = GameState.End;
    }

    public bool Playing()
    {
        if(gameState == GameState.Play)
        {
            return true;
        }
        return false;
    }

    public void ClickStartButton()
    {
        gameState++;
        SceneManager.LoadScene("FlappyBird");
    }

    public void NextState()
    {
        gameState++;
    }

    public void CountWall()
    {
        walls++;
    }

    public int GetWall()
    {
        return walls;
    }

    public void PassWall()
    {
        passWalls++;
    }

    public int GetPassWall()
    {
        return passWalls;
    }

    public void Restart()
    {
        gameState = GameState.Start;
        walls = 0;
        passWalls = 0;
    }
}
