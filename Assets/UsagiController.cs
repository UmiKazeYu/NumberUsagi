using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsagiController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator animator;
    private float velocityX = 18.0f;
    private float velocityY = 22.0f;
    int touchedBlockNumber = -1;
    public GameObject startBlock;
    private GameObject nextBlock;
    public Text gameStateText;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
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

        if (Input.GetKey(KeyCode.LeftArrow) && this.transform.position.x > -8.3f)
        {
            inputVelocityX = -velocityX;
            GetComponent<Animator>().SetBool("walkBool", true);
            this.gameObject.transform.localScale = new Vector2(2, 2);
        }
        if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < 8.3f)
        {
            inputVelocityX = velocityX;
            GetComponent<Animator>().SetBool("walkBool", true);
            this.gameObject.transform.localScale = new Vector2(-2, 2);
            startBlock.tag = "WrongBlockTag";
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            GetComponent<Animator>().SetBool("walkBool", false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            GetComponent<Animator>().SetBool("walkBool", false);
        }

        this.myRigidbody2D.velocity = new Vector2(inputVelocityX, this.myRigidbody2D.velocity.y);
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

            // 数字を取得
            touchedBlockNumber = int.Parse(collision.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>().text);
            // 次のブロックを変更
            if (touchedBlockNumber < 10)
            {
                nextBlock = GameObject.Find("Block" + (touchedBlockNumber + 1));
                nextBlock.tag = "NextBlockTag";
                Debug.Log(touchedBlockNumber);
                collision.gameObject.tag = "WrongBlockTag";
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
        gameStateText.text = "しっぱい…";
    }

    void Complete()
    {
        Debug.Log("おめでとう！");
        velocityX = 0;
        this.myRigidbody2D.velocity = new Vector2(0, 0);
        gameStateText.text = "おめでとう！";
    }
}