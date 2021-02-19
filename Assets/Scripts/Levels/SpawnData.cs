using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum GenerationType
{
    Enemies,
    Items,
    Patrolling,
}

[Serializable]
public class SpawnData
{
    public GenerationType type;
    public float spawnRate = 2;
    public GameObject[] toSpawn;
    public bool spawnOnLine = true;
    public float spawnPos;
}
