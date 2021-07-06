using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdates : MonoBehaviour
{
    public static PlayerUpdates instance = null;

    [SerializeField] private BoatManager boat;

    [SerializeField] private BoatPowerSO powerSO;
    [SerializeField] private FishCapacitySO capacitySO;
    [SerializeField] private RodsCountSO rodsCountSO;
    [SerializeField] private RodsPerFishSO rodsPerFishSO;
    [SerializeField] private BoatSpeedSO speedSO;
    [SerializeField] private CatchDistanceSO distanceSO;
    [SerializeField] private BoatHealthSO healthSO;

    public int powerLevel
    {
        get { return Preffs.GetInt("RodsPowerLevel", 0); }
        set { Preffs.SetInt("RodsPowerLevel", value); UpdateParameters(); }
    }

    public int capacityLevel
    {
        get { return Preffs.GetInt("BoatCapacityLevel", 0); }
        set { Preffs.SetInt("BoatCapacityLevel", value); UpdateParameters(); }
    }

    public int rodsCountLevel
    {
        get { return Preffs.GetInt("RodsCountLevel", 0); }
        set { Preffs.SetInt("RodsCountLevel", value); UpdateParameters(); }
    }
    public int rodsPerFishLevel
    {
        get { return Preffs.GetInt("RodsPerFishLevel", 0); }
        set { Preffs.SetInt("RodsPerFishLevel", value); UpdateParameters(); }
    }

    public int speedLevel
    {
        get { return Preffs.GetInt("BoatSpeedLevel", 0); }
        set { Preffs.SetInt("BoatSpeedLevel", value); UpdateParameters(); }
    }

    public int distanceLevel
    {
        get { return Preffs.GetInt("CatchDistanceLevel", 0); }
        set { Preffs.SetInt("CatchDistanceLevel", value); UpdateParameters(); }
    }

    public int healthLevel
    {
        get { return Preffs.GetInt("BoatHealthLevel", 0); }
        set { Preffs.SetInt("BoatHealthLevel", value); UpdateParameters(); }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    public void UpdateParameters()
    {
        var power = powerSO.power[powerLevel];
        var capacity = capacitySO.fishCapacity[capacityLevel];
        var rodsCount = rodsCountSO.rods[rodsCountLevel];
        var rodsPerFish = rodsPerFishSO.rodsPerFish[rodsPerFishLevel];
        var speed = speedSO.boatSpeed[speedLevel];
        var catchDistance = distanceSO.distance[distanceLevel];
        var health = healthSO.health[healthLevel];

        boat.UpdateImprovements(power, capacity, rodsCount, rodsPerFish, speed, catchDistance, health);
    }
}