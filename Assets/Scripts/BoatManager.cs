using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public static BoatManager instance = null;

    public float playerMoney = 0;
    [Space]
    public float catchPower = 10;
    public float maxFishCapacity = 100;
    public float rodsCount = 6;
    public int maxRodsPerFish = 2;
    public float boatSpeed = 5;
    public float maxCatchDistance = 5;
    public float maxHealth = 100;

    private float currentHealth = 100;
    private BoatStorage storage;
    private BoatFishing fishing;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
        fishing = GetComponent<BoatFishing>();
        storage = GetComponent<BoatStorage>();
    }

    public bool GetIsCanStartCatchFish(Fish fish)
    {
        return storage.currentWeight + fish.weight <= maxFishCapacity ? true : false;
    }

    public void AddFish(Fish fish)
    {
        storage.AddFish(fish);
        fishing.UpdateRods();
    }

    public void SellAll()
    {
        playerMoney += storage.moneyCost;
        storage.SetEmptyStorage();
    }
}
