using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStorage : MonoBehaviour
{
    
    private List<FishInStorage> fishList = new List<FishInStorage>();
    public float moneyCost { get; private set; } = 0;
    public float currentWeight { get; private set; } = 0;
    public float maxCapacity { get { return BoatManager.instance.maxFishCapacity; } }

    public void AddFish(Fish fish)
    {
        var fs = new FishInStorage(fish.weight, fish.price);
        fishList.Add(fs);
        moneyCost += fs.price;
        currentWeight += fs.weight;
        print("fish added: вес " + fs.weight + " стоимость " + fs.price);
    }

    public void SetEmptyStorage()
    {
        moneyCost = 0;
        currentWeight = 0;
        fishList.Clear();
    }
}