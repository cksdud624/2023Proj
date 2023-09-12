using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCenter : MonoBehaviour
{
    private static GameCenter sInstance;

    public GameObject Cloud;
    public GameObject Enemy;
    public GameObject Spawner;
    public GameObject Player;
    public GameObject ReadyText;
    public GameObject ScoreText;

    int score = 0;

    float cloudInterval = 0f;
    float cloudYPos = 0f;
    float enemyInterval = 0f;
    float enemyYPos = 0f;

    enum GameState
    {
        Start, Ready, Play, Result
    }

    GameState gameState;

    // Update is called once per frame
    void Update()
    {
        if(GetStart() == true)
        {
            SetNextState();
        }
        else if(GetReady() == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SetNextState();
                generateCloud();
                generateEnemy();
                ReadyText.SetActive(false);
            }
        }
        else if(GetPlay() == true)
        {

        }
        else if(GetResult() == true)
        {

        }
    }

    public void updateScore(int point)
    {
        score += point;
        ScoreText.GetComponent<Text>().text = "Score : " + score;
    }

    void generateCloud()
    {
        StartCoroutine(spawnCloud());
    }

    void generateEnemy()
    {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnCloud()
    {
        while(true)
        {
            cloudInterval = Random.Range(1f, 3f);
            cloudYPos = Random.Range(-4f, 4f);
            Instantiate(Cloud, new Vector3(Spawner.transform.position.x, Spawner.transform.position.y + cloudYPos, Spawner.transform.position.z),
                Spawner.transform.rotation);
            yield return new WaitForSeconds(cloudInterval);
        }
    }

    IEnumerator spawnEnemy()
    {
        while(true)
        {
            enemyInterval = Random.Range(1f, 3f);
            enemyYPos = Random.Range(-4f, 4f);
            Instantiate(Enemy, new Vector3(Spawner.transform.position.x, Spawner.transform.position.y + enemyYPos, Spawner.transform.position.z),
                Spawner.transform.rotation);
            yield return new WaitForSeconds(enemyInterval);
        }
    }

    private void Start()
    {
        gameState = GameState.Start;
    }

    public bool GetStart()
    {
        if (gameState == GameState.Start)
            return true;
        return false;
    }

    public bool GetReady()
    {
        if (gameState == GameState.Ready)
            return true;
        return false;
    }

    public bool GetPlay()
    {
        if (gameState == GameState.Play)
            return true;
        return false;
    }

    public bool GetResult()
    {
        if (gameState == GameState.Result)
            return true;
        return false;
    }

    public void SetNextState()
    {
        gameState++;
    }
}
