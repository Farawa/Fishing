using System.Collections.Generic;
using UnityEngine;

public class BoatStorage : MonoBehaviour
{
    public static BoatStorage instance = null;

    private List<FishInStorage> fishList = new List<FishInStorage>();
    public float moneyCost { get; private set; } = 0;
    public float currentWeight { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

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

    public float GetMoneyFromFish(float weight)
    {
        var mass = 0f;
        var price = 0f;
        for (int i = fishList.Count - 1; i >= 0; i--)
        {
            if (mass + fishList[i].weight <= weight)
            {
                price += fishList[i].price;
                moneyCost -= fishList[i].price;
                currentWeight -= fishList[i].weight;
                fishList.RemoveAt(i);
            }
        }
        return price;
    }
}