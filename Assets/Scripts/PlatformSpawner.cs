using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _platformSets;

    [SerializeField]
    private Vector3 _distanceOffset;

    internal bool isSpawn;

    private void Update()
    {
        if(isSpawn)
        {
            GameObject platformPrefab = _platformSets[Random.Range(0, _platformSets.Length)];

            GameObject platform = Instantiate(platformPrefab, transform.position + _distanceOffset, Quaternion.identity);
            isSpawn = false;
        }
    }

}
