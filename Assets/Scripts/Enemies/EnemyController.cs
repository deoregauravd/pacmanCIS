using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2.0f;
    public float obstacleDetectionDistance = 1.0f;
    public LayerMask obstacleLayer;

    private Transform targetPoint;
    private bool movingToEndPoint = true;

    private void Start()
    {
        targetPoint = endPoint;
    }

    private void Update()
    {
       
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionDistance, obstacleLayer);

        if (hit.collider != null)
        {
          
            if (movingToEndPoint)
                targetPoint = startPoint;
            else
                targetPoint = endPoint;
        }

        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            
            movingToEndPoint = !movingToEndPoint;
            if (movingToEndPoint)
                targetPoint = endPoint;
            else
                targetPoint = startPoint;
        }
    }
}
