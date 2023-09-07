using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public TextMeshProUGUI[] ranking;
    // Start is called before the first frame update
    void Start()
    {
        List<string> list = GameManager.Instance.getRanking();

        for(int i = 0; i < list.Count; i++)
        {
            ranking[i].text = list[i];

            if (list[i] == "CarPlayer")
                ranking[i].text = "You";
        }
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
