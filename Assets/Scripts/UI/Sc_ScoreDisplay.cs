using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_ScoreDisplay : MonoBehaviour
{
    Sc_LevelManager manager => FindObjectOfType<Sc_LevelManager>();
    Text myText => GetComponent<Text>();

    private void Update()
    {
        myText.text = "Score = " + manager.GlobalScore;
    }
}
