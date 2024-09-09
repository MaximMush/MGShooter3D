using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject[] ZombieCount;

    public int maxZombiesOnWave = 10;

    public int ZombieKillsOnWave;

    private void Update()
    {
        if (ZombieKillsOnWave >= maxZombiesOnWave)
        {
            ChangeWave();
        }
        CountZombiesOfWave();
    }

    void CountZombiesOfWave()
    {
        ZombieCount = GameObject.FindGameObjectsWithTag("Zombie");
    }

    void ChangeWave()
    {
        maxZombiesOnWave++;

        ZombieKillsOnWave = 0;

        for (int i = 0; i < ZombieCount.Length; i++)
        {
            Destroy(ZombieCount[i].gameObject);
        }
    }
}
