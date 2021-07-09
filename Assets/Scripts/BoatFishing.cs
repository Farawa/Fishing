using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFishing : MonoBehaviour
{
    public static BoatFishing instance = null;

    [SerializeField] private Rod[] rods;
    [SerializeField] private FishingHelper fishingHelper;
    public float catchPower { get { return BoatManager.instance.catchPower; } }
    public float maxCatchDistance { get { return BoatManager.instance.maxCatchDistance; } }
    public int maxRodsPerFish { get { return BoatManager.instance.maxRodsPerFish; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
    }

    public void SetCatchDistance(float value)
    {
        fishingHelper.SetColliderSize(value);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            var fish = collision.gameObject.GetComponent<Fish>();
            if (fish.pullingRodsCount < maxRodsPerFish && BoatManager.instance.GetIsCanStartCatchFish(fish))
            {
                var rod = GetFreeRod();
                if (rod != null)
                {
                    rod.PullFish(fish, maxCatchDistance, catchPower);
                }
            }
        }
    }

    public void SetRodsCount(int value)
    {
        for (int i = 0; i < value; i++)
        {
            rods[i].gameObject.SetActive(true);
        }
        for (int i = value; i < rods.Length; i++)
        {
            rods[i].gameObject.SetActive(false);
        }
    }

    public void UpdateRods()
    {
        var isHaveOnePullRod = false;
        foreach (var rod in rods)
        {
            if (!rod.isFree)
            {
                if (!BoatManager.instance.GetIsCanStartCatchFish(rod.currentFish))
                {
                    rod.StopPull();
                }
                else
                {
                    isHaveOnePullRod = true;
                }
            }
        }
        if (!isHaveOnePullRod)
        {
            //TODO
            print("Хранилище полное");
        }
    }

    private Rod GetFreeRod()
    {
        foreach (Rod rod in rods)
        {
            if (rod.isFree&&rod.gameObject.activeSelf)
                return rod;
        }
        return null;
    }

    public void CatchFish(Fish fish)
    {
        BoatManager.instance.AddFish(fish);
        fish.gameObject.SetActive(false);
    }
}
