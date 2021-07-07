using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public static BoatManager instance = null;

    public float playerMoney { get { return Preffs.GetFloat("PlayerMoney"); } set { Preffs.SetFloat("PlayerMoney", value); } }
    public float currentHealth { get; private set; } = 100;

    public float catchPower = 10;
    public float maxFishCapacity = 100;
    public int rodsCount = 6;
    public int maxRodsPerFish = 2;
    public float boatSpeed = 5;
    public float maxCatchDistance = 5;
    public float maxHealth = 100;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
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
        BoatFishing.instance.SetCatchDistance(catchDistance);
    }

    public bool GetIsCanStartCatchFish(Fish fish)
    {
        return BoatStorage.instance.currentWeight + fish.weight <= maxFishCapacity ? true : false;
    }

    public void AddFish(Fish fish)
    {
        BoatStorage.instance.AddFish(fish);
        BoatFishing.instance.UpdateRods();
    }

    public void SellAll()
    {
        playerMoney += BoatStorage.instance.moneyCost;
        BoatStorage.instance.SetEmptyStorage();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
    }
}
