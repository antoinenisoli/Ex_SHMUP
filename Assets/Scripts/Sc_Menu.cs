﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Menu : MonoBehaviour
{
    [SerializeField] Vector2 screenRes = new Vector2(480, 640);

    private void Awake()
    {
        Screen.SetResolution((int)screenRes.x, (int)screenRes.y, false);
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButtonSound(string soundName = "Bip00")
    {
        Sc_SoundManager.Instance.PlaySound(soundName, 0.2f, 1);
    }
}
