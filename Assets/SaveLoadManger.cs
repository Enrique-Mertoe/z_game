using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManger : MonoBehaviour
{
    private const string ScoreKey = "BestSaveScore";
    public static SaveLoadManger Instance { get; set; }

    public void Awake()
    {
        if (Instance && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void SaveHighestScore(int score)
    {
        PlayerPrefs.SetInt(ScoreKey,score);
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.HasKey(ScoreKey) ? PlayerPrefs.GetInt(ScoreKey) : 0;
    }
}
