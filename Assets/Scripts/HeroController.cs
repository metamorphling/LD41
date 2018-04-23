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
    bool isJumping = false;
    public bool isDead = false; 
    float timeInAir = 0f;
    float jumpHeightStrong, jumpHeightWeak;
    Animator anim;
    public AudioSource[] audio;
    bool walkSoundChange = false;
    public bool blockMovement = false;

    public enum AudioType
    {
        bg,
        end,
        jump,
        walk1,
        walk2
    }

    public void PlaySound(AudioType type)
    {
        int sound = (int)type;
        audio[sound].Play();
    }
    public void StopSound(AudioType type)
    {
        int sound = (int)type;
        audio[sound].Stop();
    }

    IEnumerator SoundControl()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (isMoving == true && isJumping == false)
            {
                if (walkSoundChange == false)
                {
                    PlaySound(AudioType.walk1);
                    walkSoundChange = true;
                }
                else
                {
                    PlaySound(AudioType.walk2);
                    walkSoundChange = false;
                }
            }
        }
    }

    public void ExecuteCommand(GameText.Commands todo)
    {
        switch (todo)
        {
            case GameText.Commands.Stop:
                moveDirection = 0f;
                break;
            case GameText.Commands.Left:
                moveDirection = -1f;
                break;
            case GameText.Commands.Right:
                moveDirection = 1f;
                break;
            case GameText.Commands.Jump:
                doJump = 1f;
                if (Mathf.Abs(rb.velocity.x) < 0.5f)
                {
                    jumpHeight = jumpHeightStrong;
                }
                else
                {
                    jumpHeight = jumpHeightWeak;
                }
                isJumping = true;
                PlaySound(AudioType.jump);
                break;
        }
    }

	void Start () {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        jumpHeightWeak = jumpHeight;
        jumpHeightStrong = jumpHeight * 1.6f;
        anim = GetComponent<Animator>();
        StartCoroutine("SoundControl");
        PlaySound(AudioType.bg);
    }



    void Update() {
        if (blockMovement == true)
        {
            moveDirection = 0;
            jumpSpeed = 0;
        }
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
            isMoving = true;
        }
        else
        {
            anim.SetBool("isMoving", false);
            isMoving = false;
        }
	}

    private void FixedUpdate()
    {
        if (rb.velocity.y > 0)
        {
            timeInAir += Time.deltaTime;
            float jumpDistance = rb.velocity.y * timeInAir;
            if (jumpDistance > jumpHeight)
                doJump = -1f;

        }
        else if (rb.velocity.y < 0)
        {
            if (isJumping == false && isGrounded == false)
            {
                moveDirection = 0f;
            }
            doJump = -1f;
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, doJump * jumpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground" && isGrounded == false)
        {
            isJumping = false;
            isGrounded = true;
            doJump = 0f;
            timeInAir = 0;
        }
        else if (collision.transform.tag == "Danger")
        {
            isDead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground" && isGrounded == true)
        {
            isGrounded = false;
        }
    }
}
