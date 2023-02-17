using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    int score;
    public TMP_Text scoreTxt;

    private void Start()
    {
        score = GameManager.Instance.Score;
        scoreTxt.text = $"Score: {score}";
    }

    public void PlayAgain()
    {
        
        GameManager.Instance.GoGame();
    }

    public void MainMenu()
    {
        GameManager.Instance.GoMenu();
    }
}
