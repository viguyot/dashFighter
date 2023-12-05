using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
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
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            lastKeyGrab = "UpArrow";
        }
        else if (Input.GetKey(KeyCode.D))
        {
            lastKeyGrab = "RightArrow";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            lastKeyGrab = "DownArrow";
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            lastKeyGrab = "LeftArrow";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isGrabbing)
            {
                // Vérifiez s'il y a un joueur proche pour être attrapé
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
                // Relâcher le joueur attrapé
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
