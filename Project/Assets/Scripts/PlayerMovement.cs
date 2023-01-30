using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    public Vector3 velocity = Vector3.zero;
    
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isOnGround;

    void Start() {
    }

    void FixedUpdate() {

        float horizontalMovement;

        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton","RightArrow")))) {
            horizontalMovement = Vector3.left.x * moveSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton","RightArrow")))) {
            horizontalMovement = Vector3.right.x * moveSpeed * Time.fixedDeltaTime;
        }
        else {
            horizontalMovement = 0;
        }

        if ((Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton","Space")))) && isOnGround)
        {
            isJumping = true;
        }
    }
}
