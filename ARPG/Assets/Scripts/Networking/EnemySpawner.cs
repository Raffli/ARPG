﻿using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float range = 8f;
    public int level = 1;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-range, range),
                0.0f,
                Random.Range(-range, range));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, GetComponent<NetworkTransform>().gameObject.transform.position + spawnPosition, spawnRotation, GetComponent<NetworkTransform>().gameObject.transform);
            enemy.GetComponent<EnemyAI>().SetLevel(level);
            enemy.GetComponent<EnemyHealth>().SetLevel(level);
            NetworkServer.Spawn(enemy);
        }
    }
}