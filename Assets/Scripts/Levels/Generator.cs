using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    ShipController ship;
    [SerializeField] float spawnLine;
    [SerializeField] SpawnData[] spawnData;

    private void Awake()
    {
        ship = FindObjectOfType<ShipController>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - Vector3.right * spawnLine, transform.position + Vector3.right * spawnLine);
    }

    public void CreateObject()
    {
        foreach (var item in spawnData)
            StartCoroutine(Generate(item, item.spawnRate));
    }

    IEnumerator Generate(SpawnData _data, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_data.toSpawn.Length > 0 && ship != null && !ship.isDead)
        {
            Vector2 newPos;
            int random = Random.Range(0, _data.toSpawn.Length);

            if (!_data.spawnOnLine)
                newPos = new Vector2(0, transform.position.y);
            else
                newPos = new Vector2(Random.Range(-spawnLine, spawnLine), transform.position.y);

            if (_data.toSpawn[random] != null)
                Instantiate(_data.toSpawn[random], newPos, Quaternion.identity);
        }

        StartCoroutine(Generate(_data, _data.spawnRate));
    }
}
