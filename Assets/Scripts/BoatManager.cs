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
    public int rodsCount = 6;
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

    private void Start()
    {
        PlayerUpdates.instance.UpdateParameters();
    }

    public void UpdateImprovements(float power, float capacity, int rods, int rodsPerFish, float speed, float catchDistance, float health)
    {
        catchPower = power;
        maxFishCapacity = capacity;
        rodsCount = rods;
        maxRodsPerFish = rodsPerFish;
        boatSpeed = speed;
        maxCatchDistance = catchDistance;
        maxHealth = health;
        BoatFishing.instance.SetRodsCount(rodsCount);
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

    public void TakeDamage(float value)
    {
        currentHealth -= value;
    }
}
