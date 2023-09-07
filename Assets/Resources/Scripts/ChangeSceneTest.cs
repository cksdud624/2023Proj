using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.ChangeScene();
        }
        */
        /*
        if (Input.GetMouseButtonDown(1))
        {
            if(GameManager.Instance.changeScene == 0)
            {
                GameManager.Instance.ChangeScene("Game");
                GameManager.Instance.changeScene++;
            }

            else if(GameManager.Instance.changeScene == 1)
            {
                GameManager.Instance.ChangeScene("03_Collision");
                GameManager.Instance.changeScene++;
            }
        }
        */
    }

    public void ClickButton()
    {
        GameManager.Instance.ChangeScene("Game");
        GameManager.Instance.changeScene++;
    }

    /*
    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 200, 200, 30), "¾À º¯°æ"))
        {
            if (GameManager.Instance.changeScene == 0)
            {
                GameManager.Instance.ChangeScene("99_End");
                GameManager.Instance.changeScene++;
            }

            else if (GameManager.Instance.changeScene == 1)
            {
                GameManager.Instance.ChangeScene("03_Collision");
                GameManager.Instance.changeScene++;
            }
        }
    }
    */
}
