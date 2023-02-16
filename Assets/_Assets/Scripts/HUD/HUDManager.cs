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
    public int score = 0;

    private void Awake()
    {
        timer = GameManager.Instance.Timer;
    }

    private void FixedUpdate()
    {
        scoreTxt.text = $"Score: {score}";
        Debug.Log(score);
        timer -= Time.fixedDeltaTime;
        int sec = ((int)timer % 60);
        int min = ((int)timer / 60);
        timerTxt.text = timer > 0 ? string.Format("{0:00}:{1:00}", min, sec) : "00:00";
        TimeOut();
    }

    void TimeOut()
    {
        if (timer <= 0)
        {
            GameManager.Instance.Score = score;
            SceneManager.LoadScene(2);
        }
    }
}
