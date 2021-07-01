using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [SerializeField] private float playerMoney = 0;
    private BoatStorage storage;
    private BoatFishing fishing;

    private void Awake()
    {
        fishing = GetComponent<BoatFishing>();
        storage = GetComponent<BoatStorage>();
    }

    public bool GetIsCanStartCatchFish(Fish fish)
    {
        return storage.currentWeight + fish.weight <= storage.MaxCapacity ? true : false;
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
