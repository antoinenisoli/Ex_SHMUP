﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LevelManager : MonoBehaviour
{
    [Serializable]
    public class SpawnData
    {
        [SerializeField] string _name;
        public float spawnRate = 2;
        public GameObject[] toSpawn;
    }

    Sc_ShipController ship => FindObjectOfType<Sc_ShipController>();
    public int GlobalScore;
    [SerializeField] float spawnLine;
    [SerializeField] SpawnData[] spawnData;

    private void Start()
    {
        foreach (SpawnData data in spawnData)
        {
            StartCoroutine(Generate(data, data.spawnRate));
        }
    }

    public void IncreaseScore(int amount)
    {
        GlobalScore += amount;
    }

    IEnumerator Generate(SpawnData _data, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_data.toSpawn.Length > 0 && ship != null && !ship.isDead)
        {
            Vector2 newPos = new Vector2(UnityEngine.Random.Range(-spawnLine, spawnLine), transform.position.y);
            int random = UnityEngine.Random.Range(0, _data.toSpawn.Length);

            if (_data.toSpawn[random] != null)
            {
                Instantiate(_data.toSpawn[random], newPos, Quaternion.identity);
            }
        }

        StartCoroutine(Generate(_data, _data.spawnRate));
    }
}
