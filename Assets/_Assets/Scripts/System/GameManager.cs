using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    private float spawnTimer = 1;
    private float timer = 30;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    public float SpawnTimer
    {
        get { return spawnTimer; }
        set { spawnTimer = value; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoMenu()
    {
        this.score = 0;
        SceneManager.LoadScene(0);
    }

    public void GoGame()
    {
        this.score = 0;
        SceneManager.LoadScene(1);
    }

    public void GoGameOver(int score)
    {
        this.score = score;
        SceneManager.LoadScene(2);
    }
}
