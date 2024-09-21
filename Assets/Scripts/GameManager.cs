using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;

    public int meteorCount = 0;

    public Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager.Restart += RestartGame;
        Player.PlayerDead += GameOver;
        Meteor.MeteorDown += MeteorDown;

        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    public void RestartGame()
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }
    }
    void GameOver() {
        gameOver = true;
        CancelInvoke();
    }
    void MeteorDown()
    {
        meteorCount++;
        score++;
        if(meteorCount >= 5) BigMeteor();
        scoreText.text = "Score: " + score;
    }
}
