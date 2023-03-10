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

    [Header("Dash")]
    [SerializeField]
    private float dashForce;

    [SerializeField]
    private string lastKeyDash;

    [Header("Flash")]
    [SerializeField]
    private float flashDistance;

    [SerializeField]
    private string lastKeyFlash;

    [Header("Grab")]
    [SerializeField]
    private float grabRadius;

    [SerializeField]
    private float throwForce;

    [SerializeField]
    private Rigidbody2D grabbedBody;

    [SerializeField]
    private bool isGrabbing;

    [SerializeField]
    private string lastKeyGrab;

    void Start() { }

    void FixedUpdate()
    {
        // Move Horizontaly

        float horizontalMovement;

        if (Input.GetKey("left"))
        {
            horizontalMovement = Vector3.left.x * moveSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey("right"))
        {
            horizontalMovement = Vector3.right.x * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            horizontalMovement = 0;
        }

        // Jump section

        if ((Input.GetKey("space") || Input.GetKey("up")) && isOnGround)
        {
            isJumping = true;
        }

        // Fast fall

        if (Input.GetKey("down"))
        {
            playerRb.AddForce(new Vector2(0f, fallingSpeed));
        }

        // Dash section

        prepareToDash();

        // Flash section

        prepareToFlash();

        // Grab section

        prepareToGrab();

        // Move player

        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, playerRb.velocity.y);
        playerRb.velocity = Vector3.SmoothDamp(
            playerRb.velocity,
            targetVelocity,
            ref velocity,
            .05f
        );

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

        //canGrab = collision.CompareTag("Player");
    }

    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.015f);
        isOnGround = true;
    }

    // Dash section

    void prepareToDash()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            lastKeyDash = "UpArrow";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lastKeyDash = "RightArrow";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lastKeyDash = "DownArrow";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lastKeyDash = "LeftArrow";
        }

        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lastKeyDash == "UpArrow")
            {
                playerRb.AddForce(new Vector2(0, dashForce));
            }
            else if (lastKeyDash == "RightArrow")
            {
                playerRb.AddForce(new Vector2(dashForce * 10, 0));
            }
            else if (lastKeyDash == "DownArrow")
            {
                playerRb.AddForce(new Vector2(0, -dashForce));
            }
            else if (lastKeyDash == "LeftArrow")
            {
                playerRb.AddForce(new Vector2(-dashForce * 10, 0));
            }
        }
    }

    // Flash section

    void prepareToFlash()
    {
        if (Input.GetKey("right"))
        {
            lastKeyFlash = "RightArrow";
        }
        else if (Input.GetKey("left"))
        {
            lastKeyFlash = "LeftArrow";
        }

        Flash();
    }

    void Flash()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (lastKeyFlash == "RightArrow")
            {
                transform.position = (
                    new Vector3(
                        transform.position.x + flashDistance,
                        transform.position.y,
                        transform.position.z
                    )
                );
            }
            else if (lastKeyFlash == "LeftArrow")
            {
                transform.position = (
                    new Vector3(
                        transform.position.x - flashDistance,
                        transform.position.y,
                        transform.position.z
                    )
                );
            }
        }
    }

    // Grab section

    /*void prepareToGrab() {
        if (Input.GetKeyDown(KeyCode.R) && canGrab) {
            Debug.Log("Grab effectu??");
        }
    }*/

    void prepareToGrab()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            lastKeyGrab = "UpArrow";
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            lastKeyGrab = "RightArrow";
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lastKeyGrab = "DownArrow";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lastKeyGrab = "LeftArrow";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isGrabbing)
            {
                // V??rifiez s'il y a un joueur proche pour ??tre attrap??
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, grabRadius);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.tag == "Player" && collider.gameObject != gameObject)
                    {
                        grabbedBody = collider.GetComponent<Rigidbody2D>();
                        grabbedBody.bodyType = RigidbodyType2D.Kinematic;
                        isGrabbing = true;
                        break;
                    }
                }
            }
            else
            {
                // Rel??cher le joueur attrap??
                grabbedBody.bodyType = RigidbodyType2D.Dynamic;
                if (lastKeyGrab == "UpArrow")
                {
                    grabbedBody.AddForce(new Vector2(0, throwForce));
                }
                else if (lastKeyGrab == "RightArrow")
                {
                    grabbedBody.AddForce(new Vector2(throwForce * 10, 0));
                }
                else if (lastKeyGrab == "DownArrow")
                {
                    grabbedBody.AddForce(new Vector2(0, -throwForce));
                }
                else if (lastKeyGrab == "LeftArrow")
                {
                    grabbedBody.AddForce(new Vector2(-throwForce * 10, 0));
                }
                grabbedBody = null;
                isGrabbing = false;
            }
        }
    }
}
