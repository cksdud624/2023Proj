using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlappyBird : MonoBehaviour
{
    public float jumpPower = 5.0f;
    public float rotatePower = 30.0f;
    public float jumpRotatePower = 60.0f;
    float rotateAmount = 0;

    public GameObject rotatePoint;
    public GameObject gameOverText;
    public GameObject readyText;
    public GameObject titleButton;
    public Text passedText;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyBirdManager.Instance.Playing() == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            }


            if(GetComponent<Rigidbody>().velocity.y < 0)
            {
                float rotate = rotatePower * Time.deltaTime;

                if (rotateAmount + rotate <= 60)
                {
                    this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x + rotate,
                        this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                    rotateAmount += rotate;
                }
            }
            else
            {
                float rotate = jumpRotatePower * Time.deltaTime;

                if (rotateAmount - rotate >= -30)
                {
                    this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x - rotate,
                        this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                    rotateAmount -= rotate;
                }
            }


        }
        else if (FlappyBirdManager.Instance.Playing() == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FlappyBirdManager.Instance.NextState();
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                readyText.SetActive(false);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        FlappyBirdManager.Instance.GameOver();
        gameOverText.SetActive(true);
        titleButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        FlappyBirdManager.Instance.PassWall();
        passedText.text = "Passed : " + FlappyBirdManager.Instance.GetPassWall();
    }

    public void Restart()
    {
        FlappyBirdManager.Instance.Restart();
        SceneManager.LoadScene("FlappyBirdStart");;
    }
}
