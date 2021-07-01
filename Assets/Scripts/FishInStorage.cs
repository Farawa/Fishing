using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FishInStorage
{
    public FishInStorage(float weight, float price)
    {
        this.weight = weight;
        this.price = price;
    }

    public float weight;
    public float price;
}