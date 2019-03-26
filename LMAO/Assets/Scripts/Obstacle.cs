using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector2 vectorToEnd = new Vector2(0, 10);
    public bool hasBeenReached = false;
    public int obstacleNumber;
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<GameManagerScript>().AllObstacles.Add(this.gameObject);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag ==  "Player" && hasBeenReached == false)
        {
            hasBeenReached = true;
            StartCoroutine(gameManager.GetComponent<GameManagerScript>().SpawnObstacle());
            
        }
        
    }
}
