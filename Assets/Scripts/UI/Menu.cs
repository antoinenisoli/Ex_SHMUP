using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Vector2 screenRes = new Vector2(480, 640);
    [SerializeField] bool fullScreen;

    private void Awake()
    {
        if (!fullScreen)
            Screen.SetResolution((int)screenRes.x, (int)screenRes.y, fullScreen);
    }

    public void PlayDifficulty(string diff = "Easy")
    {
        if (Enum.TryParse(diff, out Difficulty yourEnum))
        {
            LevelManager.Instance.SetDifficulty(yourEnum);
            SceneManager.LoadScene(1);
        }
    }

    public void PlayButtonSound(string soundName = "Bip00")
    {
        SoundManager.Instance.PlaySound(soundName, 0.2f, 1);
    }
}
