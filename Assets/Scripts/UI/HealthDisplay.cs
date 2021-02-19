using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    ShipController ship;
    [SerializeField] bool HealthOrLifes;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite full, empty;

    private void Awake()
    {
        hearts = GetComponentsInChildren<Image>();
        ship = FindObjectOfType<ShipController>();
    }

    private void Update()
    {
        if (ship != null)
            if (HealthOrLifes)
                for (int i = 0; i < hearts.Length; i++)
                    hearts[i].sprite = ship.CurrentHealth > i ? full : empty;
            else
                for (int i = 0; i < hearts.Length; i++)
                    hearts[i].sprite = ship.lifes > i ? full : empty;
    }
}
