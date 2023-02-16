using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField timeGameTxt;
    public TMP_InputField timeSpawnTxt;
    public Slider timeGameSlider;
    public Slider timeSpawnSlider;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void SetSettingSlider()
    {
        timeGameTxt.text = timeGameSlider.value.ToString();
        timeSpawnTxt.text = timeSpawnSlider.value.ToString();
    } 

    public void SaveSettings()
    {
        var gameManager = GameManager.Instance;
        gameManager.Timer = timeGameSlider.value;
        gameManager.SpawnTimer = timeSpawnSlider.value;
    }
}
