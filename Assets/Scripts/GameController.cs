using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;
    public GameObject collectibleCoinPrefab;
    public GameObject exitPointPrefab;
    public Button[] movementButtons;
    public static int score;

    private Transform player;
    private Transform[,] grid;
    private int exitPos;

    private int gridSize = 6;

    private float tileSize = 1.0f; // Adjust this based on your grid size
    private Vector3 targetPosition;
    private List<Vector3> coinPositions = new List<Vector3>();


    private void Start()
    {
        InitializeLevel();
        InitializeCoins();
        InitializeBlockers();

        score = 0;

        movementButtons[0].onClick.AddListener(MoveRight);
        movementButtons[1].onClick.AddListener(MoveLeft);
        movementButtons[2].onClick.AddListener(MoveUp);
        movementButtons[3].onClick.AddListener(MoveDown);
    }

    private void InitializeLevel()
    {
        grid = new Transform[gridSize, gridSize];
        exitPos = gridSize - 1;
        exitPointPrefab = Instantiate(exitPointPrefab, new Vector3(exitPos, exitPos, -0.1f), Quaternion.identity);
        Vector3 playerSpawnPosition = GetRandomEmptyCell();
        
        player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity).transform;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                

                if (grid[i, j] == null) // Check if cell is empty
                {
                    Vector3 position = new Vector3(i, j, 0) * tileSize;


                    Instantiate(tilePrefab, position, Quaternion.identity);
                    grid[i, j] = player; // Mark cell as occupied
                }
            }
        }

    }

    private void InitializeCoins()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomX, randomY;
            do
            {
                randomX = Random.Range(0, gridSize);
                randomY = Random.Range(0, gridSize);
            } while (IsPositionOccupied(new Vector3(randomX, randomY)));

            Vector3 coinPosition = new Vector3(randomX * tileSize, randomY * tileSize, -0.01f);
            coinPositions.Add(coinPosition);
            Instantiate(collectibleCoinPrefab, coinPosition, Quaternion.identity);
        }
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        foreach (Vector3 coinPos in coinPositions)
        {
            if (position == coinPos)
            {
                return true;
            }
        }
        return false;
    }

    private void InitializeBlockers()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomX, randomY;
            do
            {
                randomX = Random.Range(0, gridSize);
                randomY = Random.Range(0, gridSize);
            } while (IsPositionOccupied(new Vector3(randomX, randomY)));

            Vector3 blockerPosition = new Vector3(randomX * tileSize, randomY * tileSize, -0.01f);
            Instantiate(obstaclePrefab, blockerPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomEmptyCell()
    {
        int x, y;
        do
        {
            x = Random.Range(0, gridSize);
            y = Random.Range(0, gridSize);
        } while (grid[x, y] != null);

        return new Vector3(x, y, 0) * tileSize;
    }

    private void Update()
    {
        player.position = Vector3.MoveTowards(player.position, targetPosition, Time.deltaTime);

    }

    private void MoveRight()
    {
        targetPosition = player.position + Vector3.right * tileSize;
    }

    private void MoveLeft()
    {
        targetPosition = player.position + Vector3.left * tileSize;
    }

    private void MoveUp()
    {
        targetPosition = player.position + Vector3.up * tileSize;
    }

    private void MoveDown()
    {
        targetPosition = player.position + Vector3.down * tileSize;
    }


  

    private void RestartLevel()
    {
        
        foreach (Vector3 coinPos in coinPositions)
        {
            Destroy(GameObject.FindGameObjectWithTag("Coin"));
        }
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(obstacle);
        }

        InitializeCoins();
        InitializeBlockers();

        Vector3 playerSpawnPosition = GetRandomEmptyCell();
        player.position = playerSpawnPosition;
    }


}