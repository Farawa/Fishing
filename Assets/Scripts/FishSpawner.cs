using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private string uniqueSpawnName;
    [Space]
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int maxFishCount = 10;
    [SerializeField] private float spawnDelay = 5;
    [Space]
    [SerializeField] private float fishSpeed = 3;
    [SerializeField] private float fishWeight = 5;
    [SerializeField] private float fishPrice = 20;
    [SerializeField] private float fishHealth = 100;

    private List<GameObject> fishList = new List<GameObject>();
    private Vector3 spawnerSize;

    private DateTime lastSpawnDate
    {
        get
        {
            var line = Preffs.GetString(uniqueSpawnName + "lastSpawnDate");
            if (line.Length == 0)
                return new DateTime(2020, 1, 1);
            return DateTime.Parse(line);
        }
        set
        {
            Preffs.SetString(uniqueSpawnName + "lastSpawnDate",value.ToString());
        }
    }

    private int lastFishCount
    {
        get
        {
            return Preffs.GetInt(uniqueSpawnName + "lastCount");
        }
        set
        {
            Preffs.SetInt(uniqueSpawnName + "lastCount",value);
        }
    }

    private void Awake()
    {
        spawnerSize = transform.localScale;
        GetComponent<MeshRenderer>().enabled = false;
        transform.localScale = Vector3.one;
    }

    private void Start()
    {
        FirstFishSpawn();
    }

    private void FirstFishSpawn()
    {
        var lastCount = lastFishCount;
        var needCount = maxFishCount - lastCount;
        var timeDifference = DateTime.Now - lastSpawnDate;
        var fishFromTime = timeDifference.TotalSeconds / spawnDelay;
        if (fishFromTime > needCount) fishFromTime = needCount;
        fishFromTime += lastCount;

        PlaceFish((int)fishFromTime);

        var remainTime = timeDifference.TotalSeconds % spawnDelay;
        var remainSpan = new TimeSpan(0, 0, (int)remainTime);
        lastSpawnDate = DateTime.Now.Subtract(remainSpan);
    }

    private void Update()
    {
        if (GetCurrentFishCount() >= maxFishCount) return;
        var timeDifference = DateTime.Now - lastSpawnDate;
        if(timeDifference.TotalSeconds>spawnDelay)
        {
            PlaceFish(1);
            lastSpawnDate = DateTime.Now;
        }
    }

    private void OnDestroy()
    {
        lastFishCount = GetCurrentFishCount();
    }

    private Vector3 GetRandomFishPosition()
    {
        var x = UnityEngine.Random.Range(-spawnerSize.x, spawnerSize.x);
        var z = UnityEngine.Random.Range(-spawnerSize.z, spawnerSize.z);
        var y = 0;
        return new Vector3(x, y, z);
    }

    private void PlaceFish(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var fish = GetFish();
            fish.transform.localPosition = GetRandomFishPosition();
        }
    }

    private GameObject GetFish()
    {
        foreach (var fish in fishList)
        {
            if (!fish.activeSelf)
            {
                fish.SetActive(true);
                return fish;
            }
        }
        var t = Instantiate(fishPrefab, transform);
        t.GetComponent<Fish>().Setup(fishHealth, fishPrice, fishWeight, fishSpeed, spawnerSize);
        fishList.Add(t);
        return t;
    }

    private int GetCurrentFishCount()
    {
        int count = 0;
        foreach (var fish in fishList)
        {
            if (fish.activeSelf)
                count++;
        }
        return count;
    }
}