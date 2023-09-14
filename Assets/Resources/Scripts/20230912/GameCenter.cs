
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class GameCenter : MonoBehaviour
{

    public GameObject Cloud;
    public GameObject Enemy;
    public GameObject Spawner;
    public GameObject Player;
    public GameObject ReadyText;
    public GameObject ScoreText;
    public GameObject Projectile;
    public GameObject LifeText;
    public GameObject GameOverText;
    public GameObject HPBar;
    public GameObject OptionPopUp;

    int score = 0;

    float cloudInterval = 0f;
    float cloudYPos = 0f;
    float enemyInterval = 0f;
    float enemyYPos = 0f;
    float generateInterval = 2f;

    int level = 0;

    enum GameState
    {
        Start, Ready, Play, Result, End, Option
    }

    GameState gameState;

    int[] tester;

    void Start()
    {
        gameState = GameState.Start;
        Player.GetComponent<Player2D>().gamePlaying += GetPlay;
        Player.GetComponent<Player2D>().generateProj += generateProj;
        Player.GetComponent<Player2D>().nextState += SetNextState;
        Player.GetComponent<Player2D>().updateLife += updateLife;
        OptionPopUp.GetComponent<PopUp>().SetLevel += SetDifficulty;
        OptionPopUp.GetComponent<PopUp>().Tester += test;
    }
    void test(string a, string b)
    {
        Debug.Log(a);
        Debug.Log(b);
    }

    void generateProj(Vector3 position, Quaternion rotation)
    {
        GameObject newProjectile = Instantiate(Projectile, position, rotation);
        newProjectile.GetComponent<Projectile2D>().gamePlaying += GetPlay;
    }
    // Update is called once per frame
    void Update()
    {

        if (GetStart() == true)
        {
            SetNextState();
        }
        else if (GetReady() == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetNextState();
                generateCloud();
                generateEnemy();
                ReadyText.SetActive(false);
            }
        }
        else if (GetResult() == true)
        {
            GameOverText.SetActive(true);
            gameState = GameState.End;
        }
        else if (GetEnd() == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("06_2DProj");
            }
        }
        else if (GetPlay() == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = GameState.Option;
                OptionPopUp.SetActive(true);
            }
        }
        else if (GetOption() == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = GameState.Play;
                OptionPopUp.SetActive(false);
            }
        }
        
    }

    public void updateLife(int life)
    {
        LifeText.GetComponent<Text>().text = "Life : " + life;
        HPBar.GetComponent<Scrollbar>().size = (float)life / 5;
    }
    public void updateScore(int point)
    {
        score += point;
        ScoreText.GetComponent<Text>().text = "Score : " + score;
    }

    void SetDifficulty(int num)
    {
        level = num;
        if (level == 0)
            generateInterval = 2f;
        else
            generateInterval = 1f;
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
            if (gameState == GameState.Play)
            {
                cloudInterval = UnityEngine.Random.Range(1f, 3f);
                cloudYPos = UnityEngine.Random.Range(-5f, 5f);
                GameObject newCloud = Instantiate(Cloud, new Vector3(Spawner.transform.position.x,
                    Spawner.transform.position.y + cloudYPos, Spawner.transform.position.z),
                    Spawner.transform.rotation);
                newCloud.GetComponent<Cloud2D>().gamePlaying += GetPlay;
            }
            yield return new WaitForSeconds(cloudInterval);
        }
    }

    IEnumerator spawnEnemy()
    {
        while(true)
        {
            if (gameState == GameState.Play)
            {
                enemyInterval = UnityEngine.Random.Range(0.5f, generateInterval);
                enemyYPos = UnityEngine.Random.Range(-5f, 5f);
                GameObject newEnemy = Instantiate(Enemy, new Vector3(Spawner.transform.position.x,
                    Spawner.transform.position.y + enemyYPos, Spawner.transform.position.z),
                    Spawner.transform.rotation);
                newEnemy.GetComponent<Enemy2D>().gamePlaying += GetPlay;
                newEnemy.GetComponent<Enemy2D>().scoreCustomEvent += updateScore;
            }
            yield return new WaitForSeconds(enemyInterval);
        }
    }

    public bool GetOption()
    {
        if(gameState == GameState.Option)
            return true;
        return false;
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

    public bool GetEnd()
    {
        if (gameState == GameState.End)
            return true;
        return false;
    }
}
