using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFishing : MonoBehaviour
{
    public static BoatFishing instance = null;

    [SerializeField] private Rod[] rods;
    [SerializeField] private float maxCatchDistance = 5;
    [SerializeField] private float catchPower = 10;
    private SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = maxCatchDistance;
        if (instance == null)
            instance = this;
        else
            throw new System.Exception("больше 1го кораблика");
    }

    private void OnTriggerStay(Collider collision)
    {
        print("кого-то нашел: " + collision.transform.name);
        if (collision.gameObject.tag == "Fish")
        {
            var fish = collision.gameObject.GetComponent<Fish>();
            if (!fish.isPulling)
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

    private Rod GetFreeRod()
    {
        foreach (Rod rod in rods)
        {
            if (rod.isFree)
                return rod;
        }
        return null;
    }

    public void CatchFish()
    {
        print("fish catched");
    }
}
