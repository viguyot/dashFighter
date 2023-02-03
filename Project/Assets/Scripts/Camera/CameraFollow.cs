using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player1;

    public GameObject player2;

    public float offset;
    public float offsetSmoothing;

    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centerPoint = (player1.transform.position + player2.transform.position) / 2;

        targetPosition = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);

        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        Camera.main.orthographicSize = distance * offset;

        if(Camera.main.orthographicSize < 6f)
        {
            Camera.main.orthographicSize = 6f;
        }
        else if (Camera.main.orthographicSize > 10f)
        {
            Camera.main.orthographicSize = 10f;
        }
        
    }    
}