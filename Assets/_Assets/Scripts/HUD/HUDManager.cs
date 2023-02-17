using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private TMP_Text scoreTxt;

    private float timer;
    public int score;
    ScoreManager scoreManager;


    private void Start()
    {
        score = 0;
        timer = GameManager.Instance.Timer;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void FixedUpdate()
    {
        UpdateScore();
        UpdateTimer();
        CheckTimeOut();
    }

    private void UpdateScore()
    {
        score = scoreManager.score;
        scoreTxt.text = $"Score: {score}";
    }

    private void UpdateTimer()
    {
        timer -= Time.fixedDeltaTime;
        int sec = Mathf.FloorToInt(timer % 60);
        int min = Mathf.FloorToInt(timer / 60);
        timerTxt.text = timer > 0 ? string.Format("{0:00}:{1:00}", min, sec) : "00:00";
    }

    private void CheckTimeOut()
    {
        if (timer <= 0)
        {
            GameManager.Instance.GoGameOver(score);
        }
    }
}
