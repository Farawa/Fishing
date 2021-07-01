using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFishing : MonoBehaviour
{
    public static BoatFishing instance = null;

    [SerializeField] private Rod[] rods;
    [SerializeField] private float maxCatchDistance = 5;
    [SerializeField] private float catchPower = 10;
    [SerializeField] private int maxRodsPerFish = 2;
    [SerializeField] private FishingHelper fishingHelper;
    private BoatManager boatManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            throw new System.Exception();
        boatManager = GetComponent<BoatManager>();
        fishingHelper.SetColliderSize(maxCatchDistance);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            var fish = collision.gameObject.GetComponent<Fish>();
            if (fish.pullingRodsCount < maxRodsPerFish && boatManager.GetIsCanStartCatchFish(fish))
            {
                var rod = GetFreeRod();
                if (rod != null)
                {
                    rod.PullFish(fish, maxCatchDistance, catchPower);
                    print("start pool");
                }
            }
        }
    }

    public void UpdateRods()
    {
        var isHaveOnePullRod = false;
        foreach (var rod in rods)
        {
            if (!rod.isFree)
            {
                if (!boatManager.GetIsCanStartCatchFish(rod.currentFish))
                {
                    rod.StopPull();
                    print("pull stoped");
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
            if (rod.isFree)
                return rod;
        }
        return null;
    }

    public void CatchFish(Fish fish)
    {
        boatManager.AddFish(fish);
        fish.DestroySelf();
    }
}
