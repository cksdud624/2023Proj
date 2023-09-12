using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdGameStart : MonoBehaviour
{
    public void ClickToNextScene()
    {
        FlappyBirdManager.Instance.ClickStartButton();
    }
}
