using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagiController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator animator;
    private float velocityX = 18.0f;
    private float velocityY = 22.0f;
    int touchedBlockNumber = -1;
    bool failure = true;

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
        }
        if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < 8.3f)
        {
            inputVelocityX = velocityX;
            GetComponent<Animator>().SetBool("walkBool", true);
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
        if (collision.gameObject.tag == "BlockTag" && failure)
        {
            this.myRigidbody2D.velocity = new Vector2(this.myRigidbody2D.velocity.x, velocityY);
            animator.SetTrigger("jumpTrigger");

            int blockNumber = int.Parse(collision.gameObject.name);

            if (touchedBlockNumber + 1 == blockNumber)
            {
                Debug.Log("あってます");
            }
            else
            {
                failure = false;
                Debug.Log("まちがえまちた");
            }

            touchedBlockNumber = blockNumber;
        }

        Debug.Log("Hit" + collision.gameObject.name);



    }
}