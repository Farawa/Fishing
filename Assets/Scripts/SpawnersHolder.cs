using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersHolder : MonoBehaviour
{
    public static SpawnersHolder instance = null;

    [SerializeField] private FishSpawner[] spawners;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    public Fish GetFish()
    {
        foreach(var spawner in spawners)
        {
            var t = spawner.GetFreeFish();
            if (t != null)
            {
                return t;
            }
        }
        return null;
    }
}