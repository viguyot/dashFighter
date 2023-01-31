using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRb;

    private Vector3 velocity = Vector3.zero;
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float fallingSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isOnGround = false;

    [SerializeField]
    private string lastKeyFlash;
    [SerializeField]
    private bool isFlashing = false;

    void Start() {
    }

    void FixedUpdate() {
        
        // Move Horizontaly

        float horizontalMovement;

        if (Input.GetKey("left")) {
            horizontalMovement = Vector3.left.x * moveSpeed * Time.fixedDeltaTime;
        } else if (Input.GetKey("right")) {
            horizontalMovement = Vector3.right.x * moveSpeed * Time.fixedDeltaTime;
        } else {
            horizontalMovement = 0;
        }

        // Jump section

        if ((Input.GetKey("space") || Input.GetKey("up")) && isOnGround) {
            isJumping = true;
        }

        // Fast fall

        if (Input.GetKey("down")) {
            playerRb.AddForce(new Vector2(0f, fallingSpeed));
        }

        // Flash section
        
        prepareToFlash();

        // Move player

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, playerRb.velocity.y);
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            playerRb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
            isOnGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            StartCoroutine(WaitJump());
        }
    }

    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.015f);
        isOnGround = true;
    }

    // Dash

    // Flash

    void prepareToFlash() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            isFlashing = true;
        }

        if (Input.GetKey("right")) {
            lastKeyFlash = "RightArrow";
        } else if (Input.GetKey("left")) {
            lastKeyFlash = "LeftArrow";
        }

        Flash();
    }

    void Flash()
    {
        if(isFlashing)
        { 
            if (lastKeyFlash == "RightArrow") {
                transform.position = (new Vector3(transform.position.x + 4, transform.position.y, transform.position.z));
            } else if (lastKeyFlash == "LeftArrow") {
                transform.position = (new Vector3(transform.position.x - 4, transform.position.y, transform.position.z));
            }

            isFlashing = false; 
        }
    }

    // Shield
    // Grab
}
