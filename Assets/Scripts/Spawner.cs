using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private float maxHeight = 1f;
    [SerializeField] private float minHeight = -1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn),spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position +=Vector3.up * Random.Range(minHeight,maxHeight);
    }

}
