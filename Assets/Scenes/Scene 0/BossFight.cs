using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject turtleSquishedPrefab;
    public List<GameObject> heartList;

    public int playerLives = 5;
    public int enemyLives = 100;
    public float heartX = 8f;
    public float heartY = 4.5f;
    public float heartSpacingX = -1f;


    // Start is called before the first frame update
    void Start()
    {
        //turn off gravity
        Physics2D.gravity = new Vector2(0, 0);

        //create heart objects
        heartList = new List<GameObject>();
        for (int i = 0; i < playerLives; i++)
        {
            GameObject tHeartGO = Instantiate<GameObject>(heartPrefab);
            Vector3 pos = Vector3.zero;
            pos.x = heartX + (heartSpacingX * i);
            pos.y = heartY;
            tHeartGO.transform.position = pos;
            heartList.Add(tHeartGO);
        }
    }

    public void PlayerHit()
    {
        //remove heart, game over at 0 hearts
        int heartIndex = heartList.Count - 1;
        GameObject heartGO = heartList[heartIndex];
        heartList.RemoveAt(heartIndex);
        Destroy(heartGO);

        if (heartList.Count == 0)
        {
            Invoke("GameOver", 0f);
        }
    }

    public void EnemyHit(int damage)
    {
        enemyLives -= damage;

        if (enemyLives <= 0)
        {
            GameObject enemy = GameObject.Find("TurtleEnemy");
            GameObject squished = Instantiate<GameObject>(turtleSquishedPrefab);
            squished.transform.position = enemy.transform.position;
            Destroy(enemy);
            Invoke("GameOver", 5f);
        }
            
    }

    void GameOver()
        {
            SceneManager.LoadScene("Game_Over");
        }
}
