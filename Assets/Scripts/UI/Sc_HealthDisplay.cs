using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_HealthDisplay : MonoBehaviour
{
    Sc_ShipController ship => FindObjectOfType<Sc_ShipController>();
    [SerializeField] bool HealthOrLifes;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite full, empty;

    private void Awake()
    {
        hearts = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (ship != null)
        {
            if (HealthOrLifes)
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].sprite = ship.CurrentHealth > i ? full : empty;
                }
            }
            else
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].sprite = ship.lifes > i ? full : empty;
                }
            }
        }
    }
}
