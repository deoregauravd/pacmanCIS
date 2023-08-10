using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int score;
    public TMP_Text scoreText;


    private void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            HandleCoinCollision(other.gameObject);
            score++;
            other.gameObject.SetActive(false);
            Debug.Log("Collided");
        }
        else if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(other.gameObject);
            Debug.Log("Collided");
        }
        else if (other.CompareTag("Exit"))
        {
            HandleExitCollision();
            Debug.Log("Collided");
        }
        else if (other.CompareTag("Enemy"))
        {
            RestartLevel();
        }
    }

    private void HandleCoinCollision(GameObject coin)
    {
        
    }

    private void HandleObstacleCollision(GameObject obstacle)
    {
    
    }

    private void HandleExitCollision()
    {
       
    }

    private void RestartLevel()
    {
        
    }
}
