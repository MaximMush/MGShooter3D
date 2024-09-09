using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    public Waves _waves;

    public GameObject Zombie;

    public float SpawnTime;
    
    public float MinSpawnTime = 5f;

    public float MaxSpawnTime = 15f;

    void Start()
    {
        _waves = FindObjectOfType<Waves>();

        Zombie = Resources.Load("Prefabs/Zombie") as GameObject;

        StartCoroutine("WaitTimeForSpawn");
    }
    
    IEnumerator WaitTimeForSpawn()
    { 
        while (true)
        {
            if (_waves.ZombieCount.Length < _waves.maxZombiesOnWave)
            {
                Instantiate(Zombie.gameObject, gameObject.transform.position, Quaternion.identity);

                SpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);

                yield return new WaitForSeconds(SpawnTime);

            }
            else
            {
                yield return new WaitForSeconds(SpawnTime);
            }
        }
    }
}
