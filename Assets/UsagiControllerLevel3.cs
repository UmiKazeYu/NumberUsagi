using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsagiControllerLevel3 : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator animator;
    private float velocityX = 20.0f;
    private float velocityY = 25.0f;
    int touchedBlockNumber = -1;
    public GameObject startBlock;
    private GameObject nextBlock;
    public Text gameStateText;
    string startText = "1番からスタート！";
    bool gameOver = false;
    AudioSource audioSource;
    public AudioClip okay;
    public AudioClip clear;
    public AudioClip over;
    int screenWidthHalf = Screen.width / 2;
    public int leftSideFingerId = 0;
    public int rightSideFingerId = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        gameStateText.text = startText;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputVelocityX = 0;
        //float inputVelocityY = 0;

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    animator.SetTrigger("jumpTrigger");
        //    inputVelocityY = velocityY;
        //}
        //else
        //{
        //    inputVelocityY = this.myRigidbody2D.velocity.y;
        //}

        // キーボード・マウス入力
        if ((Input.GetKey(KeyCode.LeftArrow) || (Input.GetMouseButton(0) && Input.mousePosition.x <= screenWidthHalf)) && this.transform.position.x > -8.3f)
        {
            inputVelocityX = -velocityX;
            GetComponent<Animator>().SetBool("walkBool", true);
            this.gameObject.transform.localScale = new Vector2(2, 2);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || (Input.GetMouseButton(0) && Input.mousePosition.x > screenWidthHalf)) && this.transform.position.x < 8.3f)
        {
            inputVelocityX = velocityX;
            GetComponent<Animator>().SetBool("walkBool", true);
            this.gameObject.transform.localScale = new Vector2(-2, 2);
            startBlock.tag = "WrongBlockTag";
            if (gameStateText.text == startText)
            {
                gameStateText.text = "";
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetMouseButtonUp(0))
        {
            GetComponent<Animator>().SetBool("walkBool", false);
        }

        // タッチ入力
        foreach (Touch touch in Input.touches)
        {
            if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && touch.position.x <= screenWidthHalf && this.transform.position.x > -8.3f)
            {
                inputVelocityX = -velocityX;
                GetComponent<Animator>().SetBool("walkBool", true);
                this.gameObject.transform.localScale = new Vector2(2, 2);
                leftSideFingerId = touch.fingerId;
            }
            else if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && touch.position.x > screenWidthHalf && this.transform.position.x < 8.3f)
            {
                inputVelocityX = velocityX;
                GetComponent<Animator>().SetBool("walkBool", true);
                this.gameObject.transform.localScale = new Vector2(-2, 2);
                startBlock.tag = "WrongBlockTag";
                rightSideFingerId = touch.fingerId;
                if (gameStateText.text == startText)
                {
                    gameStateText.text = "";
                }
            }
            else if (touch.phase == TouchPhase.Ended && leftSideFingerId == touch.fingerId)
            {
                GetComponent<Animator>().SetBool("walkBool", false);
            }
            else if (touch.phase == TouchPhase.Ended && rightSideFingerId == touch.fingerId)
            {
                GetComponent<Animator>().SetBool("walkBool", false);
            }
            else if (touch.phase == TouchPhase.Began && gameOver)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        this.myRigidbody2D.velocity = new Vector2(inputVelocityX, this.myRigidbody2D.velocity.y);

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && gameOver)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StartBlockTag")
        {
            this.myRigidbody2D.velocity = new Vector2(this.myRigidbody2D.velocity.x, velocityY);
            animator.SetTrigger("jumpTrigger");
        }
        else if (collision.gameObject.tag == "NextBlockTag")
        {
            this.myRigidbody2D.velocity = new Vector2(this.myRigidbody2D.velocity.x, velocityY);
            animator.SetTrigger("jumpTrigger");
            GetComponent<ParticleSystem>().Play();

            // 数字を取得
            touchedBlockNumber = int.Parse(collision.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
            // 次のブロックを変更
            if (touchedBlockNumber < 10)
            {
                nextBlock = GameObject.Find("Block" + (touchedBlockNumber + 1));
                nextBlock.tag = "NextBlockTag";
                Debug.Log(touchedBlockNumber);
                collision.gameObject.tag = "WrongBlockTag";
                audioSource.PlayOneShot(okay, 0.5f);
            }
            else
            {
                Complete();
            }

        }
        else if (collision.gameObject.tag == "WrongBlockTag")
        {
            GameOver();
        }

        //if (collision.gameObject.tag == "BlockTag" && failure)
        //{


        //    int blockNumber = int.Parse(collision.gameObject.name);

        //    if (touchedBlockNumber + 1 == blockNumber)
        //    {
        //        Debug.Log("あってます");
        //    }
        //    else
        //    {
        //        failure = false;
        //        Debug.Log("まちがえまちた");
        //    }

        //    touchedBlockNumber = blockNumber;
        ////}

        //Debug.Log("Hit" + collision.gameObject.name);


    }
    void GameOver()
    {
        Debug.Log("ざんねん");
        velocityX = 0;
        this.myRigidbody2D.velocity = new Vector2(0, 0);
        gameStateText.text = "しっぱい…<size=20>[Enter]</size>";
        gameOver = true;
        audioSource.PlayOneShot(over, 0.5f);
    }

    void Complete()
    {
        Debug.Log("おめでとう！");
        velocityX = 0;
        this.myRigidbody2D.velocity = new Vector2(0, 0);
        gameStateText.text = "おめでとう！<size=20>[Enter]</size>";
        gameOver = true;
        audioSource.PlayOneShot(clear, 0.5f);
    }
}