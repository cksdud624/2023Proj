using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DefenseGameCenter : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;

    public GameObject Cubes;

    GameObject[] Cube;

    public GameObject ReadyText;
    public GameObject ResultText;
    public GameObject ScoreText;
    public GameObject CubeText;


    bool NeedToReset = false;

    float spawnInterval = 1f;

    enum GameState
    {
        Start, Ready, Play, Result
    }

    void Awake()
    {
        ResetCubes();
        ResultText.SetActive(false);
        CubeText.SetActive(false);
        ReadyText.SetActive(false); 
        ScoreText.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < Cube.Length; i++)
        {
            Cube[i].GetComponent<Cube>().ResetCube += SetNeedToReset;
        }
        Player.GetComponent<PlayerControl>().IsPlaying += IsPlaying;
    }

    GameState gameState = GameState.Start;
    void Update()
    {
        if(gameState == GameState.Start)
        {
            ReadyText.SetActive(true);
            gameState = GameState.Ready;
        }
        else if(gameState == GameState.Ready)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ReadyText.SetActive(false);
                ScoreText.SetActive(true);
                CubeText.SetActive(true);
                gameState = GameState.Play;
                CallSpawnEnemy();
            }
        }
        else if(gameState == GameState.Play)
        {
            if(NeedToReset == true)
            {
                ResetCubes();
                NeedToReset = false;
            }

            if(Cube.Length <= 0)
            {
                gameState = GameState.Result;
                ResultText.SetActive(true);
            }
        }
        else if(gameState == GameState.Result)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("DefenseGame");
            }
        }
    }

    void SetNeedToReset()
    {
        NeedToReset = true;
    }

    void CallSpawnEnemy()
    {
        StartCoroutine(SpawnEnemy());
    }

    void ResetCubes()
    {
        Cube[] temp = Cubes.GetComponentsInChildren<Cube>();
        Cube = new GameObject[temp.Length];
        for(int i = 0; i < temp.Length; i++)
        {
            Cube[i] = temp[i].gameObject;
        }

    }

    bool IsPlaying()
    {
        if(gameState == GameState.Play)
            return true;
        return false;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (gameState == GameState.Play)
            {
                spawnInterval = Random.Range(1f, 1.1f);
                float spawnPosZ = Cube[Random.Range(0, Cube.Length)].transform.position.z;
                Vector3 temp = new Vector3(-6f, 0f, spawnPosZ);
                GameObject newEnemy = Instantiate(Enemy, temp, Enemy.transform.rotation);
                newEnemy.GetComponent<EnemyControl>().IsPlaying += IsPlaying;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
