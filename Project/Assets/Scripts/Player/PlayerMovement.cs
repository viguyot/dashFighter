using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D playerRb;
    private Vector3 velocity = Vector3.zero;
    private Animator animator;
    private Sensor_Bandit groundSensor;

    [SerializeField] private float moveSpeed = 700;
    [SerializeField] private float fallingSpeed = -100;
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private bool isOnGround = false;

    [Header("Dash")]
    [SerializeField] private float dashForce = 500;
    [SerializeField] private string lastKeyDash;

    [Header("Grab")]
    [SerializeField] private float grabRadius = 100;
    [SerializeField] private float throwForce = 300;
    [SerializeField] private Rigidbody2D grabbedBody;
    [SerializeField] private bool isGrabbing;
    [SerializeField] private string lastKeyGrab;

    void Start() {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    void FixedUpdate() {
        // Move Horizontaly
        float horizontalMovement;

        if (Input.GetKey(KeyCode.Q)) {
            horizontalMovement = Vector3.left.x * moveSpeed * Time.fixedDeltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            horizontalMovement = Vector3.right.x * moveSpeed * Time.fixedDeltaTime;
        } else {
            horizontalMovement = 0;
        }

        if (horizontalMovement > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontalMovement < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            
        animator.SetFloat("AirSpeed", playerRb.velocity.y);

        // Jump section
        if ((Input.GetKey(KeyCode.Z)) && isOnGround) {
            animator.SetTrigger("Jump");
            isOnGround = false;
            animator.SetBool("Grounded", isOnGround);
            playerRb.AddForce(new Vector2(0f, jumpForce));
            groundSensor.Disable(0.2f);
        }

        // Fast fall
        if (Input.GetKey(KeyCode.S)) {
            playerRb.AddForce(new Vector2(0f, fallingSpeed));
        }

        prepareToDash();
        QuickAttack();
        prepareToGrab();
        MovePlayer(horizontalMovement);

        if (!isOnGround && groundSensor.State()) {
            isOnGround = true;
            animator.SetBool("Grounded", isOnGround);
        }

        if(isOnGround && !groundSensor.State()) {
            isOnGround = false;
            animator.SetBool("Grounded", isOnGround);
        }

        if (Mathf.Abs(horizontalMovement) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 2);
        //Combat Idle
        else
            animator.SetInteger("AnimState", 1);
        /*//Idle
        else
            animator.SetInteger("AnimState", 0);*/
    }

    void MovePlayer(float _horizontalMovement) {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, playerRb.velocity.y);
        playerRb.velocity = Vector3.SmoothDamp(
            playerRb.velocity,
            targetVelocity,
            ref velocity,
            .05f
        );
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            StartCoroutine(WaitJump());
        }
    }

    private IEnumerator WaitJump() {
        yield return new WaitForSeconds(0.015f);
        isOnGround = true;
    }

    // Dash section
    void prepareToDash() {
        if (Input.GetKey(KeyCode.Z)) {
            lastKeyDash = "UpArrow";
        } else if (Input.GetKey(KeyCode.D)) {
            lastKeyDash = "RightArrow";
        } else if (Input.GetKey(KeyCode.S)) {
            lastKeyDash = "DownArrow";
        } else if (Input.GetKey(KeyCode.Q)) {
            lastKeyDash = "LeftArrow";
        }
        Dash();
    }

    void Dash() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (lastKeyDash == "UpArrow") {
                playerRb.AddForce(new Vector2(0, dashForce));
            } else if (lastKeyDash == "RightArrow") {
                playerRb.AddForce(new Vector2(dashForce * 10, 0));
            } else if (lastKeyDash == "DownArrow") {
                playerRb.AddForce(new Vector2(0, -dashForce));
            } else if (lastKeyDash == "LeftArrow") {
                playerRb.AddForce(new Vector2(-dashForce * 10, 0));
            }
        }
    }

    // Quick Attack section
    void QuickAttack() {
        if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetTrigger("Attack");
        }
    }

    // Grab section
    void prepareToGrab() {
        if (Input.GetKey(KeyCode.Z)) {
            lastKeyGrab = "UpArrow";
        } else if (Input.GetKey(KeyCode.D)) {
            lastKeyGrab = "RightArrow";
        } else if (Input.GetKey(KeyCode.S)) {
            lastKeyGrab = "DownArrow";
        } else if (Input.GetKey(KeyCode.Q)) {
            lastKeyGrab = "LeftArrow";
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (!isGrabbing) {
                // Vérifiez s'il y a un joueur proche pour être attrapé
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, grabRadius);
                foreach (Collider2D collider in colliders) {
                    if (collider.gameObject.tag == "Player" && collider.gameObject != gameObject) {
                        grabbedBody = collider.GetComponent<Rigidbody2D>();
                        grabbedBody.bodyType = RigidbodyType2D.Static;
                        playerRb.bodyType = RigidbodyType2D.Static;
                        isGrabbing = true;
                        break;
                    }
                }
            } else {
                // Relâcher le joueur attrapé
                grabbedBody.bodyType = RigidbodyType2D.Dynamic;
                playerRb.bodyType = RigidbodyType2D.Dynamic;
                if (lastKeyGrab == "UpArrow") {
                    grabbedBody.AddForce(new Vector2(0, throwForce));
                } else if (lastKeyGrab == "RightArrow") {
                    grabbedBody.AddForce(new Vector2(throwForce * 10, 0));
                } else if (lastKeyGrab == "DownArrow") {
                    grabbedBody.AddForce(new Vector2(0, -throwForce));
                } else if (lastKeyGrab == "LeftArrow") {
                    grabbedBody.AddForce(new Vector2(-throwForce * 10, 0));
                }
                grabbedBody = null;
                isGrabbing = false;
            }
        }
    }
}
