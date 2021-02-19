using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    private void Update()
    {
        myText.text = "Score = " + LevelManager.Instance.GlobalScore;
    }
}
