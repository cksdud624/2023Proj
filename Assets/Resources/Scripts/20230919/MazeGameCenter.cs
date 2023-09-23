using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MazeGameCenter : MonoBehaviour
{
    public GameObject Floors;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Item;
    public GameObject Chaser;
    public GameObject ScoreText;
    public GameObject ItemText;

    float ItemInterval = 10f;
    float EnemyInterval = 15f;

    List<GameObject> enemies = new List<GameObject>();

    List<GameObject> items =new List<GameObject>();
    int score = 0;
    int leftCube = 5;

    List<Transform> transforms = new List<Transform>();

    enum GameState
    {
        Ready, Play, Result
    }

    GameState gameState = GameState.Ready;

    private void Awake()
    {
        Player.GetComponent<Player3D>().IsPlaying += IsPlaying;
        Player.GetComponent<Player3D>().DestroyItem += DestroyItem;
        Transform[] temp;
        temp = Floors.GetComponentsInChildren<Transform>();
        Debug.Log(temp.Length);
        for(int i = 0; i < temp.Length; i++)
        {
            string name = temp[i].gameObject.name;
            if (name.Length >= 5)
            {
                if (name.Substring(0, 5) == "Floor")
                {
                    transforms.Add(temp[i]);
                }
            }
        }
    }

    void ChangeTarget()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<Enemy3D>().target == null)
            {
                if (items.Count > 0)
                {
                    enemies[i].GetComponent<Enemy3D>().target =
                        items[Random.Range(0, items.Count)];
                }
            }
        }
    }

    bool IsPlaying()
    {
        if (gameState == GameState.Play)
            return true;
        return false;
    }

    void DestroyEnemyItem(GameObject gameObject)
    {
        leftCube--;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == gameObject)
            {
                items.Remove(gameObject);
                Destroy(gameObject);
            }
        }
        ItemText.GetComponent<Text>().text = "Items : " + leftCube + "  ";
    }

    void DestroyItem(GameObject gameObject)
    {
        score += 100;

        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] == gameObject)
            {
                items.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        ScoreText.GetComponent<Text>().text = "Score : " + score;
    }

    private void Update()
    {
        if(gameState == GameState.Ready)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameState = GameState.Play;
                StartCoroutine(GenerateItem());
                StartCoroutine(GenerateEnemy());
            }
        }
        else if(gameState == GameState.Play)
        {
            ChasingItem();
            ChangeTarget();

            if(leftCube <= 0)
            {
                gameState = GameState.Result;
            }
        }
        else if(gameState == GameState.Result)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("20230919");
            }
        }
    }

    void ChasingItem()
    {
        Vector3 ChaserPos = Chaser.transform.position;
        float magnitude;

        float minf = -1;
        int index = -1;

        for(int i = 0; i < items.Count; i++)
        {
            magnitude = Mathf.Pow(Mathf.Abs(items[i].transform.position.x - ChaserPos.x), 2);
            magnitude += Mathf.Pow(Mathf.Abs(items[i].transform.position.z - ChaserPos.z), 2);
            if(minf == -1)
            {
                minf = magnitude;
                index = i;
            }
            else if(minf > magnitude)
            {
                minf = magnitude;
                index = i;
            }
        }

        if(!(minf < 0 && index < 0))
        {
            Chaser.transform.LookAt(items[index].transform.position);
            Chaser.transform.rotation = Quaternion.Euler(Chaser.transform.eulerAngles.x + 60,
                Chaser.transform.eulerAngles.y, Chaser.transform.eulerAngles.z);
        }
    }

    IEnumerator GenerateItem()
    {
        while (true)
        {
            if(gameState == GameState.Play)
            {
                if(items.Count < 3)
                {
                    Vector3 temp = transforms[Random.Range(0, transforms.Count)].position;
                    GameObject newItem = Instantiate(Item, temp, transform.rotation);
                    items.Add(newItem);
                }
            }
            yield return new WaitForSeconds(ItemInterval);
        }
    }

    IEnumerator GenerateEnemy()
    {
        while(true)
        {
            if (gameState == GameState.Play)
            {
                if (enemies.Count < 5)
                {
                    Vector3 temp = transforms[Random.Range(0, transforms.Count)].position;
                    GameObject newEnemy = Instantiate(Enemy, temp, Enemy.transform.rotation);
                    newEnemy.GetComponent<Enemy3D>().IsPlaying += IsPlaying;
                    newEnemy.GetComponent<Enemy3D>().DestroyEnemyItem += DestroyEnemyItem;
                    enemies.Add(newEnemy);
                }
            }

            yield return new WaitForSeconds(EnemyInterval);
        }
    }

}
