using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    Generator generator;
    public int GlobalScore;
    [SerializeField] Difficulty currentDifficulty;
    bool started;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void StartGame()
    {
        generator = FindObjectOfType<Generator>();
        if (generator && !started)
        {
            started = true;
            generator.CreateObject();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        StartGame();
    }

    private void Start()
    {
        StartGame();
    }

    public void SetDifficulty(Difficulty diff)
    {
        currentDifficulty = diff;
    }

    public void IncreaseScore(int amount)
    {
        GlobalScore += amount;
    }
}
