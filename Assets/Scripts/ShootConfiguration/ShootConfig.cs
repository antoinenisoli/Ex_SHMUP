using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootMode
{
    Auto,
    Laser,
}

[CreateAssetMenu(fileName = "ShootConfig", menuName = "ShootConfig/Config")]
public class ShootConfig : ScriptableObject
{
    public new string name;
    public string bulletSound = "Shoot01";
    public ShootMode mode;
    public Color displayColor = Color.white;
    public GameObject bullet;
    public float bulletSpeed = 8;
    public float fireRate = 0.1f;
}
