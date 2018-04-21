using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {
    public float jumpSpeed = 1f;
    public float moveSpeed = 1f;
    public float jumpHeight = 2f;

    Rigidbody2D rb;
    GameText.Commands command = GameText.Commands.Stop;
    float moveDirection = 0f;
    float doJump = 0f;
    bool isMoving = false;
    bool isGrounded = false;
    float timeInAir = 0f;

    public void ExecuteCommand(GameText.Commands todo)
    {
        switch (todo)
        {
            case GameText.Commands.Stop:
                moveDirection = 0f;
                Debug.Log("stop");
                break;
            case GameText.Commands.Left:
                moveDirection = -1f;
                Debug.Log("left");
                break;
            case GameText.Commands.Right:
                moveDirection = 1f;
                Debug.Log("right");
                break;
            case GameText.Commands.Jump:
                doJump = 1f;
                Debug.Log("jump");
                break;
        }
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (rb.velocity.y > 0)
        {
            timeInAir += Time.deltaTime;
            float jumpDistance = rb.velocity.y * timeInAir;
            if (jumpDistance > jumpHeight)
                doJump = -1f;
        }
        else if (rb.velocity.y < 0)
        {
            doJump = -1f;
        }

	}

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, doJump * jumpSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" && isGrounded == false)
        {
            isGrounded = true;
            doJump = 0f;
            Debug.Log("grounded");
            timeInAir = 0; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("tag "  + collision.transform.tag);
        if (collision.transform.tag == "Ground" && isGrounded == true)
        {
            isGrounded = false;
            Debug.Log("ungrounded");
        }
    }
}
