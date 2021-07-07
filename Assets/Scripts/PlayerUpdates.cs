using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdates : MonoBehaviour
{
    public static PlayerUpdates instance = null;

    [SerializeField] private BoatManager boat;

    [SerializeField] private ParameterData[] datas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    private ParameterData GetData(ParameterType type)
    {
        foreach(var data in datas)
        {
            if (data.type == type)
                return data;
        }
        throw new System.Exception();
    }

    public ShopValues GetShopValues(ParameterType type)
    {
        return GetData(type).GetStruct();
    }

    public int GetParameterLevel(ParameterType type)
    {
        return GetData(type).GetCurrentLevel();
    }

    public float GetParameterValue( ParameterType type)
    {
        var data = GetData(type);
        return data.GetValue(data.GetCurrentLevel());
    }

    public bool TryImproveParameter(ParameterType type)
    {
        var data = GetData(type);
        var money = BoatManager.instance.playerMoney;
        var price = data.GetPrice(data.GetCurrentLevel() + 1);
        if (money >= price)
        {
            data.SetCurrentLevel(data.GetCurrentLevel() + 1);
            BoatManager.instance.playerMoney -= price;
            UpdateParameters();
            print("куплено");
            return true;
        }
        print("не куплено");
        return false;
    }

    public void UpdateParameters()
    {
        var power = GetData(ParameterType.power).GetValue(GetParameterLevel(ParameterType.power));
        var capacity = GetData(ParameterType.capacity).GetValue(GetParameterLevel(ParameterType.capacity));
        var rodsCount = GetData(ParameterType.rodsCount).GetValue(GetParameterLevel(ParameterType.rodsCount));
        var rodsPerFish = GetData(ParameterType.rodsPerFish).GetValue(GetParameterLevel(ParameterType.rodsPerFish));
        var speed = GetData(ParameterType.speed).GetValue(GetParameterLevel(ParameterType.speed));
        var catchDistance = GetData(ParameterType.distance).GetValue(GetParameterLevel(ParameterType.distance));
        var health = GetData(ParameterType.health).GetValue(GetParameterLevel(ParameterType.health));

        boat.UpdateImprovements(power, capacity, (int)rodsCount, (int)rodsPerFish, speed, catchDistance, health);
    }
}