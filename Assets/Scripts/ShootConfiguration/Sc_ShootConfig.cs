using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootMode
{
    Semi,
    Auto,
    Laser,
}

[CreateAssetMenu(fileName = "ShootConfig", menuName = "ShootConfig/Config")]
public class Sc_ShootConfig : ScriptableObject
{
    public string bulletSound = "Shoot01";
    public Color displayColor = Color.white;
    public GameObject bullet;
    public float bulletSpeed = 8;
    public float fireRate = 0.1f;
}
